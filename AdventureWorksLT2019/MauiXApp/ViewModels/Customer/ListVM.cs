using Framework.MauiX.Helpers;
using AdventureWorksLT2019.MauiXApp.Common.Services;
using AdventureWorksLT2019.MauiXApp.Messages;
using AdventureWorksLT2019.MauiXApp.DataModels;
using AdventureWorksLT2019.MauiXApp.Common.Helpers;
using AdventureWorksLT2019.MauiXApp.Services;
using Framework.Models;
using Framework.MauiX.ViewModels;
using CommunityToolkit.Mvvm.Messaging;
using System.Windows.Input;

namespace AdventureWorksLT2019.MauiXApp.ViewModels.Customer;

public class ListVM : ListVMBase<CustomerAdvancedQuery, CustomerIdentifier, CustomerDataModel, CustomerService, CustomerItemChangedMessage>
{
    #region AdvancedQuery.Start ForeignKey SelectLists and DateTimeRanges
    // AdvancedQuery.DateTimeRangeList: DateTimeRangeListPast/DateTimeRangeListFuture/DateTimeRangeListAll
    private List<NameValuePair> m_DateTimeRangeListPast;
    public List<NameValuePair> DateTimeRangeListPast
    {
        get => m_DateTimeRangeListPast;
        set => SetProperty(ref m_DateTimeRangeListPast, value);
    }
    /*
    private List<NameValuePair> m_DateTimeRangeListFuture;
    public List<NameValuePair> DateTimeRangeListFuture
    {
        get => m_DateTimeRangeListFuture;
        set => SetProperty(ref m_DateTimeRangeListFuture, value);
    }
    private List<NameValuePair> m_DateTimeRangeListAll;
    public List<NameValuePair> DateTimeRangeListAll
    {
        get => m_DateTimeRangeListAll;
        set => SetProperty(ref m_DateTimeRangeListAll, value);
    }
    */

    // AdvancedQuery.DateTimeRange.1 ModifiedDateRange
    private NameValuePair m_SelectedModifiedDateRange;
    public NameValuePair SelectedModifiedDateRange
    {
        get => m_SelectedModifiedDateRange;
        set
        {
            SetProperty(ref m_SelectedModifiedDateRange, value);
            EditingQuery.ModifiedDateRange = value.Value;
            EditingQuery.ModifiedDateRangeLower = PreDefinedDateTimeRangesHelper.GetLowerBound(value.Value);
            EditingQuery.ModifiedDateRangeLower = PreDefinedDateTimeRangesHelper.GetUpperBound(value.Value);
        }
    }

    #endregion AdvancedQuery.End ForeignKey SelectLists and DateTimeRanges

    //public ICommand BulkDeleteCommand { get; private set; }

