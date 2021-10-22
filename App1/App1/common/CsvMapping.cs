using System;
using System.Collections.Generic;
using System.Text;
using TinyCsvParser.Mapping;

namespace App1.common
{
	public class KeyValue
	{
		public string Key { get; set; }
		public string Value { get; set; }

		public KeyValue()
		{

		}

		public KeyValue(string key, string value)
		{
			Key = key;
			Value = value;
		}
	}

	public class CsvMapping : CsvMapping<KeyValue>
	{
		public CsvMapping()
			: base()
		{
			MapProperty(0, x => x.Key);
			MapProperty(1, x => x.Value);
		}
	}
}
