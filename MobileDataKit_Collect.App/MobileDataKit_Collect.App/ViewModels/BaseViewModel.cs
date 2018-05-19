using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using System.Linq;
using MobileDataKit_Collect.Common;

namespace MobileDataKit_Collect.App.ViewModels
{
    public class BaseViewModel<T> : ExtendedBindableObject, IBaseViewModel
    {
        protected virtual void AddValidations()
        {

        }
        public ICommand ChangeThemeCommand => new Command(() => ExecuteChangeThemeCommand());

       

        protected virtual void ExecuteChangeThemeCommand()
        {
            CurrentTheme currentTheme = null;
            if (App.Realm.Db.All<CurrentTheme>().Count() == 0)
            {
                currentTheme = new CurrentTheme { ThemeName = "white" };
                currentTheme = App.Realm.Db.Add(currentTheme, true);
                App.Realm.Commit();
            }
            else
                currentTheme = App.Realm.Db.All<CurrentTheme>().First();

            if(currentTheme.ThemeName =="white")
            {
                App.Current.Resources["backgroundColor"] = Color.FromHex("#222327");
                App.Current.Resources["textColor"] = Color.FromHex("#FFFFFF");
                currentTheme.ThemeName = "dark";

                var cu = this.page as CustomPage;
                if(cu !=null  && cu.FormattedTitle !=null && cu.FormattedTitle.Spans !=null)
                foreach (var t in cu.FormattedTitle.Spans)
                    t.ForegroundColor = Color.Black;
            }
            else
            {
                App.Current.Resources["textColor"] = Color.FromHex("#000000");
                App.Current.Resources["backgroundColor"] = Color.FromHex("#FFFFFF");
                currentTheme.ThemeName = "white";
                var cu = this.page as CustomPage;
                if (cu != null && cu.FormattedTitle != null && cu.FormattedTitle.Spans != null)
                    foreach (var t in cu.FormattedTitle.Spans)
                        t.ForegroundColor = Color.White;
            }
               



            App.Realm.Commit();
        }
        protected virtual void InitilizeValidationObjects()
        {

        }
        private T _Model;
        public virtual T Model
        {
            get
            {
                return _Model;
            }
            set
            {
                _Model = value;
                OnPropertyChanged(nameof(BaseViewModel<T>.Model));
            }
        }






        public ICommand ValidatePropertyCommand => new Command<string>((s) => ExecuteValidateProperty(s));
        protected bool ExecuteValidateProperty(string propertyName)
        {
            try
            {
                var ttt = Csla.Reflection.MethodCaller.CallPropertyGetter(this, propertyName) as Validations.IValidity;


                if (ttt != null)
                    return ttt.Validate();
            }
            catch
            {

            }


            return false;
        }

        protected bool Validate()
        {
            var result = true;

            var deeeee = this.GetType().GetRuntimeProperties();
            foreach (var t in deeeee)
            {
                if (typeof(Validations.IValidity).GetTypeInfo().IsAssignableFrom(t.PropertyType.GetTypeInfo()))
                {
                    var rttt = Csla.Reflection.MethodCaller.CallPropertyGetter(this, t.Name) as Validations.IValidity;

                    if (rttt != null)
                    {
                        if (result)
                            result = rttt.Validate();
                        else
                            rttt.Validate();

                    }



                }

            }



            return result;
        }
        private string title = string.Empty;
        public const string TitlePropertyName = "Title";

        /// <summary>
        /// Gets or sets the "Title" property
        /// </summary>
        /// <value>The title.</value>
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        private string subtitle = string.Empty;
        /// <summary>
        /// Gets or sets the "Subtitle" property
        /// </summary>
        public const string SubtitlePropertyName = "Subtitle";
        public string Subtitle
        {
            get { return subtitle; }
            set { SetProperty(ref subtitle, value); }
        }

        private string icon = null;
        /// <summary>
        /// Gets or sets the "Icon" of the viewmodel
        /// </summary>
        public const string IconPropertyName = "Icon";
        public string Icon
        {
            get { return icon; }
            set { SetProperty(ref icon, value); }
        }
        internal INavigation Navigation { get; set; }
        //internal readonly IMeetupService meetupService;
        //internal readonly IMessageDialog messageDialog;
        internal readonly Random random;
        // internal readonly IDataService dataService;
        internal readonly Page page;

        public BaseViewModel(Page page)
        {

            this.page = page;


            Plugin.Connectivity.CrossConnectivity.Current.ConnectivityChanged += Current_ConnectivityChanged;
            if (page != null)
            {
                this.Navigation = page.Navigation;
                this.page.Appearing += Page_Appearing;
                this.page.Disappearing += Page_Disappearing;
            }

            //  meetupService = DependencyService.Get<IMeetupService>();
            //messageDialog = DependencyService.Get<IMessageDialog>();
            //dataService = DependencyService.Get<IDataService>();
            random = new Random();
            this.InitilizeValidationObjects();
            this.AddValidations();
        }


        public bool IsConnected
        {
            get
            {
                return Plugin.Connectivity.CrossConnectivity.Current.IsConnected;
            }
        }


        protected virtual void OnConnectivityChanged(Plugin.Connectivity.Abstractions.ConnectivityChangedEventArgs e)
        {

        }
        private void Current_ConnectivityChanged(object sender, Plugin.Connectivity.Abstractions.ConnectivityChangedEventArgs e)
        {

            OnConnectivityChanged(e);
            OnPropertyChanged(nameof(BaseViewModel<T>.IsConnected));
        }

        protected virtual void OnDisappearing()
        {

        }
        private void Page_Disappearing(object sender, EventArgs e)
        {


            this.OnDisappearing();
        }

        protected virtual void OnAppearing()
        {

        }






        private void Page_Appearing(object sender, EventArgs e)
        {




            this.OnAppearing();
        }

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                this.isBusy = value;
                RaisePropertyChanged(() => IsBusy);
                if (IsBusyChanged != null)
                    IsBusyChanged(isBusy);
            }
        }

        bool canLoadMore = false;
        public bool CanLoadMore
        {
            get { return canLoadMore; }
            set { SetProperty(ref canLoadMore, value); }
        }

        public Action<bool> IsBusyChanged { get; set; }

        Command loadMoreCommand;

        public ICommand LoadMoreCommand
        {
            get { return loadMoreCommand ?? (loadMoreCommand = new Command( () => ExecuteLoadMoreCommand())); }
        }

        protected virtual void ExecuteLoadMoreCommand()
        {
        }

        public Action<int> FinishedFirstLoad { get; set; }



        protected void SetProperty<T>(
            ref T backingStore, T value,
            [CallerMemberName]string propertyName = "",
            Action onChanged = null)
        {


            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return;

            backingStore = value;

            if (onChanged != null)
                onChanged();

            OnPropertyChanged(propertyName);
        }

        #region INotifyPropertyChanged implementation

        #endregion

        //public void OnPropertyChanged(string propertyName)
        //{
        //    if (PropertyChanged == null)
        //        return;

        //    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        //}

        public object GetModel()
        {
            return this.Model;
        }
    }
}
