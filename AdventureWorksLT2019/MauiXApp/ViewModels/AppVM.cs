using AdventureWorksLT2019.MauiXApp.Common.Services;
using Framework.MauiX.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Input;

namespace AdventureWorksLT2019.MauiXApp.ViewModels;

public class AppVM : ObservableObject
{
    public bool HasAuthentication { get; set; }

    protected bool m_ShellNavBarIsVisible;
    public bool ShellNavBarIsVisible
    {
        get => m_ShellNavBarIsVisible;
        set => SetProperty(ref m_ShellNavBarIsVisible, value);
    }

    private Microsoft.Spatial.GeographyPoint m_CurrentLocation;
    public Microsoft.Spatial.GeographyPoint CurrentLocation
    {
        get => m_CurrentLocation;
        set => SetProperty(ref m_CurrentLocation, value);
    }

    private readonly SecureStorageService _secureStorageService;
    private readonly AuthenticationService _authenticationService;

    public AppVM(
        SecureStorageService secureStorageService,
        AuthenticationService authenticationService
        )
    {
        _secureStorageService = secureStorageService;
        _authenticationService = authenticationService;
    }
}

