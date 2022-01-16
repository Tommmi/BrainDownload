using System.Collections.Generic;
using Brain.Entities.UserStatus;

namespace DownloadToBrain.common
{
	public class VocabularyStatus
	{
		public List<WordStatus> Words { get; set; }

		public VocabularyStatus()
		{

		}
		public VocabularyStatus(List<WordStatus> words)
		{
			Words = words;
		}
	}
}
