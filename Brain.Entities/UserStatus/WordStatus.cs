using System;

namespace Brain.Entities.UserStatus
{
	public class WordStatus
	{
		public Guid Id { get; set; }
		public DateTime NextRepetition { get; set; }
		public DateTime LastRepetition { get; set; }
		public int CntApproved { get; set; }
		public int CntFailed { get; set; }
		public bool Deleted { get; set; }

		public WordStatus()
		{

		}

		public WordStatus(
			Guid id,
			DateTime nextRepetition,
			DateTime lastRepetition,
			int cntApproved,
			int cntFailed,
			bool deleted)
		{
			Id = id;
			NextRepetition = nextRepetition;
			LastRepetition = lastRepetition;
			CntApproved = cntApproved;
			CntFailed = cntFailed;
			Deleted = deleted;
		}

	}
}
