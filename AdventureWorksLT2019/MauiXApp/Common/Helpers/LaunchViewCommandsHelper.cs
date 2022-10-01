using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Messaging;

namespace AdventureWorksLT2019.MauiXApp.Common.Helpers;

public static class LaunchViewCommandsHelper
{
    public static Command<int> GetLaunchProductCategoryDetailsPopupCommand()
    {
        return new Command<int>(async (productCategoryID) =>
        {
            var popup = new AdventureWorksLT2019.MauiXApp.Views.ProductCategory.DetailsPopup();
            WeakReferenceMessenger.Default.Send<AdventureWorksLT2019.MauiXApp.Messages.ProductCategoryIdentifierMessage>(new AdventureWorksLT2019.MauiXApp.Messages.ProductCategoryIdentifierMessage(new AdventureWorksLT2019.MauiXApp.DataModels.ProductCategoryIdentifier { ProductCategoryID = productCategoryID }, Framework.Models.ViewItemTemplates.Details));
            await AdventureWorksLT2019.MauiXApp.AppShell.Current.CurrentPage.ShowPopupAsync(popup);
        });
    }

    public static Command<int> GetLaunchProductCategoryDetailsPageCommand(string returnPath)
    {
        return new Command<int>(async (productCategoryID) =>
        {
            await AdventureWorksLT2019.MauiXApp.Common.Services.AppShellService.GoToAbsoluteAsync(AdventureWorksLT2019.MauiXApp.Common.Services.AppShellRoutes.ProductCategoryDetailsPage);
            WeakReferenceMessenger.Default.Send<AdventureWorksLT2019.MauiXApp.Messages.ProductCategoryIdentifierMessage>(new AdventureWorksLT2019.MauiXApp.Messages.ProductCategoryIdentifierMessage(new AdventureWorksLT2019.MauiXApp.DataModels.ProductCategoryIdentifier { ProductCategoryID = productCategoryID }, Framework.Models.ViewItemTemplates.Details, returnPath));
        });
    }
}