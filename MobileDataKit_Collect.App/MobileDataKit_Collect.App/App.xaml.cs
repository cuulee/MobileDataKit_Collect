using System;
using System.Linq;
using Xamarin.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace MobileDataKit_Collect.App
{
	public partial class App : Application
	{
        public static string AppName { get; set; } = "MobileDataKitApp";
        public static string Token
        {
            get
            {

                var account = AccountStore.Create().FindAccountsForService(App.AppName).FirstOrDefault();
                return (account != null) ? account.Properties["Token"] : null;
            }
            set
            {

                Account account = new Account
                {
                    Username = UserName
                };
                account.Properties.Add("Token", value);
                AccountStore.Create().Save(account, App.AppName);


            }

        }



        public static string UserName
        {
            get
            {

                var account = AccountStore.Create().FindAccountsForService(App.AppName).FirstOrDefault();
                return (account != null) ? account.Username : null;

            }
        }
        public static ContentPage TempRoot;
        public static Model.RealmWrapper _realm = null;
        public static Model.RealmWrapper Realm

        {
            get
            {
                if (_realm == null)
                    _realm = new Model.RealmWrapper();
                return _realm;
            }

        }
        public App ()
		{
			InitializeComponent();

           

            var t = new MobileDataKit_Collect.Common.FormElement();
            var realm = Realms.Realm.GetInstance(new Realms.RealmConfiguration {SchemaVersion=3 });
           

            if (realm.All<MobileDataKit_Collect.Common.Project>().Count()==0)
            {
                var ttt = System.IO.File.ReadAllBytes(@"Assets\CRF_01.xls");
                var pro = (new MobileDataKit_Collect.Common.FormElementFactory()).Create(ttt);

                using (var tr = realm.BeginWrite())
                {

                    realm.Add(pro, true);


                    tr.Commit();
                }

            }

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
