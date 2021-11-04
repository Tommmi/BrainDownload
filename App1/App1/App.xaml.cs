using App1.Services;
using App1.Views;
using System;
using App1.Infrastructure;
using Brain.Interfaces;
using Brain.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1
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
			DependencyService.Get<ILearnService>().Initialize().Wait();
		}

		protected override void OnSleep()
		{
		}

		protected override void OnResume()
		{
		}
	}
}
