namespace AdventureWorksLT2019.MauiXApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            // RegisterRoutes();
            BindingContext = Framework.MauiX.Helpers.ServiceHelper.GetService<AdventureWorksLT2019.MauiXApp.ViewModels.AppShellVM>();
        }

        //void RegisterRoutes()
        //{
        //    var routes = AdventureWorksLT2019.MauiXApp.Services.AppShellRoutingHelper.GetRoutes();

        //    foreach (var item in routes)
        //    {
        //        Routing.RegisterRoute(item.Key, item.Value);
        //    }
        //}
    }
}