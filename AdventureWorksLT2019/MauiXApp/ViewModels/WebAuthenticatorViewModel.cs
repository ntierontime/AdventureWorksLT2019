using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Authentication;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AdventureWorksLT2019.MauiXApp.ViewModels
{
	public class WebAuthenticatorViewModel : INotifyPropertyChanged
    {
        protected string WebApiRootUrl
        {
            get
            {
#if DEBUG
#if WINDOWS
            return "https://localhost:16601/mobileauth/";
#elif ANDROID
            return "https://customer2-app-10-0-2-2.nip.io:16601/mobileauth/";
#elif IOS || MACCATALYST
                return "https://localhost:16601/mobileauth/";
#else
            return "https://localhost:16601/mobileauth/";
#endif
#else
    // TODO: use production url.
#if WINDOWS
            return "https://localhost:16601/mobileauth/";
#elif ANDROID
            return "https://10.0.2.2:16601/mobileauth/";
#elif IOS || MACCATALYST
            return "https://localhost:16601/mobileauth/";
#else
            return "https://localhost:16601/mobileauth/";
#endif
#endif
            }
        }

        //const string authenticationUrl = "https://xamarin-essentials-auth-sample.azurewebsites.net/mobileauth/";

		public WebAuthenticatorViewModel()
		{
			MicrosoftCommand = new Command(async () => await OnAuthenticate("Microsoft"));
			GoogleCommand = new Command(async () => await OnAuthenticate("Google"));
			FacebookCommand = new Command(async () => await OnAuthenticate("Facebook"));
			AppleCommand = new Command(async () => await OnAuthenticate("Apple"));
		}

		public ICommand MicrosoftCommand { get; }

		public ICommand GoogleCommand { get; }

		public ICommand FacebookCommand { get; }

		public ICommand AppleCommand { get; }

		string accessToken = string.Empty;

		public string AuthToken
		{
			get => accessToken;
			set => SetProperty(ref accessToken, value);
		}

		async Task OnAuthenticate(string scheme)
		{
			try
			{
				WebAuthenticatorResult r = null;

				if (scheme.Equals("Apple", StringComparison.Ordinal)
					&& DeviceInfo.Platform == DevicePlatform.iOS
					&& DeviceInfo.Version.Major >= 13)
				{
					// Make sure to enable Apple Sign In in both the
					// entitlements and the provisioning profile.
					var options = new AppleSignInAuthenticator.Options
					{
						IncludeEmailScope = true,
						IncludeFullNameScope = true,
					};
					r = await AppleSignInAuthenticator.AuthenticateAsync(options);
				}
				else
				{
					var authUrl = new Uri(WebApiRootUrl + scheme);
					var callbackUrl = new Uri("xamarinessentials://");

					r = await WebAuthenticator.AuthenticateAsync(authUrl, callbackUrl);
				}

				AuthToken = string.Empty;
				if (r.Properties.TryGetValue("name", out var name) && !string.IsNullOrEmpty(name))
					AuthToken += $"Name: {name}{Environment.NewLine}";
				if (r.Properties.TryGetValue("email", out var email) && !string.IsNullOrEmpty(email))
					AuthToken += $"Email: {email}{Environment.NewLine}";
				AuthToken += r?.AccessToken ?? r?.IdToken;
			}
			catch (OperationCanceledException ex)
			{
				Console.WriteLine("Login canceled.");

				AuthToken = string.Empty;
				await DisplayAlertAsync("Login canceled.");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Failed: {ex.Message}");

				AuthToken = string.Empty;
				await DisplayAlertAsync($"Failed: {ex.Message}");
			}
		}

        bool isBusy;

        public bool IsBusy
        {
            get => isBusy;
            set => SetProperty(ref isBusy, value, onChanged: () => OnPropertyChanged(nameof(IsNotBusy)));
        }

        public bool IsNotBusy => !IsBusy;

        public virtual void OnAppearing()
        {
        }

        public virtual void OnDisappearing()
        {
        }

        internal event Func<string, Task> DoDisplayAlert;

        internal event Func<WebAuthenticatorViewModel, bool, Task> DoNavigate;

        public Task DisplayAlertAsync(string message)
        {
            return DoDisplayAlert?.Invoke(message) ?? Task.CompletedTask;
        }

        public Task NavigateAsync(WebAuthenticatorViewModel vm, bool showModal = false)
        {
            return DoNavigate?.Invoke(vm, showModal) ?? Task.CompletedTask;
        }

        protected virtual bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = "", Action onChanged = null, Func<T, T, bool> validateValue = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            if (validateValue != null && !validateValue(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        protected virtual bool SetProperty<T>(T originalValue, T value, Action onChanged, [CallerMemberName] string propertyName = "", Func<T, T, bool> validateValue = null)
        {
            if (EqualityComparer<T>.Default.Equals(originalValue, value))
                return false;

            if (validateValue != null && !validateValue(originalValue, value))
                return false;

            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
