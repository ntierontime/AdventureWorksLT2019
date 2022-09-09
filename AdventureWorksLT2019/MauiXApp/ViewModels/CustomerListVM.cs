using System.Windows.Input;

namespace AdventureWorksLT2019.MauiXApp.ViewModels
{
    public class CustomerListVM: Framework.MauiX.ViewModels.ListVMBase<AdventureWorksLT2019.MauiXApp.DataModels.CustomerAdvancedQuery, AdventureWorksLT2019.MauiXApp.DataModels.CustomerDataModel>
    {
        private readonly AdventureWorksLT2019.MauiXApp.Services.CustomerService _customerService;

        public CustomerListVM(AdventureWorksLT2019.MauiXApp.Services.CustomerService customerService)
        {
            _customerService = customerService;
        }

        public ICommand SearchCommand => new Command<string>(OnSearch);

        private async void OnSearch(string textSearch)
        {
            Criteria.TextSearch = textSearch;
            var response = await _customerService.Search(Criteria);
            if (response.Status == System.Net.HttpStatusCode.OK)
            {
                //if (clearExisting)
                {
                    Result = new System.Collections.ObjectModel.ObservableCollection<AdventureWorksLT2019.MauiXApp.DataModels.CustomerDataModel>(response.ResponseBody);
                }
                //else
                //{
                //    foreach (var item in response.ResponseBody)
                //    {
                //        Result.Add(item);
                //    }
                //}
            }
        }
    }
}
