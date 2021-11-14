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
using App1.Infrastructure;
using Brain.Entities;
using Brain.Interfaces;
using TinyCsvParser;
using Xamarin.Forms;

namespace App1.ViewModels
{
	public class BaseViewModel : INotifyPropertyChanged
	{
		#region fields


		#endregion

		#region properties

		public IDictionary<string, string> Labels => DependencyService.Get<LabelRepository>().GetLabels();

		public string Title => Labels["LearnPageTitle"];

		public ILearnService LearnService => DependencyService.Get<ILearnService>();

		#endregion

		#region private methods

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
