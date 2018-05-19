using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MobileDataKit_Collect.App.ViewModels
{
    class WelcomeViewModel:BaseViewModel<Model.LoginResult>
    {

        public WelcomeViewModel(Page page) : base(page)
        {


        }



        public ICommand RegisterCommand => new Command(async () => await ExecuteRegisterAsync());

        private async Task ExecuteRegisterAsync()
        {
            await this.Navigation.PushAsync(new Views.Register());

        }



        public ICommand LoginCommand => new Command(async () => await ExecuteLoginAsync());

        private async Task ExecuteLoginAsync()
        {
            await this.Navigation.PushAsync(new Views.Login());

        }
    }
}
