namespace AdventureWorksLT2019.MauiXApp
{
    public partial class App : Application
    {
        //public AdventureWorksLT2019.MauiX.ViewModels.AppVM AppVM
        //{
        //    get
        //    {
        //        return DependencyService.Resolve<AdventureWorksLT2019.MauiX.ViewModels.AppVM>();
        //    }
        //}

        private readonly AdventureWorksLT2019.MauiX.ViewModels.AppVM _appVM;

        public App(AdventureWorksLT2019.MauiX.ViewModels.AppVM appVM)
        {
            _appVM = appVM;

            InitializeComponent();
            
            // 1. Register ViewModels
            AdventureWorksLT2019.MauiX.ViewModels.ViewModelLocator.RegisterViewModels();

            MainPage = new AdventureWorksLT2019.MauiX.Pages.AppLoadingPage();
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