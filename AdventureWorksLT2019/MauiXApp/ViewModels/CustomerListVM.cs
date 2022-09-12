using SQLite;
using System.Windows.Input;
using Framework.MauiX.SQLite;
using Framework.MauiX.DataModels;
using Framework.Models;
using AdventureWorksLT2019.MauiXApp.Services;

namespace AdventureWorksLT2019.MauiXApp.ViewModels
{
    public class CustomerListVM: Framework.MauiX.ViewModels.ListVMBase<AdventureWorksLT2019.MauiXApp.DataModels.CustomerAdvancedQuery, AdventureWorksLT2019.MauiXApp.DataModels.CustomerDataModel>
    {
        private readonly AdventureWorksLT2019.MauiXApp.Services.CustomerService _customerService;

        public CustomerListVM(AdventureWorksLT2019.MauiXApp.Services.CustomerService customerService)
            : base()
        {
            _customerService = customerService;

            QueryOrderBySettings = new System.Collections.ObjectModel.ObservableCollection<Framework.MauiX.DataModels.ObservableQueryOrderBySetting>(
                AdventureWorksLT2019.MauiXApp.Services.CustomerService.GetQueryOrderBySettings());
            CurrentQueryOrderBySetting = QueryOrderBySettings.First(t => t.IsSelected);

            TextSearchCommand = new Command<string>(async (text) => {
                Query.TextSearch = text;
                await DoSearch(true); // clear existing
            });

            LaunchAdvancedSearchCommand = new Command(async () => {
                await DoSearch(true); // clear existing
            });

            ApplyAdvancedSearchCommand = new Command(async () => {
                await DoSearch(true); // clear existing
            });

            LoadMoreCommand = new Command(async () => {
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
                    foreach(var item in response.ResponseBody)
                    {
                        Result.Add(item);
                    }
                }
            }
        }
    }
}
