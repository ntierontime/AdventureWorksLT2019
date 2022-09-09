namespace AdventureWorksLT2019.MauiXApp.DataModels
{
    public partial class CustomerCompositeModel : Framework.Models.CompositeModel<AdventureWorksLT2019.MauiXApp.DataModels.CustomerDataModel, AdventureWorksLT2019.MauiXApp.DataModels.CustomerCompositeModel.__DataOptions__>
    {
        //// 4. ListTable = 4,
        //public CustomerAddressDataModel.DefaultView[]? CustomerAddresses_Via_CustomerID { get; set; }
        //public SalesOrderHeaderDataModel.DefaultView[]? SalesOrderHeaders_Via_CustomerID { get; set; }

        public enum __DataOptions__
        {
            __Master__,
            //// 4. ListTable
            //CustomerAddresses_Via_CustomerID,
            //SalesOrderHeaders_Via_CustomerID,

        }
    }
}

