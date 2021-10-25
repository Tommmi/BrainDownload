namespace App1.Models.UserState
{
	public class UsersStatus
	{
		public int CntWordsTaken { get; set; }

		public UsersStatus()
		{

		}
		public UsersStatus(
			int cntWordsTaken)
		{
			CntWordsTaken = cntWordsTaken;
		}
	}
}
