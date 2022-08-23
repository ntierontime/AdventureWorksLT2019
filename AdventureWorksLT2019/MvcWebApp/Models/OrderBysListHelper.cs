using Framework.Models;
using AdventureWorksLT2019.Resx;

namespace AdventureWorksLT2019.MvcWebApp.Models
{
    public class OrderBysListHelper
    {
        private readonly IUIStrings _localizor;

        public OrderBysListHelper(IUIStrings localizor)
        {
            _localizor = localizor;
        }

        public List<NameValuePair> GetBuildVersionOrderBys()
        {
            return new List<NameValuePair>(new[] {
                new NameValuePair { Name = string.Format("{0} a-Z <i class='fa-solid fa-down-long pe-1'></i>", _localizor.Get("VersionDate")), Value = "VersionDate~ASC" },
                new NameValuePair { Name = string.Format("{0} a-Z <i class='fa-solid fa-up-long pe-1'></i>", _localizor.Get("VersionDate")), Value = "VersionDate~DESC" },
            });
        }
        public string GetDefaultBuildVersionOrderBys()
        {
            return GetBuildVersionOrderBys().First().Value;
        }

        public List<NameValuePair> GetErrorLogOrderBys()
        {
            return new List<NameValuePair>(new[] {
                new NameValuePair { Name = string.Format("{0} a-Z <i class='fa-solid fa-down-long pe-1'></i>", _localizor.Get("ErrorTime")), Value = "ErrorTime~ASC" },
                new NameValuePair { Name = string.Format("{0} a-Z <i class='fa-solid fa-up-long pe-1'></i>", _localizor.Get("ErrorTime")), Value = "ErrorTime~DESC" },
            });
        }
        public string GetDefaultErrorLogOrderBys()
        {
            return GetErrorLogOrderBys().First().Value;
        }

        public List<NameValuePair> GetAddressOrderBys()
        {
            return new List<NameValuePair>(new[] {
                new NameValuePair { Name = string.Format("{0} a-Z <i class='fa-solid fa-down-long pe-1'></i>", _localizor.Get("ModifiedDate")), Value = "ModifiedDate~ASC" },
                new NameValuePair { Name = string.Format("{0} a-Z <i class='fa-solid fa-up-long pe-1'></i>", _localizor.Get("ModifiedDate")), Value = "ModifiedDate~DESC" },
            });
        }
        public string GetDefaultAddressOrderBys()
        {
            return GetAddressOrderBys().First().Value;
        }

        public List<NameValuePair> GetCustomerOrderBys()
        {
            return new List<NameValuePair>(new[] {
                new NameValuePair { Name = string.Format("{0} a-Z <i class='fa-solid fa-down-long pe-1'></i>", _localizor.Get("ModifiedDate")), Value = "ModifiedDate~ASC" },
                new NameValuePair { Name = string.Format("{0} a-Z <i class='fa-solid fa-up-long pe-1'></i>", _localizor.Get("ModifiedDate")), Value = "ModifiedDate~DESC" },
            });
        }
        public string GetDefaultCustomerOrderBys()
        {
            return GetCustomerOrderBys().First().Value;
        }

        public List<NameValuePair> GetCustomerAddressOrderBys()
        {
            return new List<NameValuePair>(new[] {
                new NameValuePair { Name = string.Format("{0} a-Z <i class='fa-solid fa-down-long pe-1'></i>", _localizor.Get("ModifiedDate")), Value = "ModifiedDate~ASC" },
                new NameValuePair { Name = string.Format("{0} a-Z <i class='fa-solid fa-up-long pe-1'></i>", _localizor.Get("ModifiedDate")), Value = "ModifiedDate~DESC" },
            });
        }
        public string GetDefaultCustomerAddressOrderBys()
        {
            return GetCustomerAddressOrderBys().First().Value;
        }

        public List<NameValuePair> GetProductOrderBys()
        {
            return new List<NameValuePair>(new[] {
                new NameValuePair { Name = string.Format("{0} a-Z <i class='fa-solid fa-down-long pe-1'></i>", _localizor.Get("SellStartDate")), Value = "SellStartDate~ASC" },
                new NameValuePair { Name = string.Format("{0} a-Z <i class='fa-solid fa-up-long pe-1'></i>", _localizor.Get("SellStartDate")), Value = "SellStartDate~DESC" },
            });
        }
        public string GetDefaultProductOrderBys()
        {
            return GetProductOrderBys().First().Value;
        }

