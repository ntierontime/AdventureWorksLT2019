﻿using AdventureWorksLT2019.MauiXApp.Pages.Common;
using Microsoft.Maui.Controls;

namespace AdventureWorksLT2019.MauiXApp.Services.Common
{
    public class AppShellService
    {
        private readonly AdventureWorksLT2019.MauiXApp.Services.Common.UserService _userService;

        public AppShellService(
            AdventureWorksLT2019.MauiXApp.Services.Common.UserService userService)
        {
            _userService = userService;
        }

        public async Task AddFlyoutMenusDetails(bool gotoFirstTimeUserPage)
        {
            string gotoRoute = string.Empty;
            // 1. Main Page
            {
                var route = nameof(AdventureWorksLT2019.MauiXApp.Pages.Common.MainPage);
                AddFlyoutItem(
                    route,
                    typeof(AdventureWorksLT2019.MauiXApp.Pages.Common.MainPage),
                    AdventureWorksLT2019.Resx.Resources.UIStrings.Home);

                if (!gotoFirstTimeUserPage)
                {
                    gotoRoute = route;
                }
            }

            // 2. FirstTimeUserPage
            {
                var route = nameof(AdventureWorksLT2019.MauiXApp.Pages.Common.FirstTimeUserPage);
                AddFlyoutItem(
                    route,
                    typeof(AdventureWorksLT2019.MauiXApp.Pages.Common.FirstTimeUserPage),
                    AdventureWorksLT2019.Resx.Resources.UIStrings.FirstTimeUser);

                if (gotoFirstTimeUserPage)
                {
                    gotoRoute = route;
                }
            }
            
            // SettingsPage
            AddFlyoutItem(
                nameof(AdventureWorksLT2019.MauiXApp.Pages.CustomerListPage),
                typeof(AdventureWorksLT2019.MauiXApp.Pages.CustomerListPage),
                AdventureWorksLT2019.Resx.Resources.UIStrings.Customer);

            // SettingsPage
            AddFlyoutItem(
                nameof(AdventureWorksLT2019.MauiXApp.Pages.Common.SettingsPage),
                typeof(AdventureWorksLT2019.MauiXApp.Pages.Common.SettingsPage),
                AdventureWorksLT2019.Resx.Resources.UIStrings.Settings);

            var logoutMenuItem = new MenuItem
            {
                Text = AdventureWorksLT2019.Resx.Resources.UIStrings.LogOut,
                Command = new Command(async () =>
                {
                    var succeeded = await _userService.LogOutAsync();
                    if (succeeded)
                    {
                        await GoToAbsoluteAsync(nameof(AdventureWorksLT2019.MauiXApp.Pages.Common.LogInPage));
                    }
                })

            };
            AppShell.Current.Items.Add(logoutMenuItem);

            if (!string.IsNullOrEmpty(gotoRoute))
            {
                await GoToAbsoluteAsync(gotoRoute);
            }
        }

        private static void AddFlyoutItem(string route, Type pageType, string title)
        {
            var thePage = AppShell.Current.Items.Where(f => f.Route == route).FirstOrDefault();
            if (thePage != null) AppShell.Current.Items.Remove(thePage);

            var flyoutItem = new FlyoutItem()
            {
                Title = title,
                Route = route,
                FlyoutDisplayOptions = FlyoutDisplayOptions.AsSingleItem,
                Items =
                    {
                        new ShellContent
                        {
                            Title = title,
                            ContentTemplate = new DataTemplate(pageType),
                        },
                    }
            };
            if (!AppShell.Current.Items.Contains(flyoutItem))
            {
                AppShell.Current.Items.Add(flyoutItem);
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
