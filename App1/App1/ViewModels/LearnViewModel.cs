using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace App1.ViewModels
{
	public class LearnViewModel : BaseViewModel
	{
		public ICommand OnButtonCommand { get; }
		private int _count = 0;

		public LearnViewModel()
		{
			HtmlString = "8786234";
			OnButtonCommand = new Command(async () => OnButton());
		}


		private string htmlString = "";

		public string HtmlString
		{
			get { return htmlString; }
			set { SetProperty(ref htmlString, value); }
		}

		private void OnButton()
		{
			HtmlString = string.Format(Labels["htmlString"], _count++);
		}
	}
}