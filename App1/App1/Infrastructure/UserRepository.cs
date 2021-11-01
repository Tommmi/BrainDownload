using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App1.common;
using Brain.Entities;
using Brain.Entities.UserStatus;
using Brain.Interfaces;
using Xamarin.Forms;

namespace App1.Infrastructure
{
	public class UserRepository : IUserRepository
	{
		#region fields

		private const string KEYUSERSTATUS = "userStatus";
		private static object _sync = new object();
		private static List<Word> _vocabulary;
		private static List<WordStatus> _vocabularyStatus;
		private static Dictionary<string, string> _labels;
		private static Brain.Entities.UserStatus.UsersStatus _usersStatus;

		private AppStorage _appStorage { get; } = new AppStorage();
		bool isBusy = false;

		string title = string.Empty;

		#endregion


		#region private methods


		private Dictionary<string, string> GetLabels()
		{
			if (_labels == null)
			{
				lock (_sync)
				{
					if (_labels == null)
					{
						_labels = new AppResource("Labels.csv")
							.Items
							.ToDictionary(
								keySelector: x => x.values[0],
								elementSelector: x => x.values[1]);
					}
				}
			}

			return _labels;
		}

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
						_usersStatus = usersStatus;
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
						var vocabularyStatus = _appStorage.TryGet<VocabularyStatus>(KEYUSERSTATUS).Result;
						_vocabularyStatus = vocabularyStatus.Words;
					}
				}
			}

			return _vocabularyStatus;
			
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

		public Task SaveWordStatus(WordStatus vocableStatus)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
