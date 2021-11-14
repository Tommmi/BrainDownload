using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using TinyCsvParser;

namespace DownloadToBrain.common
{
	public class AppResource
	{
		private readonly string _csvFilename;
		private static object _sync = new Object();
		private List<(string id, string[] values)> _items;

		public List<(string id, string[] values)> Items => GetItems(_csvFilename);

		public AppResource(string csvFilename)
		{
			_csvFilename = csvFilename;
		}

		private List<(string id, string[] values)> GetItems(string scvFileName)
		{
			if (_items == null)
			{
				lock (_sync)
				{
					if (_items == null)
					{
						_items = LoadItemsFromFile(scvFileName);
					}
				}
			}

			return _items;
		}

		private List<(string id, string[] values)> LoadItemsFromFile(string fileName)
		{
			string csvContent = LoadFile(fileName);

			CsvParserOptions csvParserOptions = new CsvParserOptions(skipHeader:true, fieldsSeparator:';');
			var csvMapper = new CsvMapping();
			var csvParser = new CsvParser<KeyValue>(csvParserOptions, csvMapper);

			var records = csvParser.ReadFromString(new CsvReaderOptions(newLine: new[] { "\r\n" }), csvContent).ToList();

			return records
				.Where(r => r.IsValid)
				.Select(r => r.Result)
				.Select(x => (id: x.Col1, values: new string[] { x.Col2, x.Col3 }))
				.ToList();
		}

		private string LoadFile(string fileName)
		{
			var assembly = Assembly.GetExecutingAssembly();
			string resourceName = assembly.GetManifestResourceNames().Single(str => str.EndsWith(fileName));

			using (Stream stream = assembly.GetManifestResourceStream(resourceName))
			using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
			{
				string result = reader.ReadToEnd();
				return result;
			}
		}


	}
}
