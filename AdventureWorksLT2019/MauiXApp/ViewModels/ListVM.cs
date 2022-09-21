using CommunityToolkit.Mvvm.Messaging;

namespace AdventureWorksLT2019.MauiXApp.ViewModels.Customer;

public class ListVM : Framework.MauiX.ViewModels.ListVMBase<AdventureWorksLT2019.MauiXApp.DataModels.CustomerAdvancedQuery, AdventureWorksLT2019.MauiXApp.DataModels.CustomerIdentifier, AdventureWorksLT2019.MauiXApp.DataModels.CustomerDataModel, AdventureWorksLT2019.MauiXApp.Services.CustomerService, AdventureWorksLT2019.MauiXApp.Messages.CustomerItemChangedMessage, AdventureWorksLT2019.MauiXApp.Messages.CustomerItemRequestMessage>
{
    // 1.Start For AdvancedQuery

    private List<Framework.Models.NameValuePair> m_DateTimeRangeListPast;
    
    public List<Framework.Models.NameValuePair> DateTimeRangeListPast
    {
        get => m_DateTimeRangeListPast;
        set => SetProperty(ref m_DateTimeRangeListPast, value);
    }
    /*
    private List<Framework.Models.NameValuePair> m_DateTimeRangeListFuture;
    public List<Framework.Models.NameValuePair> DateTimeRangeListFuture
    {
        get => m_DateTimeRangeListFuture;
        set => SetProperty(ref m_DateTimeRangeListFuture, value);
    }
    private List<Framework.Models.NameValuePair> m_DateTimeRangeListAll;
    public List<Framework.Models.NameValuePair> DateTimeRangeListAll
    {
        get => m_DateTimeRangeListAll;
        set => SetProperty(ref m_DateTimeRangeListAll, value);
    }
    */

    private Framework.Models.NameValuePair m_SelectedModifiedDateRange;
    public Framework.Models.NameValuePair SelectedModifiedDateRange
    {
        get => m_SelectedModifiedDateRange;
        set
        {
            SetProperty(ref m_SelectedModifiedDateRange, value);
            EditingQuery.ModifiedDateRange = value.Value;
            EditingQuery.ModifiedDateRangeLower = Framework.Models.PreDefinedDateTimeRangesHelper.GetLowerBound(value.Value);
            EditingQuery.ModifiedDateRangeLower = Framework.Models.PreDefinedDateTimeRangesHelper.GetUpperBound(value.Value);
        }
    }

    // 1.End For AdvancedQuery

    public ListVM(AdventureWorksLT2019.MauiXApp.Services.CustomerService dataService)
        : base(dataService)
    {
        DateTimeRangeListPast = AdventureWorksLT2019.MauiXApp.Common.Helpers.SelectListHelper.GetDefaultPredefinedDateTimeRange();
        //DateTimeRangeListFuture = AdventureWorksLT2019.MauiXApp.Common.Helpers.SelectListHelper.GetDefaultPredefinedDateTimeRange(false, true);
        //DateTimeRangeListAll = AdventureWorksLT2019.MauiXApp.Common.Helpers.SelectListHelper.GetDefaultPredefinedDateTimeRange(true, true);
        SelectedModifiedDateRange = DateTimeRangeListPast.FirstOrDefault(t => t.Value == EditingQuery.ModifiedDateRange);
    }

    public override void RegisterRequestSelectedItemMessage()
    {
        RegisterRequestSelectedItemMessage<AdventureWorksLT2019.MauiXApp.ViewModels.Customer.ListVM>(this);
    }

    public override void RegisterItemDataChangedMessage()
    {
        RegisterItemDataChangedMessage<AdventureWorksLT2019.MauiXApp.ViewModels.Customer.ListVM>(this);
    }
}
