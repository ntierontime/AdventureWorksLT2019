using Framework.Models;
using System.ComponentModel.DataAnnotations;

namespace AdventureWorksLT2019.MauiXApp.DataModels;

public class CustomerIdentifier
{

    // PredicateType:Equals
    public int? CustomerID { get; set; }

    public string GetWebApiRoute()
    {
        return $"{CustomerID}";
    }
}

public class CustomerAdvancedQuery: Framework.MauiX.DataModels.ObservableBaseQuery
{
    // PredicateType:Equals
    //private Framework.Models.BooleanSearchOptions m_NameStyle;
    //public Framework.Models.BooleanSearchOptions NameStyle
    //{
    //    get => m_NameStyle;
    //    set => SetProperty(ref m_NameStyle, value);
    //}
    private string m_NameStyle = "All";
    public string NameStyle
    {
        get => m_NameStyle;
        set => SetProperty(ref m_NameStyle, value);
    }

    public string ModifiedDateRange { get; set; }
    // PredicateType:Range - Lower Bound
    [DataType(DataType.DateTime)]
    public System.DateTime? ModifiedDateRangeLower { get; set; }
    // PredicateType:Range - Upper Bound
    [DataType(DataType.DateTime)]
    public System.DateTime? ModifiedDateRangeUpper { get; set; }

}

