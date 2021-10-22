using App1.Models;
using App1.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using App1.common;
using TinyCsvParser;
using Xamarin.Forms;

namespace App1.ViewModels
{
	public class BaseViewModel : INotifyPropertyChanged
	{
		public IDataStore<Item> DataStore => DependencyService.Get<IDataStore<Item>>();

		bool isBusy = false;
		public bool IsBusy
		{
			get { return isBusy; }
			set { SetProperty(ref isBusy, value); }
		}

		string title = string.Empty;
		public string Title
		{
			get { return title; }
			set { SetProperty(ref title, value); }
		}

		protected bool SetProperty<T>(ref T backingStore, T value,
			[CallerMemberName] string propertyName = "",
			Action onChanged = null)
		{
			if (EqualityComparer<T>.Default.Equals(backingStore, value))
				return false;

			backingStore = value;
			onChanged?.Invoke();
			OnPropertyChanged(propertyName);
			return true;
		}

		#region INotifyPropertyChanged
		public event PropertyChangedEventHandler PropertyChanged;
		protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
		{
			var changed = PropertyChanged;
			if (changed == null)
				return;

			changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
		#endregion


		protected Dictionary<string, string> LoadDicFromFile(string fileName)
		{
			string csvContent = LoadFile(fileName);

			CsvParserOptions csvParserOptions = new CsvParserOptions(true, ';');
			var csvMapper = new CsvMapping();
			var csvParser = new CsvParser<KeyValue>(csvParserOptions, csvMapper);

			var records = csvParser.ReadFromString(new CsvReaderOptions(newLine:new[]{"\r\n"}),csvContent).ToList();

			return records
				.Where(r=>r.IsValid)
				.Select(r=>r.Result)
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
