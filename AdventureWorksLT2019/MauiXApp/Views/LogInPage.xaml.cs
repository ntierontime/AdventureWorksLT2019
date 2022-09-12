namespace AdventureWorksLT2019.MauiXApp.Views;

public partial class LogInPage : ContentPage
{
	public LogInPage()
	{
		InitializeComponent();

        BindingContext = Framework.MauiX.Helpers.ServiceHelper.GetService<AdventureWorksLT2019.MauiXApp.ViewModels.LogInVM>();
    }

    private async void OnRegisterANewUserButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(AdventureWorksLT2019.MauiXApp.Views.RegisterUserPage));
    }
}