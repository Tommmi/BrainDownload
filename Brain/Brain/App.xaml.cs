using DownloadToBrain.Services;
using DownloadToBrain.Views;
using System;
using Brain.Interfaces;
using Brain.Services;
using DownloadToBrain.Infrastructure;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DownloadToBrain
{
	public partial class App : Application
	{

		public App()
		{
			InitializeComponent();

			DependencyService.RegisterSingleton< ISystemTime>(new SystemTime());
			DependencyService.RegisterSingleton<IUserRepository>(new UserRepository());
			DependencyService.RegisterSingleton<ILearnService>(new LearnService(DependencyService.Get<ISystemTime>(), DependencyService.Get<IUserRepository>()));
			DependencyService.RegisterSingleton<LabelRepository>(new LabelRepository());
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
