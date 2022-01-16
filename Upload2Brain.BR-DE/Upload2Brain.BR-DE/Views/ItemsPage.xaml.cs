using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Upload2Brain.BR_DE.Models;
using Upload2Brain.BR_DE.ViewModels;
using Upload2Brain.BR_DE.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Upload2Brain.BR_DE.Views
{
	public partial class ItemsPage : ContentPage
	{
		ItemsViewModel _viewModel;

		public ItemsPage()
		{
			InitializeComponent();

			BindingContext = _viewModel = new ItemsViewModel();
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			_viewModel.OnAppearing();
		}
	}
}