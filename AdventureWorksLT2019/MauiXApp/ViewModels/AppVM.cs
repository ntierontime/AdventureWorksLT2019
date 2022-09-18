using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Input;

namespace AdventureWorksLT2019.MauiXApp.ViewModels;

public class AppVM : ObservableObject
{
    public bool HasAuthentication { get; set; }

    //protected Framework.MauiX.Models.AppStates m_AppState = Framework.MauiX.Models.AppStates.Launching;
    //public Framework.MauiX.Models.AppStates AppState
    //{
    //    get => m_AppState;
    //    set => SetProperty(ref m_AppState, value);
    //}

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

    private readonly Framework.MauiX.Services.SecureStorageService _secureStorageService;
    private readonly AdventureWorksLT2019.MauiXApp.Common.Services.AuthenticationService _authenticationService;

    public AppVM(
        Framework.MauiX.Services.SecureStorageService secureStorageService,
        AdventureWorksLT2019.MauiXApp.Common.Services.AuthenticationService authenticationService
        )
    {
        _secureStorageService = secureStorageService;
        _authenticationService = authenticationService;
    }

}

