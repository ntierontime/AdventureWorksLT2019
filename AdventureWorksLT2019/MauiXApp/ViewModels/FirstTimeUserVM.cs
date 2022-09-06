using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Input;

namespace AdventureWorksLT2019.MauiXApp.ViewModels
{
    public class FirstTimeUserVM : ObservableObject
    {
        public ICommand DoneCommand => new Command(OnDone, CanDone);

        private void OnDone()
        {
            Application.Current.MainPage = new AdventureWorksLT2019.MauiXApp.AppShell();
        }
        private bool CanDone()
        {
            return true;
        }
    }
}
