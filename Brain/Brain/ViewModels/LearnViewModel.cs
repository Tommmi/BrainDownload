using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Input;
using DownloadToBrain.common;
using Brain.Entities;
using Brain.Entities.UserStatus;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DownloadToBrain.ViewModels
{
	public class LearnViewModel : BaseViewModel
	{
		#region types

		private enum State
		{
			Initial,
			NoWordsMore,
			LeftShowing,
			RightShowing
		}

		#endregion

		#region fields

		private string _htmlLeft = "";
		private string _htmlRight = "";
		private int _wordsInLongMemory = 0;
		private int _wordsInShortMemory = 0;
		private string _wordsInLongMemoryLabel = "";
		private string _wordsInShortMemoryLabel = "";
		
		private State _state = State.Initial;
		private int _count = 0;
		private NextWordResult _nextWordResult;

		#endregion

		#region Constructor

		public LearnViewModel()
		{
			OnNextBttnCmd = new Command(async () => OnNextBttn());
			OnMoreOrLessBttnCmd = new Command(async () => OnMoreOrLessBttn());
			OnFailedBttnCmd = new Command(async () => OnFailedBttn());
			OnWellKnownBttnCmd = new Command(async () => OnWellKnownBttn());
			SetThreeBttnState(isWellKnownBttnVisible:false);
		}

		#endregion

		#region private methods


		private void SetNextWord()
		{
			_nextWordResult = LearnService.TryGetNextVocable().Result;

			if (_nextWordResult.Succeeded)
			{
				HtmlLeft = _nextWordResult.Vocable.Left.ReplaceLineFeeds();
				_state = State.LeftShowing;
			}
			else
			{
				HtmlLeft = Labels["noWordsLeft"];
				_state = State.NoWordsMore;
			}

			HtmlRight = "";

			SetThreeBttnState(isWellKnownBttnVisible: false);

			var progress = LearnService.GetProgress().Result;
			WordsInLongMemory = progress.WordsInLongMemory.ToInt();
			WordsInShortMemory = progress.WordsInShortMemory.ToInt();
			WordsInLongMemoryLabel = Labels["WordsInLongMemoryLabel"];
			WordsInShortMemoryLabel = Labels["WordsInShortMemoryLabel"];
		}

		private void SetThreeBttnState(bool isWellKnownBttnVisible)
		{
			IsFailedBttnEnabled = isWellKnownBttnVisible;
			IsFailedBttnVisible = isWellKnownBttnVisible;
			IsMoreOrLessBttnEnabled = isWellKnownBttnVisible;
			IsMoreOrLessBttnVisible = isWellKnownBttnVisible;
			IsWellKnownBttnEnabled = isWellKnownBttnVisible;
			IsWellKnownBttnVisible = isWellKnownBttnVisible;
			IsNextBttnEnabled = !isWellKnownBttnVisible;
			IsNextBttnVisible = !isWellKnownBttnVisible;
			IsHtmlRightVisible = isWellKnownBttnVisible;
		}

		#endregion

		#region WordsInLongMemory

		public int WordsInLongMemory
		{
			get { return _wordsInLongMemory; }
			set { SetProperty(ref _wordsInLongMemory, value); }
		}

		public string WordsInLongMemoryLabel
		{
			get { return _wordsInLongMemoryLabel; }
			set { SetProperty(ref _wordsInLongMemoryLabel, value); }
		}

		#endregion

		#region WordsInShortMemory

		public int WordsInShortMemory
		{
			get { return _wordsInShortMemory; }
			set { SetProperty(ref _wordsInShortMemory, value); }
		}

		public string WordsInShortMemoryLabel
		{
			get { return _wordsInShortMemoryLabel; }
			set { SetProperty(ref _wordsInShortMemoryLabel, value); }
		}
		#endregion

		#region HtmlLeft

		public string HtmlLeft
		{
			get { return _htmlLeft; }
			set { SetProperty(ref _htmlLeft, value); }
		}

		#endregion

		#region HtmlRight

		private bool _isHtmlRightVisible = false;

		public string HtmlRight
		{
			get { return _htmlRight; }
			set { SetProperty(ref _htmlRight, value); }
		}

		public bool IsHtmlRightVisible
		{
			get { return _isHtmlRightVisible; }
			set { SetProperty(ref _isHtmlRightVisible, value); }
		}

		#endregion

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
			switch (_state)
			{
				case State.Initial:
				case State.NoWordsMore:
					SetNextWord();
					break;
				case State.LeftShowing:
					HtmlRight = _nextWordResult.Vocable.Right.ReplaceLineFeeds();
					SetThreeBttnState(isWellKnownBttnVisible:true);
					_state = State.RightShowing;
					break;
				case State.RightShowing:
				default:
					throw new ArgumentOutOfRangeException();
			}
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
			switch (_state)
			{
				case State.RightShowing:
					LearnService.SetTrainResult(wordId: _nextWordResult.Vocable.Id, TrainResult.WellKnown).Wait();
					SetNextWord();
					break;
				case State.Initial:
				case State.NoWordsMore:
				case State.LeftShowing:
				default:
					throw new ArgumentOutOfRangeException();
			}
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
			switch (_state)
			{
				case State.RightShowing:
					LearnService.SetTrainResult(wordId: _nextWordResult.Vocable.Id, TrainResult.Failed).Wait();
					SetNextWord();
					break;
				case State.Initial:
				case State.NoWordsMore:
				case State.LeftShowing:
				default:
					throw new ArgumentOutOfRangeException();
			}
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
			switch (_state)
			{
				case State.RightShowing:
					LearnService.SetTrainResult(wordId: _nextWordResult.Vocable.Id, TrainResult.MoreOrLess).Wait();
					SetNextWord();
					break;
				case State.Initial:
				case State.NoWordsMore:
				case State.LeftShowing:
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		#endregion

	}
}