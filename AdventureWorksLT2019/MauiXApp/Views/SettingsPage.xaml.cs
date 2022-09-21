using AdventureWorksLT2019.MauiXApp.ViewModels;
using Framework.MauiX.Helpers;
namespace AdventureWorksLT2019.MauiXApp.Views;

public partial class SettingsPage : ContentPage
{
    public SettingsPage()
    {
        InitializeComponent();
        BindingContext = ServiceHelper.GetService<SettingsVM>();
    }
}

