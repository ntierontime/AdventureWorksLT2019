using Framework.Models;
namespace AdventureWorksLT2019.Models
{
    public partial class AddressCompositeModel : CompositeModel<AddressDataModel, AddressCompositeModel.__DataOptions__>
    {
        // 4. ListTable = 4,
        public CustomerAddressDataModel.DefaultView[]? CustomerAddresses_Via_AddressID { get; set; }
        public SalesOrderHeaderDataModel.DefaultView[]? SalesOrderHeaders_Via_BillToAddressID { get; set; }
        public SalesOrderHeaderDataModel.DefaultView[]? SalesOrderHeaders_Via_ShipToAddressID { get; set; }

        public enum __DataOptions__
        {
            __Master__,
            // 4. ListTable
            CustomerAddresses_Via_AddressID,
            SalesOrderHeaders_Via_BillToAddressID,
            SalesOrderHeaders_Via_ShipToAddressID,

        }
    }
}

