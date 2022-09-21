using AdventureWorksLT2019.MauiXApp.ViewModels;
using Framework.MauiX.Helpers;
namespace AdventureWorksLT2019.MauiXApp.Views;

public partial class FirstTimeUserPage : ContentPage
{
    public FirstTimeUserPage()
    {
        InitializeComponent();
        BindingContext = ServiceHelper.GetService<FirstTimeUserVM>();
    }
}

