namespace AdventureWorksLT2019.MauiXApp.ViewModels.Customer
{
    public class ItemVM : Framework.MauiX.ViewModels.ItemVMBase<AdventureWorksLT2019.MauiXApp.DataModels.CustomerIdentifier, AdventureWorksLT2019.MauiXApp.DataModels.CustomerDataModel>
    {
        private readonly AdventureWorksLT2019.MauiXApp.Services.CustomerService _customerService;

        public ItemVM(AdventureWorksLT2019.MauiXApp.Services.CustomerService customerService)
            : base()
        {
            _customerService = customerService;

        }
    }
}
