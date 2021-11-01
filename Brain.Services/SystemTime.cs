using System;
using System.Collections.Generic;
using System.Text;
using Brain.Interfaces;

namespace Brain.Services
{
	public class SystemTime : ISystemTime
	{
		public DateTime GetUtcTime()
		{
			return DateTime.UtcNow;
		}
	}
}
