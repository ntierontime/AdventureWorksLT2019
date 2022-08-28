using AdventureWorksLT2019.Models.Definitions;
using AdventureWorksLT2019.ServiceContracts;
using AdventureWorksLT2019.Models;
using AdventureWorksLT2019.RepositoryContracts;
using Framework.Models;

using System.Collections.Concurrent;
using System.Net;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AdventureWorksLT2019.Services
{
    public class DropDownListService : IDropDownListService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<DropDownListService> _logger;

        public DropDownListService(
            IServiceScopeFactory serviceScopeFactory,
            ILogger<DropDownListService> logger)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
        }

        public async Task<Dictionary<string, List<NameValuePair>>> GetCustomerAddressTopLevelDropDownListsFromDatabase()
        {
            var  _topLevelDropDownLists =
                new TopLevelDropDownLists[]
                {
                    TopLevelDropDownLists.Address,
                    TopLevelDropDownLists.Customer,
                };
            return await GetTopLevelDropDownListsFromDatabase(_topLevelDropDownLists);
        }

        public async Task<Dictionary<string, List<NameValuePair>>> GetProductTopLevelDropDownListsFromDatabase()
        {
            var  _topLevelDropDownLists =
                new TopLevelDropDownLists[]
                {
                    TopLevelDropDownLists.ProductModel,
                };
            return await GetTopLevelDropDownListsFromDatabase(_topLevelDropDownLists);
        }

        public async Task<Dictionary<string, List<NameValuePair>>> GetProductCategoryTopLevelDropDownListsFromDatabase()
        {
            var  _topLevelDropDownLists =
                new TopLevelDropDownLists[]
                {

                };
            return await GetTopLevelDropDownListsFromDatabase(_topLevelDropDownLists);
        }

        public async Task<Dictionary<string, List<NameValuePair>>> GetProductModelProductDescriptionTopLevelDropDownListsFromDatabase()
        {
            var  _topLevelDropDownLists =
                new TopLevelDropDownLists[]
                {
                    TopLevelDropDownLists.ProductDescription,
                    TopLevelDropDownLists.ProductModel,
                };
            return await GetTopLevelDropDownListsFromDatabase(_topLevelDropDownLists);
        }

        public async Task<Dictionary<string, List<NameValuePair>>> GetSalesOrderDetailTopLevelDropDownListsFromDatabase()
        {
            var  _topLevelDropDownLists =
                new TopLevelDropDownLists[]
                {
                    TopLevelDropDownLists.Address,
                    TopLevelDropDownLists.Customer,
                    TopLevelDropDownLists.ProductModel,
                };
            return await GetTopLevelDropDownListsFromDatabase(_topLevelDropDownLists);
        }

        public async Task<Dictionary<string, List<NameValuePair>>> GetSalesOrderHeaderTopLevelDropDownListsFromDatabase()
        {
            var  _topLevelDropDownLists =
                new TopLevelDropDownLists[]
                {
                    TopLevelDropDownLists.Address,
                    TopLevelDropDownLists.Customer,
                };
            return await GetTopLevelDropDownListsFromDatabase(_topLevelDropDownLists);
        }

        /// <summary>
        /// This method will be used to get top level dropdownlists from database for Search and Editing, to minimize roundtrip.
        /// the Key comes from {SolutionName}.Models.Definitions.TopLevelDropDownLists
        /// </summary>
        /// <returns></returns>
        public async Task<Dictionary<string, List<NameValuePair>>> GetTopLevelDropDownListsFromDatabase(TopLevelDropDownLists[] options)
        {
            var dropDownLists = new ConcurrentDictionary<string, List<NameValuePair>>();

            var tasks = new List<Task>();

            if (options == null || options.Any(t => t == TopLevelDropDownLists.BuildVersion))
            {
                tasks.Add(Task.Run(async () =>
                {
                    using (var scope = _serviceScopeFactory.CreateScope())
                    {
                        var buildVersionRepository = scope.ServiceProvider.GetRequiredService<IBuildVersionRepository>();

                        var oneList = await buildVersionRepository.GetCodeList(new BuildVersionAdvancedQuery { PageIndex = 1, PageSize = 10000 });
                        if (oneList.Status == HttpStatusCode.OK)
                        {
                            dropDownLists.TryAdd(TopLevelDropDownLists.BuildVersion.ToString(), oneList?.ResponseBody?.ToList() ?? new List<NameValuePair>());
                        }
                    }
                }));
            }

            if (options == null || options.Any(t => t == TopLevelDropDownLists.ErrorLog))
            {
                tasks.Add(Task.Run(async () =>
                {
                    using (var scope = _serviceScopeFactory.CreateScope())
                    {
                        var errorLogRepository = scope.ServiceProvider.GetRequiredService<IErrorLogRepository>();

                        var oneList = await errorLogRepository.GetCodeList(new ErrorLogAdvancedQuery { PageIndex = 1, PageSize = 10000 });
                        if (oneList.Status == HttpStatusCode.OK)
                        {
                            dropDownLists.TryAdd(TopLevelDropDownLists.ErrorLog.ToString(), oneList?.ResponseBody?.ToList() ?? new List<NameValuePair>());
                        }
                    }
                }));
            }

            if (options == null || options.Any(t => t == TopLevelDropDownLists.Address))
            {
                tasks.Add(Task.Run(async () =>
                {
                    using (var scope = _serviceScopeFactory.CreateScope())
                    {
                        var addressRepository = scope.ServiceProvider.GetRequiredService<IAddressRepository>();

                        var oneList = await addressRepository.GetCodeList(new AddressAdvancedQuery { PageIndex = 1, PageSize = 10000 });
                        if (oneList.Status == HttpStatusCode.OK)
                        {
                            dropDownLists.TryAdd(TopLevelDropDownLists.Address.ToString(), oneList?.ResponseBody?.ToList() ?? new List<NameValuePair>());
                        }
                    }
                }));
            }

            if (options == null || options.Any(t => t == TopLevelDropDownLists.Customer))
            {
                tasks.Add(Task.Run(async () =>
                {
                    using (var scope = _serviceScopeFactory.CreateScope())
                    {
                        var customerRepository = scope.ServiceProvider.GetRequiredService<ICustomerRepository>();

                        var oneList = await customerRepository.GetCodeList(new CustomerAdvancedQuery { PageIndex = 1, PageSize = 10000 });
                        if (oneList.Status == HttpStatusCode.OK)
                        {
                            dropDownLists.TryAdd(TopLevelDropDownLists.Customer.ToString(), oneList?.ResponseBody?.ToList() ?? new List<NameValuePair>());
                        }
                    }
                }));
            }

            if (options == null || options.Any(t => t == TopLevelDropDownLists.ProductDescription))
            {
                tasks.Add(Task.Run(async () =>
                {
                    using (var scope = _serviceScopeFactory.CreateScope())
                    {
                        var productDescriptionRepository = scope.ServiceProvider.GetRequiredService<IProductDescriptionRepository>();

                        var oneList = await productDescriptionRepository.GetCodeList(new ProductDescriptionAdvancedQuery { PageIndex = 1, PageSize = 10000 });
                        if (oneList.Status == HttpStatusCode.OK)
                        {
                            dropDownLists.TryAdd(TopLevelDropDownLists.ProductDescription.ToString(), oneList?.ResponseBody?.ToList() ?? new List<NameValuePair>());
                        }
                    }
                }));
            }

            if (options == null || options.Any(t => t == TopLevelDropDownLists.ProductModel))
            {
                tasks.Add(Task.Run(async () =>
                {
                    using (var scope = _serviceScopeFactory.CreateScope())
                    {
                        var productModelRepository = scope.ServiceProvider.GetRequiredService<IProductModelRepository>();

                        var oneList = await productModelRepository.GetCodeList(new ProductModelAdvancedQuery { PageIndex = 1, PageSize = 10000 });
                        if (oneList.Status == HttpStatusCode.OK)
                        {
                            dropDownLists.TryAdd(TopLevelDropDownLists.ProductModel.ToString(), oneList?.ResponseBody?.ToList() ?? new List<NameValuePair>());
                        }
                    }
                }));
            }

            if (tasks.Count > 0)
            {
                Task t = Task.WhenAll(tasks.ToArray());
                try
                {
                    await t;
                }
                catch { }
            }
            return new Dictionary<string, List<NameValuePair>>(dropDownLists);
        }
    }
}

