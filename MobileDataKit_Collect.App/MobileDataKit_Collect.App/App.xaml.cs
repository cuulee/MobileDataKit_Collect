using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace MobileDataKit_Collect.App
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();

            var ttt = System.IO.File.ReadAllBytes(@"Assets\CRF_01.xls");

            var t = new MobileDataKit_Collect.Common.FormElement();
            var realm = Realms.Realm.GetInstance();




			MainPage = new NavigationPage( new Views.FormHome());
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
