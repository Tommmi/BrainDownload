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

		private Dictionary<string, string> _vocabulary;
		private Dictionary<string, string> _resourceTexts;
		private static object _sync = new object();

		private Dictionary<string, string> Vocabulary => GetDictionary(ref _vocabulary, "Vocabulary.csv");
		private Dictionary<string, string> Labels => GetDictionary(ref _vocabulary, "Labels.csv");

		private Dictionary<string, string> GetDictionary(ref Dictionary<string, string> dic, string scvFileName)
		{
			if (dic != null)
			{
				return dic;
			}

			lock (_sync)
			{
				if (dic == null)
				{
					dic = LoadDicFromFile(scvFileName);
				}
			}

			return dic;
		}

		public LearnViewModel()
		{
			Title = "About";
			HtmlString = Vocabulary["5"];
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