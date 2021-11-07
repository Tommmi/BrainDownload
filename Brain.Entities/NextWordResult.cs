using System;
using System.Collections.Generic;
using System.Text;
using Brain.Entities.UserStatus;

namespace Brain.Entities
{
	public class NextWordResult
	{
		public bool Succeeded { get; }
		public Word Vocable { get; }
		public WordStatus WordStatus { get; }

		public NextWordResult(bool succeeded, Word vocable, WordStatus wordStatus)
		{
			Succeeded = succeeded;
			Vocable = vocable;
			WordStatus = wordStatus;
		}
	}
}
