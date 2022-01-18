using System;
using System.Collections.Generic;
using System.ComponentModel;
using Upload2Brain.BR_DE.Models;
using Upload2Brain.BR_DE.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Upload2Brain.BR_DE.Views
{
	public partial class NewItemPage : ContentPage
	{
		public Item Item { get; set; }

		public NewItemPage()
		{
			InitializeComponent();
			BindingContext = new NewItemViewModel();
		}
	}
}