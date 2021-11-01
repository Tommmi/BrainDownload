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

		public async Task Initialize()
		{
			_userStatus = await _userRepository.LoadStatus();

			var words = (await _userRepository.LoadAllWords()).ToDictionary(
				keySelector: x => x.Id, elementSelector: x => x);
			var vocabulary = (await _userRepository.LoadCompleteVocablaryStatus())
				.Where(x => !x.Deleted)
				.ToList();

			_activeVocabulary = new Dictionary<Guid, (WordStatus status, Word word)>();

			foreach (var wordStatus in vocabulary)
			{
				if (!words.TryGetValue(wordStatus.Id, out var word))
				{
					wordStatus.Deleted = true;
					await _userRepository.SaveWordStatus(wordStatus);
				}
				else
				{
					_activeVocabulary[word.Id] = (status: wordStatus, word: word);
				}
			}
		}

		public async Task<(bool succeeded, Word vocable, WordStatus wordStatus)> TryGetNextVocable()
		{
			var now = _systemTime.GetUtcTime();
			var nextWord = _activeVocabulary
				.Where(word => now > word.Value.status.NextRepetition)
				.OrderBy(word => word.Value.word.Prio)
				.FirstOrDefault();

			if (nextWord.Value.word != null)
			{
				return (succeeded: true, vocable: nextWord.Value.word, wordStatus: nextWord.Value.status);
			}

			var nextNewWord = _newVocabulary.OrderBy(x => x.Value.Prio).FirstOrDefault();
			if (nextNewWord.Value != null)
			{
				var status = new WordStatus(
					id: nextNewWord.Key,
					nextRepetition: now,
					lastRepetition: now,
					cntApproved: 0,
					cntFailed: 0,
					deleted: false);
				_activeVocabulary[status.Id] = (status: status, word: nextNewWord.Value);
				_userStatus.CntWordsTaken++;
				await _userRepository.SaveUserStatus(_userStatus);
				return (succeeded: true, vocable: nextNewWord.Value, wordStatus: status);
			}

			return (succeeded: false, vocable: null, wordStatus: null);
		}

		public async Task SetTrainResult(Guid wordId, TrainResult trainResult)
		{
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

		private DateTime GetNextRepetition(DateTime now, WordStatus wordStatus, bool moreOrLess)
		{
			TimeSpan waitTime = now - wordStatus.LastRepetition;

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
