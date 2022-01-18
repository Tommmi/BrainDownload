using System.Collections.Generic;
using System.Linq;
using DownloadToBrain.common;

namespace DownloadToBrain.Infrastructure
{
	public class LabelRepository
	{
		private static Dictionary<string, string> _labels;
		private static object _sync = new object();

		public Dictionary<string, string> GetLabels()
		{
			if (_labels == null)
			{
				lock (_sync)
				{
					if (_labels == null)
					{
						_labels = new AppResource("Labels.csv")
							.Items
							.ToDictionary(
								keySelector: x => x.values[0],
								elementSelector: x => x.values[1]);
					}
				}
			}

			return _labels;
		}
	}
}
