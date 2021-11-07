using System;
using System.Collections.Generic;
using System.Text;

namespace Brain.Entities
{
	public class LearnProgress
	{
		public double WordsInLongMemory { get; }
		public double WordsInShortMemory { get; }

		public LearnProgress(
			double wordsInLongMemory,
			double wordsInShortMemory)
		{
			WordsInLongMemory = wordsInLongMemory;
			WordsInShortMemory = wordsInShortMemory;
		}
	}
}
