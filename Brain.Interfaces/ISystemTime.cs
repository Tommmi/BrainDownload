using System;
using System.Collections.Generic;
using System.Text;

namespace Brain.Interfaces
{
	public interface ISystemTime
	{
		DateTime GetUtcTime();
	}
}
