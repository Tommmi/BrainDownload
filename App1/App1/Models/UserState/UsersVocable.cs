using System;

namespace App1.Models.UserState
{
	public class UsersVocable
	{
		public int Nb { get; set; }
		public DateTime NextRepetition { get; set; }
		public int CntApproved { get; set; }
		public int CntFailed { get; set; }

		public UsersVocable()
		{

		}

		public UsersVocable(
			int nb,
			DateTime nextRepetition,
			int cntApproved,
			int cntFailed)
		{
			Nb = nb;
			NextRepetition = nextRepetition;
			CntApproved = cntApproved;
			CntFailed = cntFailed;
		}

	}
}
