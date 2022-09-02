namespace AdventureWorksLT2019.MauiXApp
{
    public partial class App : Application
    {
        protected AdventureWorksLT2019.MauiXApp.ViewModels.AppVM _appVM
        {
            get
            {
                return Framework.MauiX.Helpers.ServiceHelper.GetService<AdventureWorksLT2019.MauiXApp.ViewModels.AppVM>();
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new AdventureWorksLT2019.MauiXApp.Pages.AppLoadingPage();
        }

        protected override async void OnStart()
        {
            await _appVM.OnStart();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}