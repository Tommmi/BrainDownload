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



		public LearnViewModel()
		{
			Title = "About";
			OnButtonCommand = new Command(async () => OnButton());
		}


		string htmlString = string.Empty;
		public string HtmlString
		{
			get { return htmlString; }
			set { SetProperty(ref htmlString, value); }
		}


		private void OnButton()
		{


		}



	}
}