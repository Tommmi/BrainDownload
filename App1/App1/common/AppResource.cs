using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using TinyCsvParser;

namespace App1.common
{
	public class AppResource
	{
		private readonly string _csvFilename;
		private static object _sync = new object();
		private static Dictionary<string, string> _dic;

		public Dictionary<string, string> Dic => GetDictionary(_csvFilename);

		public AppResource(string csvFilename)
		{
			_csvFilename = csvFilename;
		}

		private Dictionary<string, string> GetDictionary(string scvFileName)
		{
			if (_dic == null)
			{
				lock (_sync)
				{
					if (_dic == null)
					{
						_dic = LoadDicFromFile(scvFileName);
					}
				}
			}

			return _dic;
		}

		private Dictionary<string, string> LoadDicFromFile(string fileName)
		{
			string csvContent = LoadFile(fileName);

			CsvParserOptions csvParserOptions = new CsvParserOptions(true, ';');
			var csvMapper = new CsvMapping();
			var csvParser = new CsvParser<KeyValue>(csvParserOptions, csvMapper);

			var records = csvParser.ReadFromString(new CsvReaderOptions(newLine: new[] { "\r\n" }), csvContent).ToList();

			return records
				.Where(r => r.IsValid)
				.Select(r => r.Result)
				.ToDictionary(keySelector: x => x.Key, elementSelector: x => x.Value);
		}

		private string LoadFile(string fileName)
		{
			var assembly = Assembly.GetExecutingAssembly();
			string resourceName = assembly.GetManifestResourceNames().Single(str => str.EndsWith(fileName));

			using (Stream stream = assembly.GetManifestResourceStream(resourceName))
			using (StreamReader reader = new StreamReader(stream))
			{
				string result = reader.ReadToEnd();
				return result;
			}
		}


	}
}
