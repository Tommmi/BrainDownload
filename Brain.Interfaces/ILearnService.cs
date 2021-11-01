using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Brain.Entities;
using Brain.Entities.UserStatus;

namespace Brain.Interfaces
{
	public interface ILearnService
	{
		Task Initialize();
		Task<(bool succeeded, Word vocable, WordStatus wordStatus)> TryGetNextVocable();
		Task SetTrainResult(Guid wordId, TrainResult trainResult);
	}
}