        public List<NameValuePair> GetProductCategoryOrderBys()
        {
            return new List<NameValuePair>(new[] {
                new NameValuePair { Name = string.Format("{0} a-Z <i class='fa-solid fa-down-long pe-1'></i>", _localizor.Get("ModifiedDate")), Value = "ModifiedDate~ASC" },
                new NameValuePair { Name = string.Format("{0} a-Z <i class='fa-solid fa-up-long pe-1'></i>", _localizor.Get("ModifiedDate")), Value = "ModifiedDate~DESC" },
            });
        }
        public string GetDefaultProductCategoryOrderBys()
        {
            return GetProductCategoryOrderBys().First().Value;
        }

        public List<NameValuePair> GetProductDescriptionOrderBys()
        {
            return new List<NameValuePair>(new[] {
                new NameValuePair { Name = string.Format("{0} a-Z <i class='fa-solid fa-down-long pe-1'></i>", _localizor.Get("ModifiedDate")), Value = "ModifiedDate~ASC" },
                new NameValuePair { Name = string.Format("{0} a-Z <i class='fa-solid fa-up-long pe-1'></i>", _localizor.Get("ModifiedDate")), Value = "ModifiedDate~DESC" },
            });
        }
        public string GetDefaultProductDescriptionOrderBys()
        {
            return GetProductDescriptionOrderBys().First().Value;
        }

        public List<NameValuePair> GetProductModelOrderBys()
        {
            return new List<NameValuePair>(new[] {
                new NameValuePair { Name = string.Format("{0} a-Z <i class='fa-solid fa-down-long pe-1'></i>", _localizor.Get("ModifiedDate")), Value = "ModifiedDate~ASC" },
                new NameValuePair { Name = string.Format("{0} a-Z <i class='fa-solid fa-up-long pe-1'></i>", _localizor.Get("ModifiedDate")), Value = "ModifiedDate~DESC" },
            });
        }
        public string GetDefaultProductModelOrderBys()
        {
            return GetProductModelOrderBys().First().Value;
        }

        public List<NameValuePair> GetProductModelProductDescriptionOrderBys()
        {
            return new List<NameValuePair>(new[] {
                new NameValuePair { Name = string.Format("{0} a-Z <i class='fa-solid fa-down-long pe-1'></i>", _localizor.Get("ModifiedDate")), Value = "ModifiedDate~ASC" },
                new NameValuePair { Name = string.Format("{0} a-Z <i class='fa-solid fa-up-long pe-1'></i>", _localizor.Get("ModifiedDate")), Value = "ModifiedDate~DESC" },
            });
        }
        public string GetDefaultProductModelProductDescriptionOrderBys()
        {
            return GetProductModelProductDescriptionOrderBys().First().Value;
        }

        public List<NameValuePair> GetSalesOrderDetailOrderBys()
        {
            return new List<NameValuePair>(new[] {
                new NameValuePair { Name = string.Format("{0} a-Z <i class='fa-solid fa-down-long pe-1'></i>", _localizor.Get("ModifiedDate")), Value = "ModifiedDate~ASC" },
                new NameValuePair { Name = string.Format("{0} a-Z <i class='fa-solid fa-up-long pe-1'></i>", _localizor.Get("ModifiedDate")), Value = "ModifiedDate~DESC" },
            });
        }
        public string GetDefaultSalesOrderDetailOrderBys()
        {
            return GetSalesOrderDetailOrderBys().First().Value;
        }

        public List<NameValuePair> GetSalesOrderHeaderOrderBys()
        {
            return new List<NameValuePair>(new[] {
                new NameValuePair { Name = string.Format("{0} a-Z <i class='fa-solid fa-down-long pe-1'></i>", _localizor.Get("OrderDate")), Value = "OrderDate~ASC" },
                new NameValuePair { Name = string.Format("{0} a-Z <i class='fa-solid fa-up-long pe-1'></i>", _localizor.Get("OrderDate")), Value = "OrderDate~DESC" },
            });
        }
        public string GetDefaultSalesOrderHeaderOrderBys()
        {
            return GetSalesOrderHeaderOrderBys().First().Value;
        }

    }
}

