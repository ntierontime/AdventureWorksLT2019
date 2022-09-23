using Framework.MauiX.DataModels;
using Framework.MauiX.ComponentModels;
using Framework.MauiX.Services;
using Framework.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Framework.MauiX.ViewModels;

public abstract class ListVMBase<TAdvancedQuery, TIdentifier, TDataModel, TDataService, TDataChangedMessage, TItemRequestMessage> : ObservableObject
    where TAdvancedQuery : ObservableBaseQuery, IClone<TAdvancedQuery>, new()
    where TDataModel : class, IClone<TDataModel>, ICopyTo<TDataModel>
    where TDataService : class, IDataServiceBase<TAdvancedQuery, TIdentifier, TDataModel>
    where TDataChangedMessage : ValueChangedMessageExt<TDataModel>
    where TItemRequestMessage : RequestMessage<TDataModel>, new()
{
    private TAdvancedQuery m_Query = new();
    public TAdvancedQuery Query
    {
        get => m_Query;
        set => SetProperty(ref m_Query, value);
    }

    private TAdvancedQuery m_EditingQuery = new();
    /// <summary>
    /// This is a copy of Query property(not same instance), will sync when TextSearchCommand(click Keyboard.Done button) or AdvancedSearchConfirmCommand(AdvancedSearchPopup.Apply button).
    /// </summary>
    public TAdvancedQuery EditingQuery
    {
        get => m_EditingQuery;
        set => SetProperty(ref m_EditingQuery, value);
    }

    private bool m_IsBusy;
    public bool IsBusy
    {
        get => m_IsBusy;
        set => SetProperty(ref m_IsBusy, value);
    }

    private bool m_IsRefreshing;
    public bool IsRefreshing
    {
        get => m_IsRefreshing;
        set => SetProperty(ref m_IsRefreshing, value);
    }

    protected ObservableCollection<TDataModel> m_Result = new();
    public ObservableCollection<TDataModel> Result
    {
        get => m_Result;
        set => SetProperty(ref m_Result, value);
    }

    private TDataModel m_SelectedItem;
    public TDataModel SelectedItem
    {
        get => m_SelectedItem;
        set => SetProperty(ref m_SelectedItem, value);
    }

    private System.Net.HttpStatusCode m_Status;
    public System.Net.HttpStatusCode Status
    {
        get => m_Status;
        set => SetProperty(ref m_Status, value);
    }

    private string m_StatusMessage;
    public string StatusMessage
    {
        get => m_StatusMessage;
        set => SetProperty(ref m_StatusMessage, value);
    }

    public ObservableCollection<ObservableQueryOrderBySetting> QueryOrderBySettings { get; protected set; } = new();
    private ObservableQueryOrderBySetting m_CurrentQueryOrderBySetting;
    public ObservableQueryOrderBySetting CurrentQueryOrderBySetting
    {
        get => m_CurrentQueryOrderBySetting;
        set => SetProperty(ref m_CurrentQueryOrderBySetting, value);
    }

    public ICommand TextSearchCommand { get; protected set; }
    public ICommand AdvancedSearchLaunchCommand { get; protected set; }
    public ICommand AdvancedSearchConfirmCommand { get; protected set; }
    public ICommand AdvancedSearchCancelCommand { get; protected set; }

    public ICommand ListQuickActionsLaunchCommand { get; protected set; }
    public ICommand ListQuickActionsCancelCommand { get; protected set; }

    public ICommand ListOrderBysLaunchCommand { get; protected set; }
    public ICommand ListOrderByChangedCommand { get; protected set; }
    public ICommand ListOrderBysCancelCommand { get; protected set; }

    public ICommand LoadMoreCommand { get; protected set; }
    public ICommand RefreshCommand { get; protected set; }

    public ICommand LaunchItemPopupViewCommand { get; protected set; }
    public ICommand LaunchItemPageCommand { get; protected set; }


    protected readonly TDataService _dataService;

    public ListVMBase(TDataService dataService)
    {
        _dataService = dataService;

        QueryOrderBySettings = new System.Collections.ObjectModel.ObservableCollection<ObservableQueryOrderBySetting>(
            _dataService.GetQueryOrderBySettings());
        CurrentQueryOrderBySetting = QueryOrderBySettings.First(t => t.IsSelected);

        TextSearchCommand = new Command<string>(async (text) =>
        {
            if (EditingQuery.TextSearch != text)
            {
                EditingQuery.TextSearch = text;
                EditingQuery.PageIndex = 1;
                await DoSearch(true, true); // clear existing
            }
        });

        LoadMoreCommand = new Command(async () =>
        {
            EditingQuery.PageIndex++;
            await DoSearch(false, false); // keep existing
        });

        RefreshCommand = new Command(async () =>
        {
            EditingQuery.PageIndex = 1;
            await DoSearch(true, true);
        });

        RegisterRequestSelectedItemMessage();
        RegisterItemDataChangedMessage();
    }

    public virtual async Task DoSearch(bool isLoadMore, bool showRefreshing, bool loadIfAnyResult = true)
    {
        Query = EditingQuery.Clone();

        // for Page.OnAppearing()
        if (IsBusy || !loadIfAnyResult && Result.Count > 0)
        {
            return;
        }

        IsBusy = true;
        if (showRefreshing)
            IsRefreshing = true;
        var response = await _dataService.Search(Query, CurrentQueryOrderBySetting);
        if (response.Status == System.Net.HttpStatusCode.OK)
        {
            if (isLoadMore)
            {
                Result = new System.Collections.ObjectModel.ObservableCollection<TDataModel>(response.ResponseBody);
            }
            else
            {
                foreach (var item in response.ResponseBody)
                {
                    Result.Add(item);
                }
            }
        }
        else
        {
            if (isLoadMore)
                Query.PageIndex--;
        }
        if (showRefreshing)
            IsRefreshing = false;
        IsBusy = false;
    }

    public void AttachPopupLaunchCommands(
        ICommand launchAdvancedSearchCommand,
        ICommand launchListQuickActionsCommand,
        ICommand listOrderBysLaunchCommand,
        ICommand launchItemPopupViewCommand)
    {
        AdvancedSearchLaunchCommand = launchAdvancedSearchCommand;
        ListQuickActionsLaunchCommand = launchListQuickActionsCommand;
        ListOrderBysLaunchCommand = listOrderBysLaunchCommand;
        LaunchItemPopupViewCommand = launchItemPopupViewCommand;
    }

    public void AttachAdvancedSearchPopupCommands(
        ICommand cancelCommand)
    {
        AdvancedSearchCancelCommand = new Command(() =>
        {
            EditingQuery = Query.Clone();// put back original Query if AdvancedSearch cancelled
            cancelCommand.Execute(null);
        });
        AdvancedSearchConfirmCommand = new Command(async () =>
        {
            EditingQuery.PageIndex = 1;
            await DoSearch(true, true);
            cancelCommand.Execute(null);
        });
    }

    public void AttachListQuickActionsPopupCommands(
        ICommand cancelCommand)
    {
        ListQuickActionsCancelCommand = cancelCommand;
    }

    public void AttachListOrderBysPopupCommand(
        ICommand cancelCommand)
    {
        // TODO: add ListOrderByChangedCommand;
        ListOrderByChangedCommand = new Command<Framework.MauiX.DataModels.ObservableQueryOrderBySetting>(async (orderby) => {
            if (orderby == null)
                return;
            if(CurrentQueryOrderBySetting == null)
            {
                CurrentQueryOrderBySetting = orderby;
                orderby.Direction = Framework.Models.QueryOrderDirections.Ascending;
            }
            else if(orderby.PropertyName == CurrentQueryOrderBySetting.PropertyName)
            {
                // Toggle if same property changed?
                CurrentQueryOrderBySetting.Direction = CurrentQueryOrderBySetting.Direction == Framework.Models.QueryOrderDirections.Ascending ? Framework.Models.QueryOrderDirections.Descending : Framework.Models.QueryOrderDirections.Ascending;
            }
            else
            {
                CurrentQueryOrderBySetting = orderby;
                orderby.Direction = Framework.Models.QueryOrderDirections.Ascending;
            }
            if(QueryOrderBySettings != null && QueryOrderBySettings.Any(t=>t.IsSelected))
            {
                var selectedOrderBys = QueryOrderBySettings.Where(t => t.IsSelected && t.PropertyName != CurrentQueryOrderBySetting.PropertyName);
                foreach(var selectedOrderBy in selectedOrderBys)
                {
                    selectedOrderBy.IsSelected = false;
                }
                CurrentQueryOrderBySetting.IsSelected = true;
            }

            // TODO: should do a search here
            await DoSearch(false, true, true);

            // Close ListOrderBysPopup
            cancelCommand.Execute(null);
        });
        ListOrderBysCancelCommand = cancelCommand;
    }

    public abstract void RegisterRequestSelectedItemMessage();

    public static void RegisterRequestSelectedItemMessage<TListVM>(TListVM listVM)
        where TListVM : ListVMBase<TAdvancedQuery, TIdentifier, TDataModel, TDataService, TDataChangedMessage, TItemRequestMessage>
    {
        WeakReferenceMessenger.Default.Register<TListVM, TItemRequestMessage>(
            listVM, (r, m) =>
            {
                m.Reply(listVM.SelectedItem.Clone());
                WeakReferenceMessenger.Default.Unregister<TItemRequestMessage>(listVM);
            });
    }

    public abstract void RegisterItemDataChangedMessage();
    public static void RegisterItemDataChangedMessage<TListVM>(TListVM listVM)
        where TListVM : ListVMBase<TAdvancedQuery, TIdentifier, TDataModel, TDataService, TDataChangedMessage, TItemRequestMessage>
    {
        WeakReferenceMessenger.Default.Register<TListVM, TDataChangedMessage>(
            listVM, (r, m) =>
            {
                if (m.ItemView == ViewItemTemplates.Delete)
                {
                    listVM.Result.Remove(listVM.SelectedItem);
                }
                else if (m.ItemView == ViewItemTemplates.Create)
                {
                    listVM.Result.Add(m.Value);
                }
                else if (m.ItemView == ViewItemTemplates.Edit)
                {
                    m.Value.CopyTo(listVM.SelectedItem);
                }

                WeakReferenceMessenger.Default.Unregister<TDataChangedMessage>(listVM);
            });
    }
}

