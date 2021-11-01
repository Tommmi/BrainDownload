using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Brain.Entities;
using Brain.Entities.UserStatus;

namespace Brain.Interfaces
{
	public interface IUserRepository
	{
		Task<UsersStatus> LoadStatus();
		Task<List<WordStatus>> LoadCompleteVocablaryStatus();
		Task<List<Word>> LoadAllWords();

		Task SaveUserStatus(UsersStatus usersStatus);
		Task SaveWordStatus(WordStatus vocableStatus);

	}
}
