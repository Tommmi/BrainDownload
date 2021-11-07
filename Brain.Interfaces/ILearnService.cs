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
		Task<NextWordResult> TryGetNextVocable();
		Task SetTrainResult(Guid wordId, TrainResult trainResult);
	}
}
