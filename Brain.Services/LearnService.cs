using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brain.Entities;
using Brain.Entities.UserStatus;
using Brain.Interfaces;

namespace Brain.Services
{
	public class LearnService : ILearnService
	{
		private readonly ISystemTime _systemTime;
		private readonly IUserRepository _userRepository;

		private object _sync = new object();
		private bool _initialized = false;

		private Dictionary<Guid, (WordStatus status, Word word)> _activeVocabulary;
		private Dictionary<Guid, Word> _newVocabulary;
		private UsersStatus _userStatus;

		public LearnService(
			ISystemTime systemTime,
			IUserRepository userRepository)
		{
			_systemTime = systemTime;
			_userRepository = userRepository;
		}

		private void Initialize()
		{
			lock (_sync)
			{
				if (_initialized)
				{
					return;
				}
				_userStatus = _userRepository.LoadStatus().Result;

				var words = _userRepository.LoadAllWords().Result.ToDictionary(
					keySelector: x => x.Id, elementSelector: x => x);
				var vocabulary = _userRepository.LoadCompleteVocablaryStatus()
					.Result
					.Where(x => !x.Deleted)
					.ToList();

				_activeVocabulary = new Dictionary<Guid, (WordStatus status, Word word)>();

				foreach (var wordStatus in vocabulary)
				{
					if (!words.TryGetValue(wordStatus.Id, out var word))
					{
						wordStatus.Deleted = true;
						_userRepository.SaveWordStatus(wordStatus).Wait();
					}
					else
					{
						_activeVocabulary[word.Id] = (status: wordStatus, word: word);
					}
				}

				_newVocabulary = words
					.Where(w => !_activeVocabulary.ContainsKey(w.Key)).
					ToDictionary(keySelector: w => w.Key, elementSelector: w => w.Value);

				_initialized = true;
			}
		}

		public async Task<NextWordResult> TryGetNextVocable()
		{
			Initialize();
			var now = _systemTime.GetUtcTime();
			var nextActiveWord = _activeVocabulary
				.OrderBy(word => word.Value.word.Prio)
				.Where(word => now > word.Value.status.NextRepetition)
				.FirstOrDefault();

			var nextNewWord = _newVocabulary.OrderBy(x => x.Value.Prio).FirstOrDefault();

			if (nextActiveWord.Value.word != null 
			    && (nextNewWord.Value==null 
			        || nextActiveWord.Value.word.Prio < nextNewWord.Value.Prio))
			{
				return new NextWordResult(succeeded: true, vocable: nextActiveWord.Value.word, wordStatus: nextActiveWord.Value.status);
			}

			if (nextNewWord.Value != null)
			{
				var status = new WordStatus(
					id: nextNewWord.Key,
					nextRepetition: now,
					lastRepetition: now - TimeSpan.FromDays(1), 
					cntApproved: 0,
					cntFailed: 0,
					deleted: false);
				_activeVocabulary[status.Id] = (status: status, word: nextNewWord.Value);
				_newVocabulary.Remove(nextNewWord.Key);
				_userStatus.CntWordsTaken++;
				await _userRepository.SaveUserStatus(_userStatus);
				return new NextWordResult(succeeded: true, vocable: nextNewWord.Value, wordStatus: status);
			}

			return new NextWordResult(succeeded: false, vocable: null, wordStatus: null);
		}

		public async Task SetTrainResult(Guid wordId, TrainResult trainResult)
		{
			Initialize();
			var word = _activeVocabulary[wordId];
			var now = _systemTime.GetUtcTime();

			switch (trainResult)
			{
				case TrainResult.WellKnown:
				case TrainResult.MoreOrLess:
					word.status.NextRepetition = GetNextRepetition(now: now, wordStatus: word.status, moreOrLess: trainResult == TrainResult.MoreOrLess);
					word.status.CntApproved++;
					break;
				case TrainResult.Failed:
					word.status.NextRepetition = now;
					word.status.CntFailed++;
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(trainResult), trainResult, null);
			}

			word.status.LastRepetition = now;
			await _userRepository.SaveWordStatus(word.status);
		}

		public async Task DelayVocable(Guid wordId)
		{
			Initialize();
			var word = _activeVocabulary[wordId];
			var now = _systemTime.GetUtcTime();
			word.status.NextRepetition = now + TimeSpan.FromHours(1);
			await _userRepository.SaveWordStatus(word.status);
		}

		public async Task<LearnProgress> GetProgress()
		{
			Initialize();

			double wordsInLongMemory = 0;
			double wordsInShortMemory = 0;

			foreach (var word in _activeVocabulary.Values)
			{
				double curInterval = (word.status.NextRepetition - word.status.LastRepetition).TotalSeconds;
				double overTime = (_systemTime.GetUtcTime() - word.status.NextRepetition).TotalSeconds;

				double progress = 1.0;

				if (overTime > 0)
				{
					progress = 1.0 - (overTime / (2.0 * curInterval));
				}

				if (progress < 0)
				{
					progress = 0;
				}
				else if (progress > 1.0)
				{
					progress = 1.0;
				}

				wordsInShortMemory += progress;

				if (curInterval > TimeSpan.FromDays(30).TotalSeconds)
				{
					wordsInLongMemory += progress;
				}
			}


			return new LearnProgress(
				wordsInLongMemory: wordsInLongMemory,
				wordsInShortMemory: wordsInShortMemory);
		}

		private DateTime GetNextRepetition(DateTime now, WordStatus wordStatus, bool moreOrLess)
		{
			TimeSpan waitTime = now - wordStatus.LastRepetition;

			if (waitTime.TotalMilliseconds < 0)
			{
				waitTime = _userStatus.FirstTimeSpan;
			}

			if (!moreOrLess)
			{
				waitTime += now - wordStatus.LastRepetition;
			}

			if (waitTime < _userStatus.FirstTimeSpan)
			{
				waitTime = _userStatus.FirstTimeSpan;
			}

			return now + waitTime;
		}
	}
}
