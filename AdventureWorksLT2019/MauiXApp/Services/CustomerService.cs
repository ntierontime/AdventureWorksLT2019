using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureWorksLT2019.MauiXApp.Services
{
    public class CustomerService
    {
        private readonly AdventureWorksLT2019.MauiXApp.WebApiClients.CustomerApiClient _customerApiClient;
        private readonly AdventureWorksLT2019.MauiXApp.SQLite.CustomerRepository _customerRepository;
        public CustomerService(
            AdventureWorksLT2019.MauiXApp.WebApiClients.CustomerApiClient customerApiClient,
            AdventureWorksLT2019.MauiXApp.SQLite.CustomerRepository customerRepository)
        {
            _customerApiClient = customerApiClient;
            _customerRepository = customerRepository;
        }
    }
}
