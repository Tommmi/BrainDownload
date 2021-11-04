using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Input;
using App1.common;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace App1.ViewModels
{
	public class LearnViewModel : BaseViewModel
	{
		private int _count = 0;

		public LearnViewModel()
		{
			HtmlString = "8786234";
			OnNextBttnCmd = new Command(async () => OnNextBttn());
		}


		private string htmlString = "";

		public string HtmlString
		{
			get { return htmlString; }
			set { SetProperty(ref htmlString, value); }
		}

		#region Button Next

		public string NextBttnText => Labels[LabelKeyEnum.Next.ToString()];

		private bool _isNextBttnEnabled = false;
		private bool _isNextBttnVisible = false;
		public ICommand OnNextBttnCmd { get; }

		public bool IsNextBttnEnabled
		{
			get { return _isNextBttnEnabled; }
			set { SetProperty(ref _isNextBttnEnabled, value); }
		}

		public bool IsNextBttnVisible
		{
			get { return _isNextBttnVisible; }
			set { SetProperty(ref _isNextBttnVisible, value); }
		}

		private void OnNextBttn()
		{
			HtmlString = string.Format(Labels["htmlString"], _count++);
		}

		#endregion

		#region Button WellKnown

		public string WellKnownBttnText => Labels[LabelKeyEnum.WellKnown.ToString()];

		private bool _isWellKnownBttnEnabled = true;
		private bool _isWellKnownBttnVisible = true;
		public ICommand OnWellKnownBttnCmd { get; }

		public bool IsWellKnownBttnEnabled
		{
			get { return _isWellKnownBttnEnabled; }
			set { SetProperty(ref _isWellKnownBttnEnabled, value); }
		}

		public bool IsWellKnownBttnVisible
		{
			get { return _isWellKnownBttnVisible; }
			set { SetProperty(ref _isWellKnownBttnVisible, value); }
		}

		private void OnWellKnownBttn()
		{
		}

		#endregion

		#region Button Failed

		public string FailedBttnText => Labels[LabelKeyEnum.Failed.ToString()];

		private bool _isFailedBttnEnabled = true;
		private bool _isFailedBttnVisible = true;
		public ICommand OnFailedBttnCmd { get; }

		public bool IsFailedBttnEnabled
		{
			get { return _isFailedBttnEnabled; }
			set { SetProperty(ref _isFailedBttnEnabled, value); }
		}

		public bool IsFailedBttnVisible
		{
			get { return _isFailedBttnVisible; }
			set { SetProperty(ref _isFailedBttnVisible, value); }
		}

		private void OnFailedBttn()
		{
		}

		#endregion

		#region Button MoreOrLess

		public string MoreOrLessBttnText => Labels[LabelKeyEnum.MoreOrLess.ToString()];

		private bool _isMoreOrLessBttnEnabled = true;
		private bool _isMoreOrLessBttnVisible = true;
		public ICommand OnMoreOrLessBttnCmd { get; }

		public bool IsMoreOrLessBttnEnabled
		{
			get { return _isMoreOrLessBttnEnabled; }
			set { SetProperty(ref _isMoreOrLessBttnEnabled, value); }
		}

		public bool IsMoreOrLessBttnVisible
		{
			get { return _isMoreOrLessBttnVisible; }
			set { SetProperty(ref _isMoreOrLessBttnVisible, value); }
		}

		private void OnMoreOrLessBttn()
		{
		}

		#endregion

	}
}