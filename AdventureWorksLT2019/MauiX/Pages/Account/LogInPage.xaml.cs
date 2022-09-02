namespace AdventureWorksLT2019.MauiX.Pages.Account;

public partial class LogInPage : ContentPage
{
	public LogInPage()
	{
		InitializeComponent();

        BindingContext = DependencyService.Resolve<AdventureWorksLT2019.MauiX.ViewModels.Account.LogInVM>();
	}

    private void OnSignUpButtonClicked(object sender, EventArgs e)
    {
        // await Navigation.PushModalAsync(new RegisterPage());
    }
}