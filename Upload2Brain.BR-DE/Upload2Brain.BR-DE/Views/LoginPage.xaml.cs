using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Upload2Brain.BR_DE.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Upload2Brain.BR_DE.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
		public LoginPage()
		{
			InitializeComponent();
			this.BindingContext = new LoginViewModel();
		}
	}
}