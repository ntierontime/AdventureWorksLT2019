namespace AdventureWorksLT2019.MauiXApp.Pages.Account;

public partial class LogInPage : ContentPage
{
	public LogInPage()
	{
		InitializeComponent();

        BindingContext = Framework.MauiX.Helpers.ServiceHelper.GetService<AdventureWorksLT2019.MauiXApp.ViewModels.Account.LogInVM>();
	}

    private void OnSignUpButtonClicked(object sender, EventArgs e)
    {
        // await Navigation.PushModalAsync(new RegisterPage());
    }
}