using Framework.Models;

namespace AdventureWorksLT2019.MauiXApp.DataModels;

public partial class CustomerCompositeModel : CompositeModel<CustomerDataModel, CustomerCompositeModel.__DataOptions__>
{
        // 4. ListTable = 4,
        public CustomerAddressDataModel[] CustomerAddresses_Via_CustomerID { get; set; }
        public SalesOrderHeaderDataModel[] SalesOrderHeaders_Via_CustomerID { get; set; }

    public enum __DataOptions__
    {
        __Master__,
        // 4. ListTable
        CustomerAddresses_Via_CustomerID,
        SalesOrderHeaders_Via_CustomerID,

    }
}

