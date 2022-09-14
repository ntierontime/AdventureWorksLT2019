using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Input;

namespace AdventureWorksLT2019.MauiXApp.ViewModels;

public class FirstTimeUserVM : ObservableObject
{
    private readonly AdventureWorksLT2019.MauiXApp.Common.Services.UserService _userService;

    public FirstTimeUserVM(
        AdventureWorksLT2019.MauiXApp.Common.Services.UserService userService)
    {
        _userService = userService;
    }

    public ICommand SkipCommand => new Command(OnSkip, CanSkip);

    private async void OnSkip()
    {
        await Common.Services.AppShellService.GoToAbsoluteAsync(nameof(AdventureWorksLT2019.MauiXApp.Views.MainPage));
    }
    private bool CanSkip()
    {
        return true;
    }

    public ICommand DoneCommand => new Command(OnDone, CanDone);

    private async void OnDone()
    {
        await _userService.SetUserProfileCompletedAsync();
        await Common.Services.AppShellService.GoToAbsoluteAsync(nameof(AdventureWorksLT2019.MauiXApp.Views.MainPage));
    }
    private bool CanDone()
    {
        return true;
    }
}
