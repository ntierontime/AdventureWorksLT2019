using AdventureWorksLT2019.Models.Definitions;
using Framework.Models;

namespace AdventureWorksLT2019.ServiceContracts
{
    public interface IDropDownListService
    {

        Task<Dictionary<string, List<NameValuePair>>> GetCustomerAddressTopLevelDropDownListsFromDatabase();

        Task<Dictionary<string, List<NameValuePair>>> GetProductTopLevelDropDownListsFromDatabase();

        Task<Dictionary<string, List<NameValuePair>>> GetProductCategoryTopLevelDropDownListsFromDatabase();

        Task<Dictionary<string, List<NameValuePair>>> GetProductModelProductDescriptionTopLevelDropDownListsFromDatabase();

        Task<Dictionary<string, List<NameValuePair>>> GetSalesOrderDetailTopLevelDropDownListsFromDatabase();

        Task<Dictionary<string, List<NameValuePair>>> GetSalesOrderHeaderTopLevelDropDownListsFromDatabase();
        /// <summary>
        /// This method will be used to get top level dropdownlists from database for Search and Editing, to minimize roundtrip.
        /// the Key comes from {SolutionName}.Models.Definitions.TopLevelDropDownLists
        /// </summary>
        /// <returns></returns>
        Task<Dictionary<string, List<NameValuePair>>> GetTopLevelDropDownListsFromDatabase(TopLevelDropDownLists[] options);
    }
}

