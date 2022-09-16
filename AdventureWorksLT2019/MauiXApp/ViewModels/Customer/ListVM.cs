using CommunityToolkit.Mvvm.Messaging;

namespace AdventureWorksLT2019.MauiXApp.ViewModels.Customer;

public class ListVM : Framework.MauiX.ViewModels.ListVMBase<AdventureWorksLT2019.MauiXApp.DataModels.CustomerAdvancedQuery, AdventureWorksLT2019.MauiXApp.DataModels.CustomerIdentifier, AdventureWorksLT2019.MauiXApp.DataModels.CustomerDataModel, AdventureWorksLT2019.MauiXApp.Services.CustomerService, AdventureWorksLT2019.MauiXApp.Messages.CustomerItemChangedMessage, AdventureWorksLT2019.MauiXApp.Messages.CustomerItemRequestMessage>
{
    // 1.Start For AdvancedQuery

    private List<Framework.Models.NameValuePair> m_DateTimeRangePast;
    
    public List<Framework.Models.NameValuePair> DateTimeRangePast
    {
        get => m_DateTimeRangePast;
        set => SetProperty(ref m_DateTimeRangePast, value);
    }

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
        DateTimeRangePast = AdventureWorksLT2019.MauiXApp.Common.Helpers.SelectListHelper.GetDefaultPredefinedDateTimeRange();
        SelectedModifiedDateRange = DateTimeRangePast.FirstOrDefault(t => t.Value == EditingQuery.ModifiedDateRange);
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
