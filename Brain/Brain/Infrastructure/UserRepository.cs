using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Brain.Entities;
using Brain.Entities.UserStatus;
using Brain.Interfaces;
using DownloadToBrain.common;

namespace DownloadToBrain.Infrastructure
{
	public class UserRepository : IUserRepository
	{
		#region fields

		private const string KEYUSERSTATUS = "userStatus";
		private const string KEYVOCABULARYSTATUS = "vocabularyStatus";
		private static object _sync = new object();
		private static List<Word> _vocabulary;
		private static VocabularyStatus _vocabularyStatus;
		private static Dictionary<string, string> _labels;
		private static Brain.Entities.UserStatus.UsersStatus _usersStatus;

		private AppStorage _appStorage { get; } = new AppStorage();
		bool isBusy = false;

		string title = string.Empty;

		#endregion



		#region IUserRepository

		public async Task<Brain.Entities.UserStatus.UsersStatus> LoadStatus()
		{
			if (_usersStatus == null)
			{
				lock (_sync)
				{
					if (_usersStatus == null)
					{
						var usersStatus = _appStorage.TryGet<UsersStatus>(KEYUSERSTATUS).Result;
						_usersStatus = usersStatus??new UsersStatus(cntWordsTaken:0,firstTimeSpan:TimeSpan.FromSeconds(10));
					}
				}
			}

			return _usersStatus;
		}

		public async Task<List<WordStatus>> LoadCompleteVocablaryStatus()
		{
			if (_vocabularyStatus == null)
			{
				lock (_sync)
				{
					if (_vocabularyStatus == null)
					{
						_vocabularyStatus = _appStorage.TryGet<VocabularyStatus>(KEYVOCABULARYSTATUS).Result;
					}
				}
			}

			return _vocabularyStatus?.Words??new List<WordStatus>();
			
		}

		public async Task<List<Word>> LoadAllWords()
		{
			if (_vocabulary != null)
			{
				return _vocabulary;
			}

			lock (_sync)
			{
				if (_vocabulary == null)
				{
					int prio = 0;
					_vocabulary = new AppResource("Vocabulary.csv")
						.Items
						.Select(x =>
							new Word(id: Guid.Parse(x.id), prio: prio++, left: x.values[0], right: x.values[1]))
						.ToList();
				}
				return _vocabulary;
			}
		}

		public async Task SaveUserStatus(Brain.Entities.UserStatus.UsersStatus usersStatus)
		{
			await LoadStatus();

			lock (_sync)
			{
				_usersStatus = usersStatus;
				_appStorage.Save(KEYUSERSTATUS, usersStatus).Wait();
			}
		}

		public async Task SaveWordStatus(WordStatus vocableStatus)
		{
			lock (_sync)
			{
				var oldStatus = _vocabularyStatus?.Words.FirstOrDefault(x => x.Id == vocableStatus.Id);
				if (oldStatus != null)
				{
					_vocabularyStatus.Words.Remove(oldStatus);
				}
				else
				{
					if (_vocabularyStatus == null)
					{
						_vocabularyStatus = new VocabularyStatus(new List<WordStatus>());
					}
				}
				
				_vocabularyStatus.Words.Add(vocableStatus);

				_appStorage.Save(KEYVOCABULARYSTATUS, _vocabularyStatus).Wait();
			}
		}

		#endregion
	}
}
