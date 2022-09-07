using AdventureWorksLT2019.MauiXApp.Pages;
using Microsoft.Maui.Controls;

namespace AdventureWorksLT2019.MauiXApp.Services
{
    public class AppShellService
    {
        private readonly AdventureWorksLT2019.MauiXApp.Services.UserService _userService;

        public AppShellService(
            AdventureWorksLT2019.MauiXApp.Services.UserService userService)
        {
            _userService = userService;
        }

        public async Task AddFlyoutMenusDetails(bool gotoFirstTimeUserPage)
        {
            string gotoRoute = string.Empty;
            // 1. Main Page
            {
                var route = nameof(AdventureWorksLT2019.MauiXApp.Pages.MainPage);
                var pageType = typeof(AdventureWorksLT2019.MauiXApp.Pages.MainPage);
                var thePage = AppShell.Current.Items.Where(f => f.Route == route).FirstOrDefault();
                if (thePage != null) AppShell.Current.Items.Remove(thePage);

                var flyoutItem = new FlyoutItem()
                {
                    Title = AdventureWorksLT2019.Resx.Resources.UIStrings.Home,
                    Route = route,
                    FlyoutDisplayOptions = FlyoutDisplayOptions.AsMultipleItems,
                    Items =
                    {
                        new ShellContent
                        {
                            Title = AdventureWorksLT2019.Resx.Resources.UIStrings.Home,
                            ContentTemplate = new DataTemplate(pageType),
                        },
                    }
                };
                if (!AppShell.Current.Items.Contains(flyoutItem))
                {
                    AppShell.Current.Items.Add(flyoutItem);
                }

                if (!gotoFirstTimeUserPage)
                {
                    gotoRoute = route;
                }
            }

            // 2. FirstTimeUserPage
            {
                var route = nameof(AdventureWorksLT2019.MauiXApp.Pages.FirstTimeUserPage);
                var pageType = typeof(AdventureWorksLT2019.MauiXApp.Pages.FirstTimeUserPage);
                var thePage = AppShell.Current.Items.Where(f => f.Route == route).FirstOrDefault();
                if (thePage != null) AppShell.Current.Items.Remove(thePage);

                var flyoutItem = new FlyoutItem()
                {
                    Title = AdventureWorksLT2019.Resx.Resources.UIStrings.FirstTimeUser,
                    Route = route,
                    FlyoutDisplayOptions = FlyoutDisplayOptions.AsMultipleItems,
                    Items =
                    {
                        new ShellContent
                        {
                            Title = AdventureWorksLT2019.Resx.Resources.UIStrings.FirstTimeUser,
                            ContentTemplate = new DataTemplate(pageType),
                        },
                    }
                };
                if (!AppShell.Current.Items.Contains(flyoutItem))
                {
                    AppShell.Current.Items.Add(flyoutItem);
                }

                if (gotoFirstTimeUserPage)
                {
                    gotoRoute = route;
                }
            }

            var logoutMenuItem = new MenuItem
            {
                Text = AdventureWorksLT2019.Resx.Resources.UIStrings.LogOut,
                Command = new Command(async ()=> {
                    var succeeded = await _userService.LogOutAsync();
                    if (succeeded)
                    {
                        await GoToAbsoluteAsync(nameof(AdventureWorksLT2019.MauiXApp.Pages.LogInPage));
                    }
                })

            };
            AppShell.Current.Items.Add(logoutMenuItem);

            if (!string.IsNullOrEmpty(gotoRoute))
            {
                await GoToAbsoluteAsync(gotoRoute);
            }
        }

        public static async Task GoToAbsoluteAsync(string route)
        {
            if (DeviceInfo.Platform == DevicePlatform.WinUI)
            {
                AppShell.Current.Dispatcher.Dispatch(async () =>
                {
                    await Shell.Current.GoToAsync($"//{route}");
                });
            }
            else
            {
                await Shell.Current.GoToAsync($"//{route}");
            }
        }

        public static async Task GoToRelativeAsync(string route)
        {
            try
            {
                if (DeviceInfo.Platform == DevicePlatform.WinUI)
                {
                    AppShell.Current.Dispatcher.Dispatch(async () =>
                    {
                        await Shell.Current.GoToAsync(route);
                    });
                }
                else
                {
                    await Shell.Current.GoToAsync(route);
                }
            }
            catch
            {
                await GoToAbsoluteAsync(route);
            }
        }
    }
}
