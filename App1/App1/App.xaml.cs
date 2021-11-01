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

			DependencyService.Register<ISystemTime, SystemTime>();
			DependencyService.RegisterSingleton(new SystemTime());
			DependencyService.Register<IUserRepository, UserRepository>();
			DependencyService.RegisterSingleton(new UserRepository());
			DependencyService.Register<ILearnService, LearnService>();
			DependencyService.RegisterSingleton(new LearnService(DependencyService.Get<ISystemTime>(), DependencyService.Get<IUserRepository>()));
			DependencyService.RegisterSingleton(new LabelRepository());
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
