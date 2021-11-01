using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Brain.Entities;
using Brain.Entities.UserStatus;
using Brain.Interfaces;

namespace Brain.Infrastructure
{
	public class UserRepository : IUserRepository
	{
		#region fields

		private static object _sync = new object();
		private static Dictionary<Guid, (string left, string right)> _vocabulary;
		private Dictionary<string, string> _labels;

		#endregion

		#region properties

		protected AppStorage UserData { get; } = new AppStorage();
		protected Dictionary<Guid, (string left, string right)> Vocabulary => GetVocabulary();
		protected Dictionary<string, string> Labels => GetLabels();



		public IDataStore<Item> DataStore => DependencyService.Get<IDataStore<Item>>();

		public string Title
		{
			get { return title; }
			set { SetProperty(ref title, value); }
		}

		public bool IsBusy
		{
			get { return isBusy; }
			set { SetProperty(ref isBusy, value); }
		}


		#endregion

		#region private methods

		private Dictionary<Guid, (string left, string right)> GetVocabulary()
		{
			if (_vocabulary == null)
			{
				lock (_sync)
				{
					if (_vocabulary == null)
					{
						var dic = new AppResource("Vocabulary.csv").Dic;
						_vocabulary = dic.ToDictionary(keySelector: x => Guid.Parse(x.Key),
							elementSelector: x => (left: x.Value[0], right: x.Value[1]));
					}
				}
			}

			return _vocabulary;
		}

		private Dictionary<string, string> GetLabels()
		{
			if (_labels == null)
			{
				lock (_sync)
				{
					if (_labels == null)
					{
						var dic = new AppResource("Labels.csv").Dic;
						_labels = dic.ToDictionary(keySelector: x => x.Value[0],
							elementSelector: x => x.Value[1]);
					}
				}
			}

			return _labels;
		}

		#endregion




		public Task<UsersStatus> LoadStatus()
		{
			
		}

		public Task<List<WordStatus>> LoadCompleteVocablaryStatus()
		{
			
		}

		public Task<List<Word>> LoadAllWords()
		{
			
		}

		public Task SaveUserStatus(UsersStatus usersStatus)
		{

		}

		public Task SaveWordStatus(WordStatus vocableStatus)
		{
		}
	}
}
