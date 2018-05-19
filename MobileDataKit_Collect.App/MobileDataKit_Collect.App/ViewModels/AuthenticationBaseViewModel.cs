using MobileDataKit_Collect.App.Validations;
using MobileDataKit_Collect.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Auth;
using Xamarin.Forms;

namespace MobileDataKit_Collect.App.ViewModels
{
    public class AuthenticationBaseViewModel<T> : BaseViewModel<T>
    {

        public ICommand ValidateUserNameCommand => new Command(() => ExecuteValidateProperty(nameof(AuthenticationBaseViewModel<T>.UserName)));

        public ICommand ValidatePasswordCommand => new Command(() => ExecuteValidateProperty(nameof(AuthenticationBaseViewModel<T>.Password)));







        protected Validations.ValidatableObject<string> _userName;
        protected Validations.ValidatableObject<string> _Password;




        protected override void AddValidations()
        {
            base.AddValidations();
            _userName.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Enter your username" });
            _Password.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Enter Password" });
        }

        protected override void InitilizeValidationObjects()
        {
            base.InitilizeValidationObjects();
            _userName = new Validations.ValidatableObject<string>(this.page, nameof(Register.UserName));
            _Password = new Validations.ValidatableObject<string>(this.page, nameof(Register.Password));
        }

        public ValidatableObject<string> UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = value;
                RaisePropertyChanged(() => UserName);
            }
        }





        public ValidatableObject<string> Password
        {
            get
            {
                return _Password;
            }
            set
            {
                _Password = value;
                RaisePropertyChanged(() => Password);
            }
        }





        public AuthenticationBaseViewModel(Page page) : base(page)
        {
            this.Model = Activator.CreateInstance<T>();
            Csla.Reflection.MethodCaller.CallPropertySetter(this.Model, "IMEI", Plugin.DeviceInfo.CrossDeviceInfo.Current.Id);
        }


        protected void SaveCredentials(string userName, string token)
        {
            if (!string.IsNullOrWhiteSpace(userName))
            {
                Account account = new Account
                {
                    Username = userName
                };
                account.Properties.Add("Token", token);
                AccountStore.Create().Save(account, App.AppName);
            }
        }




        public ICommand LoginCommand => new Command(async () => await ExecuteLoginAsync());


        private async System.Threading.Tasks.Task ExecuteLoginAsync()
        {
            this.Password.Errors.Clear();

            //if (!Validate())
            //    return;
            this.IsBusy = true;
            Model.LoginResult response = null;

            var client2 = MobileDataKit_Collect.App.Model.SecureHttpClient.HttpClient;

            var req = new System.Net.Http.HttpRequestMessage(System.Net.Http.HttpMethod.Post, "connect/token");





            var prms = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("grant_type", "password"),
                        new KeyValuePair<string, string>("username", Csla.Reflection.MethodCaller.CallPropertyGetter( this.Model,"UserName").ToString()),
                        new KeyValuePair<string, string>("password", Csla.Reflection.MethodCaller.CallPropertyGetter( this.Model,"Password").ToString())

                    };

            req.Content = new System.Net.Http.FormUrlEncodedContent(prms);
            var response2 = await client2.SendAsync(req);
            var ttt = await response2.Content.ReadAsStringAsync();



            response = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.LoginResult>(ttt);

            if (response != null && !string.IsNullOrWhiteSpace(response.access_token))
            {








                SaveCredentials(Csla.Reflection.MethodCaller.CallPropertyGetter(this.Model, "UserName").ToString(), response.access_token);
                this.Navigation.InsertPageBefore(new Views.FormHome(), App.TempRoot);
                App.TempRoot = null;
                await this.Navigation.PopToRootAsync();



            }
            else if (!string.IsNullOrWhiteSpace(response.error_description))
            {
                this.IsBusy = false;
                this.Password.Errors.Add("userName or Password incorrect");
                RaisePropertyChanged(() => Password);


            }












        }


    }
}