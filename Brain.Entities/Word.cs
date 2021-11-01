using System;
using System.Collections.Generic;
using System.Text;

namespace Brain.Entities
{
	public class Word
	{
		public Guid Id { get; set; }
		public int Prio { get; set; }
		public string Left { get; set; }
		public string Right { get; set; }

		public Word()
		{

		}
		public Word(Guid id, int prio, string left, string right)
		{
			Id = id;
			Prio = prio;
			Left = left;
			Right = right;
		}
	}
}
