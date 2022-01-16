using System;
using System.Collections.Generic;
using Upload2Brain.BR_DE.ViewModels;
using Upload2Brain.BR_DE.Views;
using Xamarin.Forms;

namespace Upload2Brain.BR_DE
{
	public partial class AppShell : Xamarin.Forms.Shell
	{
		public AppShell()
		{
			InitializeComponent();
			Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
			Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
		}

		private async void OnMenuItemClicked(object sender, EventArgs e)
		{
			await Shell.Current.GoToAsync("//LoginPage");
		}
	}
}
