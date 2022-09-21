using AdventureWorksLT2019.MauiXApp.Common.Services;
using AdventureWorksLT2019.MauiXApp.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Input;

namespace AdventureWorksLT2019.MauiXApp.ViewModels;

public class FirstTimeUserVM : ObservableObject
{
    private readonly UserService _userService;

    public FirstTimeUserVM(
        UserService userService)
    {
        _userService = userService;
    }

    public ICommand SkipCommand => new Command(OnSkip, CanSkip);

    private async void OnSkip()
    {
        await AppShellService.GoToAbsoluteAsync(nameof(MainPage));
    }
    private bool CanSkip()
    {
        return true;
    }

    public ICommand DoneCommand => new Command(OnDone, CanDone);

    private async void OnDone()
    {
        await _userService.SetUserProfileCompletedAsync();
        await AppShellService.GoToAbsoluteAsync(nameof(MainPage));
    }
    private bool CanDone()
    {
        return true;
    }
}

