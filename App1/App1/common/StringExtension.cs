using System;
using System.Collections.Generic;
using System.Text;

namespace App1.common
{
	public static class StringExtension
	{
		public static string ReplaceLineFeeds(this string html)
		{
			return html.Replace("\n", "<br />");
		}
	}
}
