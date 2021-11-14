using System;
using System.Collections.Generic;
using System.Text;

namespace DownloadToBrain.common
{
	public static class DoubleExtension
	{
		public static int ToInt(this double val)
		{
			return Convert.ToInt32(Math.Floor(val));
		}
	}
}
