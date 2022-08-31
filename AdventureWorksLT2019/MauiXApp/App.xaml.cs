namespace AdventureWorksLT2019.MauiXApp
{
    public partial class App : Application
    {
        public AdventureWorksLT2019.MauiX.ViewModels.AppVM AppVM { get; private set; }

        public App(AdventureWorksLT2019.MauiX.ViewModels.AppVM appVM)
        {
            AppVM = appVM;

            InitializeComponent();
            MainPage = new AdventureWorksLT2019.MauiX.AppShell();
        }

        protected override async void OnStart()
        {
            await AppVM.OnStart();
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