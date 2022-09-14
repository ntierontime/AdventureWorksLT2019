using CommunityToolkit.Mvvm.Messaging;

namespace AdventureWorksLT2019.MauiXApp.ViewModels.Customer;

public class ListVM : Framework.MauiX.ViewModels.ListVMBase<AdventureWorksLT2019.MauiXApp.DataModels.CustomerAdvancedQuery, AdventureWorksLT2019.MauiXApp.DataModels.CustomerIdentifier, AdventureWorksLT2019.MauiXApp.DataModels.CustomerDataModel, AdventureWorksLT2019.MauiXApp.Services.CustomerService, AdventureWorksLT2019.MauiXApp.Messages.CustomerItemChangedMessage, AdventureWorksLT2019.MauiXApp.Messages.CustomerItemRequestMessage>
{
    public ListVM(AdventureWorksLT2019.MauiXApp.Services.CustomerService dataService)
        : base(dataService)
    {
    }

    public override void RegisterRequestSelectedItemMessage()
    {
        RegisterRequestSelectedItemMessage<AdventureWorksLT2019.MauiXApp.ViewModels.Customer.ListVM>(this);
    }

    public override void RegisterItemDataChangedMessage()
    {
        RegisterItemDataChangedMessage<AdventureWorksLT2019.MauiXApp.ViewModels.Customer.ListVM>(this);
    }
}
