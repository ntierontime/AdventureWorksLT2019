using SQLite;
using System.Windows.Input;
using Framework.MauiX.SQLite;
using Framework.MauiX.DataModels;
using Framework.Models;
using AdventureWorksLT2019.MauiXApp.Services;

namespace AdventureWorksLT2019.MauiXApp.ViewModels.Customer
{
    public class ListVM : Framework.MauiX.ViewModels.ListVMBase<DataModels.CustomerAdvancedQuery, DataModels.CustomerDataModel>
    {
        private readonly AdventureWorksLT2019.MauiXApp.Services.CustomerService _customerService;

        public ListVM(AdventureWorksLT2019.MauiXApp.Services.CustomerService customerService)
        {
            _customerService = customerService;

            QueryOrderBySettings = new System.Collections.ObjectModel.ObservableCollection<ObservableQueryOrderBySetting>(
                CustomerService.GetQueryOrderBySettings());
            CurrentQueryOrderBySetting = QueryOrderBySettings.First(t => t.IsSelected);

            TextSearchCommand = new Command<string>(async (text) =>
            {
                if (Query.TextSearch != text)
                {
                    Query.TextSearch = text;
                    await DoSearch(true); // clear existing
                }
            });

            LaunchAdvancedSearchCommand = new Command(async () =>
            {
                await DoSearch(true); // clear existing
            });

            ApplyAdvancedSearchCommand = new Command(async () =>
            {
                await DoSearch(true); // clear existing
            });

            LoadMoreCommand = new Command(async () =>
            {
                await DoSearch(false); // keep existing
            });
        }

        protected override async Task DoSearch(bool clearExisting)
        {
            var response = await _customerService.Search(Query, CurrentQueryOrderBySetting);
            if (response.Status == System.Net.HttpStatusCode.OK)
            {
                if (clearExisting)
                {
                    Result = new System.Collections.ObjectModel.ObservableCollection<DataModels.CustomerDataModel>(response.ResponseBody);
                }
                else
                {
                    foreach (var item in response.ResponseBody)
                    {
                        Result.Add(item);
                    }
                }
            }
        }
    }
}
