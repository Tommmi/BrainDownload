using System.ComponentModel;
using Upload2Brain.BR_DE.ViewModels;
using Xamarin.Forms;

namespace Upload2Brain.BR_DE.Views
{
	public partial class ItemDetailPage : ContentPage
	{
		public ItemDetailPage()
		{
			InitializeComponent();
			BindingContext = new ItemDetailViewModel();
		}
	}
}