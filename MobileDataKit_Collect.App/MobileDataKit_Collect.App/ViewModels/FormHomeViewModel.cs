using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MobileDataKit_Collect.App.ViewModels
{
    public class FormHomeViewModel : BaseViewModel<object>
    {
        public FormHomeViewModel(Page page) : base(page)
        {

        }


        public ICommand FillBlankFormCommand => new Command(async () => await ExecuteFillBlankFormAsync());


        private async System.Threading.Tasks.Task ExecuteFillBlankFormAsync()
        {

            await this.Navigation.PushAsync(new Views.EntryCanvas());
        }


    }
}