    public ListVM(CustomerService dataService)
        : base(dataService, true)
    {
        // AdvancedQuery.Start DateTimeRanges
        // AdvancedQuery.DateTimeRangeList: DateTimeRangeListPast/DateTimeRangeListFuture/DateTimeRangeListAll
        DateTimeRangeListPast = SelectListHelper.GetDefaultPredefinedDateTimeRange();
        /*
        DateTimeRangeListFuture = SelectListHelper.GetDefaultPredefinedDateTimeRange(false, true);
        DateTimeRangeListAll = SelectListHelper.GetDefaultPredefinedDateTimeRange(true, true);
        */

        // AdvancedQuery.DateTimeRange.1 ModifiedDateRange
        SelectedModifiedDateRange = DateTimeRangeListPast.FirstOrDefault(t => t.Value == EditingQuery.ModifiedDateRange);
        /*
        SelectedModifiedDateRange = DateTimeRangeListFuture.FirstOrDefault(t => t.Value == EditingQuery.ModifiedDateRange);
        SelectedModifiedDateRange = DateTimeRangeListAll.FirstOrDefault(t => t.Value == EditingQuery.ModifiedDateRange);
        */
        // AdvancedQuery.End DateTimeRanges

        // 1. Init LaunchCustomerCreatePageCommand
        LaunchCreatePageCommand = LaunchViewCommandsHelper.GetLaunchCustomerCreatePageCommand(AppShellRoutes.CustomerListPage);
        // 2. Init LaunchCustomerDeletePageCommand
        LaunchDeletePageCommand = LaunchViewCommandsHelper.GetLaunchCustomerDeletePageCommand(AppShellRoutes.CustomerListPage);
        // 3. Init LaunchCustomerDetailsPageCommand
        LaunchDetailsPageCommand = LaunchViewCommandsHelper.GetLaunchCustomerDetailsPageCommand(AppShellRoutes.CustomerListPage);
        // 4. Init LaunchCustomerEditPageCommand
        LaunchEditPageCommand = LaunchViewCommandsHelper.GetLaunchCustomerEditPageCommand(AppShellRoutes.CustomerListPage);
        // 5. Init LaunchCustomerCreatePopupCommand
        LaunchCreatePopupCommand = LaunchViewCommandsHelper.GetLaunchCustomerCreatePopupCommand();
        // 6. Init LaunchCustomerDeletePopupCommand
        LaunchDeletePopupCommand = LaunchViewCommandsHelper.GetLaunchCustomerDeletePopupCommand();
        // 7. Init LaunchCustomerDetailsPopupCommand
        LaunchDetailsPopupCommand = LaunchViewCommandsHelper.GetLaunchCustomerDetailsPopupCommand();
        // 8. Init LaunchCustomerEditPopupCommand
        LaunchEditPopupCommand = LaunchViewCommandsHelper.GetLaunchCustomerEditPopupCommand();
        // 9. Init LaunchCustomerAdvancedSearchPopupCommand
        AdvancedSearchLaunchCommand = LaunchViewCommandsHelper.GetLaunchCustomerAdvancedSearchPopupCommand();
        // 10. Init LaunchCustomerListQuickActionsPopupCommand
        ListBulkActionsLaunchCommand = new Command<string>(
            (currentBulkActionName) => 
            {
                BulkUpdateItem = _dataService.GetDefault();
                CurrentBulkActionName = currentBulkActionName;
                var launchCommand = LaunchViewCommandsHelper.GetLaunchCustomerListQuickActionsPopupCommand();
                launchCommand.Execute(null);
            },
            (currentBulkActionName) => EnableMultiSelectCommands()
            );

            
        // 11. Init LaunchCustomerListOrderBysPopupCommand
        ListOrderBysLaunchCommand = LaunchViewCommandsHelper.GetLaunchCustomerListOrderBysPopupCommand();

        //BulkDeleteCommand = new Command(
        //    async () =>
        //    {
        //        // TODO: can add popup to confirm, and popup to show status OK/Failed
        //        var response = await _dataService.BulkDelete(SelectedItems.Select(t => t.GetIdentifier()).ToList());
        //        if (response.Status == System.Net.HttpStatusCode.OK)
        //        {
        //            foreach (var item in SelectedItems)
        //            {
        //                Result.Remove(item);
        //            }
        //            SelectedItems.Clear();
        //            RefreshMultiSelectCommandsCanExecute();
        //        }
        //    },
        //    () => EnableMultiSelectCommands()
        //);
    }

    protected override void CopyBulkUpdateResult(AdventureWorksLT2019.MauiXApp.DataModels.CustomerDataModel source, AdventureWorksLT2019.MauiXApp.DataModels.CustomerDataModel destination)
    {
        if(CurrentBulkActionName == "NameStyle")
        {
            destination.NameStyle = source.NameStyle;
            return;
        }
    }

    //public override void RefreshMultiSelectCommandsCanExecute()
    //{
    //    base.RefreshMultiSelectCommandsCanExecute();
    //    ((Command)BulkDeleteCommand).ChangeCanExecute();
    //}

    public override void RegisterItemDataChangedMessage()
    {
        RegisterItemDataChangedMessage<ListVM>(this);
    }
}

