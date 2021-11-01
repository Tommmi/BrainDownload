using System;

namespace Brain.Entities.UserStatus
{
	public class UsersStatus
	{
		public int CntWordsTaken { get; set; }
		public TimeSpan FirstTimeSpan { get; set; }

		public UsersStatus()
		{

		}
		public UsersStatus(
			int cntWordsTaken,
			TimeSpan firstTimeSpan)
		{
			CntWordsTaken = cntWordsTaken;
			FirstTimeSpan = firstTimeSpan;
		}
	}
}
