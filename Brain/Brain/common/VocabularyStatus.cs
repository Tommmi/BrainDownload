using System;
using System.Collections.Generic;
using System.Text;
using Brain.Entities.UserStatus;

namespace App1.common
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
