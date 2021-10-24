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
		private static AppResource _vocabulary = new AppResource("Vocabulary.csv");
		private AppResource _labels = new AppResource("Labels.csv");

		protected Dictionary<string, string> Vocabulary => _vocabulary.Dic;
		protected Dictionary<string, string> Labels => _labels.Dic;

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

	}
}
