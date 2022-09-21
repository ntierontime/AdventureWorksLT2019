using AdventureWorksLT2019.MauiXApp.ViewModels;
using Framework.MauiX.Helpers;
namespace AdventureWorksLT2019.MauiXApp.Views;

public partial class AppLoadingPage : ContentPage
{
    public AppLoadingPage()
    {
        InitializeComponent();
        BindingContext = ServiceHelper.GetService<AppLoadingVM>();
    }
}

