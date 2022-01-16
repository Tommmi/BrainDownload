using System;
using Upload2Brain.BR_DE.Services;
using Upload2Brain.BR_DE.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Upload2Brain.BR_DE
{
	public partial class App : Application
	{

		public App()
		{
			InitializeComponent();

			DependencyService.Register<MockDataStore>();
			MainPage = new AppShell();
		}

		protected override void OnStart()
		{
		}

		protected override void OnSleep()
		{
		}

		protected override void OnResume()
		{
		}
	}
}
