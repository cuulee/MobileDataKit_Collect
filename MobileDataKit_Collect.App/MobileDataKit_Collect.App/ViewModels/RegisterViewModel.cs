using MobileDataKit_Collect.App.Validations;
using MobileDataKit_Collect.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MobileDataKit_Collect.App.ViewModels
{
    public class RegisterViewModel :  AuthenticationBaseViewModel<Register>
    {

        public RegisterViewModel(Page page) : base(page)
        {
            this.Model = new Register();
            
        }



        public ICommand ValidateConfirmPasswordCommand => new Command(() => ExecuteValidateProperty(nameof(RegisterViewModel.ConfirmPassword)));



        public ICommand ValidatePrimarySchoolCommand => new Command(() => ExecuteValidateProperty(nameof(RegisterViewModel.PrimarySchool)));




        private Validations.ValidatableObject<string> _PrimarySchool;
        private Validations.ValidatableObject<string> _ConfirmPassword;



        protected override void AddValidations()
        {
            base.AddValidations();

            _Password.Validations.Add(new CharactersGreaterThanValidator<string> { ValidationMessage = "Password should have more than 4 characters", NoCharact = 4 });
            _ConfirmPassword.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Confirm Password" });
            _ConfirmPassword.Validations.Add(new ComparePropertiesValidation<string> { ValidationMessage = "Passwords are not equal", SecondProperty = nameof(Register.Password) });
            _PrimarySchool.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Answer primary school" });
        }

        protected override void InitilizeValidationObjects()
        {
            base.InitilizeValidationObjects();

            _PrimarySchool = new Validations.ValidatableObject<string>(this.page, nameof(Register.PrimarySchool));
            _ConfirmPassword = new Validations.ValidatableObject<string>(this.page, nameof(Register.ConfirmPassword));
        }




        public ValidatableObject<string> ConfirmPassword
        {
            get
            {
                return _ConfirmPassword;
            }
            set
            {
                _ConfirmPassword = value;
                RaisePropertyChanged(() => ConfirmPassword);
            }
        }






        public ValidatableObject<string> PrimarySchool
        {
            get
            {
                return _PrimarySchool;
            }
            set
            {
                _PrimarySchool = value;
                RaisePropertyChanged(() => PrimarySchool);
            }
        }






        public ICommand RegisterCommand => new Command(async () => await ExecuteRegisterAsync());


        private System.Threading.Tasks.Task ExecuteRegisterAsync()
        {

            //if (!Validate())
            //    return Task.CompletedTask;

            IsBusy = true;
            var message = new HttpService.StartHttpMessage();

            message.HttpMessageMethod = HttpService.HttpMessageMethod.POST;

            message.Data = this.Model;
            message.Url = "api/Register";


            MessagingCenter.Subscribe<HttpService.HttpMessage>(this, message.ID.ToString(), async message1 => {

                MessagingCenter.Unsubscribe<HttpService.HttpMessage>(this, message1.ID.ToString());

                var sdsds = (Register)message1.Data;
                if (!string.IsNullOrWhiteSpace(sdsds.Description))
                {
                    this.IsBusy = false;
                    this.Model.UserName = string.Empty;
                    this.Password.Errors.Add(sdsds.Description);

                }
                else
                {
                    LoginCommand.Execute(null);
                }




            });
            MessagingCenter.Send(message, "StartHttpMessage");


            return Task.CompletedTask;


        }



    }
}
