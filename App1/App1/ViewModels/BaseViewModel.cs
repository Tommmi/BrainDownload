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
		#region fields

		private static object _sync = new object();
		private static Dictionary<Guid, (string left, string right)> _vocabulary;
		private Dictionary<string, string> _labels;
		bool isBusy = false;

		string title = string.Empty;

		#endregion

		#region properties

		protected AppStorage UserData { get; } = new AppStorage(); 

		protected Dictionary<Guid, (string left, string right)> Vocabulary => GetVocabulary();

		public IDataStore<Item> DataStore => DependencyService.Get<IDataStore<Item>>();

		public string Title
		{
			get { return title; }
			set { SetProperty(ref title, value); }
		}

		public bool IsBusy
		{
			get { return isBusy; }
			set { SetProperty(ref isBusy, value); }
		}

		protected Dictionary<string, string> Labels => GetLabels();

		#endregion

		#region private methods

		private Dictionary<Guid, (string left, string right)> GetVocabulary()
		{
			if (_vocabulary == null)
			{
				lock (_sync)
				{
					if (_vocabulary == null)
					{
						var dic = new AppResource("Vocabulary.csv").Dic;
						_vocabulary = dic.ToDictionary(keySelector: x => Guid.Parse(x.Key),
							elementSelector: x => (left: x.Value[0], right: x.Value[1]));
					}
				}
			}

			return _vocabulary;
		}

		private Dictionary<string, string> GetLabels()
		{
			if (_labels == null)
			{
				lock (_sync)
				{
					if (_labels == null)
					{
						var dic = new AppResource("Labels.csv").Dic;
						_labels = dic.ToDictionary(keySelector: x => x.Value[0],
							elementSelector: x => x.Value[1]);
					}
				}
			}

			return _labels;
		}

		#endregion

		#region protected

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

		#endregion

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

	}
}
