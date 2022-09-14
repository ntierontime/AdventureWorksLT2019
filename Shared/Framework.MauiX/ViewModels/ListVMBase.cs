using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Framework.MauiX.ViewModels;

public abstract class ListVMBase<TAdvancedQuery, TIdentifier, TDataModel, TDataService, TDataChangedMessage, TItemRequestMessage> : ObservableObject 
    where TAdvancedQuery : Framework.MauiX.DataModels.ObservableBaseQuery, new()
    where TDataModel : class
    where TDataService : class, Framework.MauiX.Services.IDataServiceBase<TAdvancedQuery, TIdentifier, TDataModel>
    where TDataChangedMessage : Framework.MauiX.ComponentModels.ValueChangedMessageExt<TDataModel>
    where TItemRequestMessage : RequestMessage<TDataModel>, new()
{
    private TAdvancedQuery m_Query = new();
    public TAdvancedQuery Query
    {
        get => m_Query;
        set => SetProperty(ref m_Query, value);
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

    public ObservableCollection<Framework.MauiX.DataModels.ObservableQueryOrderBySetting> QueryOrderBySettings { get; protected set; } = new();
    private Framework.MauiX.DataModels.ObservableQueryOrderBySetting m_CurrentQueryOrderBySetting;
    public Framework.MauiX.DataModels.ObservableQueryOrderBySetting CurrentQueryOrderBySetting
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

    public ICommand LoadMoreCommand { get; protected set; }
    public ICommand RefreshCommand { get; protected set; }
    
    public ICommand LaunchItemPopupViewCommand { get; protected set; }
    public ICommand LaunchItemPageCommand { get; protected set; }

    protected readonly TDataService _dataService;

    public ListVMBase(TDataService dataService)
    {
        _dataService = dataService;

        QueryOrderBySettings = new System.Collections.ObjectModel.ObservableCollection<Framework.MauiX.DataModels.ObservableQueryOrderBySetting>(
            _dataService.GetQueryOrderBySettings());
        CurrentQueryOrderBySetting = QueryOrderBySettings.First(t => t.IsSelected);

        TextSearchCommand = new Command<string>(async (text) =>
        {
            if (Query.TextSearch != text)
            {
                Query.TextSearch = text;
                Query.PageIndex = 1;
                await DoSearch(true, true); // clear existing
            }
        });

        LoadMoreCommand = new Command(async () =>
        {
            Query.PageIndex++;
            await DoSearch(false, false); // keep existing
        });

        RefreshCommand = new Command(async () =>
        {
            Query.PageIndex = 1;
            await DoSearch(true, true);
        });

        RegisterRequestSelectedItemMessage();
    }

    public virtual async Task DoSearch(bool isLoadMore, bool showRefreshing)
    {
        if(showRefreshing)
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
            if(isLoadMore)
                Query.PageIndex--;
        }
        if (showRefreshing)
            IsRefreshing = false;
    }

    public void AttachPopupLaunchCommands(
        ICommand launchAdvancedSearchCommand,
        ICommand launchListQuickActionsCommand,
        ICommand launchItemPopupViewCommand)
    {
        AdvancedSearchLaunchCommand = launchAdvancedSearchCommand;
        ListQuickActionsLaunchCommand = launchListQuickActionsCommand;
        LaunchItemPopupViewCommand = launchItemPopupViewCommand;
    }

    public void AttachAdvancedSearchPopupCommands(
        ICommand cancelCommand)
    {
        AdvancedSearchCancelCommand = cancelCommand;
        AdvancedSearchConfirmCommand = new Command(async () =>
        {
            Query.PageIndex = 1;
            await DoSearch(true, true);
            AdvancedSearchCancelCommand.Execute(null);
        });
    }

    public void AttachListQuickActionsPopupCommands(
        ICommand cancelCommand)
    {
        ListQuickActionsCancelCommand = cancelCommand;
    }

    public abstract void RegisterRequestSelectedItemMessage();

    public static void RegisterRequestSelectedItemMessage<TListVM>(TListVM listVM)
        where TListVM : ListVMBase<TAdvancedQuery, TIdentifier, TDataModel, TDataService, TDataChangedMessage, TItemRequestMessage>
    {
        WeakReferenceMessenger.Default.Register<TListVM, TItemRequestMessage>(
            listVM, (r, m) =>
            {
                m.Reply(listVM.SelectedItem);
                WeakReferenceMessenger.Default.Unregister<TItemRequestMessage>(listVM);
            });
    }

    public abstract void RegisterItemDataChangedMessage();
    public static void RegisterItemDataChangedMessage<TListVM>(TListVM listVM)
        where TListVM: ListVMBase<TAdvancedQuery, TIdentifier, TDataModel, TDataService, TDataChangedMessage, TItemRequestMessage>
    {
        WeakReferenceMessenger.Default.Register<TListVM, TDataChangedMessage>(
            listVM, (r, m) =>
            {
                if (m.ItemView == Framework.Models.ViewItemTemplates.Delete)
                {
                    listVM.Result.Remove(listVM.SelectedItem);
                }
                else if (m.ItemView == Framework.Models.ViewItemTemplates.Create)
                {
                    listVM.Result.Add(m.Value);
                }
                else if (m.ItemView == Framework.Models.ViewItemTemplates.Edit)
                {

                }

                WeakReferenceMessenger.Default.Unregister<TDataChangedMessage>(listVM);
            });
    }
}
