using System;
using System.Collections.Generic;
using System.Text;
using TinyCsvParser.Mapping;

namespace DownloadToBrain.common
{
	public class KeyValue
	{
		public string Col1 { get; set; }
		public string Col2 { get; set; }
		public string Col3 { get; set; }

		public KeyValue()
		{

		}

		public KeyValue(string col1, string col2, string col3)
		{
			Col1 = col1;
			Col2 = col2;
			Col3 = col3;
		}
	}

	public class CsvMapping : CsvMapping<KeyValue>
	{
		public CsvMapping()
			: base()
		{
			MapProperty(0, x => x.Col1);
			MapProperty(1, x => x.Col2);
			MapProperty(2, x => x.Col3);
		}
	}
}
