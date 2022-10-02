using Framework.Models;
using Framework.MauiX.DataModels;
using Framework.MauiX.ComponentModels;
using Framework.MauiX.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Framework.MauiX.ViewModels;

public abstract class ListVMBase<TAdvancedQuery, TIdentifier, TDataModel, TDataService, TDataChangedMessage> : ObservableObject
    where TAdvancedQuery : ObservableBaseQuery, IClone<TAdvancedQuery>, new()
    where TDataModel : class, IClone<TDataModel>, ICopyTo<TDataModel>, IGetIdentifier<TIdentifier>
    where TDataService : class, IDataServiceBase<TAdvancedQuery, TIdentifier, TDataModel>
    where TDataChangedMessage : ValueChangedMessageExt<TDataModel>
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

    private ObservableCollection<TDataModel> m_Result = new();
    public ObservableCollection<TDataModel> Result
    {
        get => m_Result;
        set => SetProperty(ref m_Result, value);
    }
    private ObservableCollection<TDataModel> m_SelectedItems = new();
    public ObservableCollection<TDataModel> SelectedItems
    {
        get => m_SelectedItems;
        set => SetProperty(ref m_SelectedItems, value);
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

    private SelectionMode m_CurrentSelectionMode = SelectionMode.Single;
    public SelectionMode CurrentSelectionMode
    {
        get => m_CurrentSelectionMode;
        set => SetProperty(ref m_CurrentSelectionMode, value);
    }
    private string m_CurrentSelectionModeText = "Select";
    public string CurrentSelectionModeText
    {
        get => m_CurrentSelectionModeText;
        set => SetProperty(ref m_CurrentSelectionModeText, value);
    }

    public ICommand TextSearchCommand { get; protected set; }
    public ICommand AdvancedSearchLaunchCommand { get; protected set; }
    public ICommand AdvancedSearchConfirmCommand { get; protected set; }
    public ICommand AdvancedSearchCancelCommand { get; protected set; }

    public ICommand ListOrderBysLaunchCommand { get; protected set; }
    public ICommand ListOrderByChangedCommand { get; protected set; }
    public ICommand ListOrderBysCancelCommand { get; protected set; }

    public ICommand ListQuickActionsLaunchCommand { get; protected set; }
    public ICommand ListQuickActionsCancelCommand { get; protected set; }

    public ICommand LoadMoreCommand { get; protected set; }
    public ICommand RefreshCommand { get; protected set; }

    public ICommand LaunchCreatePopupCommand { get; protected set; }
    public ICommand LaunchDeletePopupCommand { get; protected set; }
    public ICommand LaunchDetailsPopupCommand { get; protected set; }
    public ICommand LaunchEditPopupCommand { get; protected set; }

    public ICommand LaunchCreatePageCommand { get; protected set; }
    public ICommand LaunchDeletePageCommand { get; protected set; }
    public ICommand LaunchDetailsPageCommand { get; protected set; }
    public ICommand LaunchEditPageCommand { get; protected set; }

    public ICommand ToggleSelectModeCommand { get; protected set; }
    public ICommand ClearSelectedItemsCommand { get; protected set; }
    public ICommand SelectionChangedCommand { get; protected set; }

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

        ToggleSelectModeCommand = new Command(() =>
        {
            CurrentSelectionMode = CurrentSelectionMode == SelectionMode.Single ? SelectionMode.Multiple : SelectionMode.Single;
            CurrentSelectionModeText = CurrentSelectionMode == SelectionMode.Single ? "Select" : "Done";
        });

        // TODO: this is a workaround of SelectedItems binding not working
        // https://github.com/dotnet/maui/issues/8435
        // SelectedItems="{Binding Path=SelectedItems, Mode=TwoWay}"
        //ClearSelectedItemsCommand = new Command(() =>
        //{
        //    SelectedItems.Clear();
        //});

        // TODO: this is a workaround of SelectedItems binding not working
        // https://github.com/dotnet/maui/issues/8435
        // SelectedItems="{Binding Path=SelectedItems, Mode=TwoWay}"
        SelectionChangedCommand = new Command<IList<object>>(
            (selectItems) =>
            {
                if (selectItems == null || selectItems.Count == 0)
                {
                    SelectedItems.Clear();
                }
                else
                {
                    var typedSelectItems = selectItems.Select(t=> t as TDataModel);
                    SelectedItems = new ObservableCollection<TDataModel>(typedSelectItems);
                }
                RefreshMultiSelectCommandsCanExecute();
            }
        );

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
            if (!isLoadMore)
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

    public virtual void RefreshMultiSelectCommandsCanExecute()
    {
        ((Command)ClearSelectedItemsCommand).ChangeCanExecute();
    }

    public bool EnableMultiSelectCommands()
    {
        return SelectedItems != null && SelectedItems.Count > 0;
    }
    /// <summary>
    /// TODO: this is a workaround of SelectedItems binding not working
    /// https://github.com/dotnet/maui/issues/8435
    /// SelectedItems="{Binding Path=SelectedItems, Mode=TwoWay}"
    /// </summary>
    public void AttachClearSelectedItemsCommand(Command clearSelectedItemsCommand)
    {
        ClearSelectedItemsCommand = clearSelectedItemsCommand;
    }

    public async void AttachAdvancedSearchPopupCommands(
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

#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
        LoadCodeListsIfAny();
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
    }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    protected virtual async Task LoadCodeListsIfAny()
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
    {
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
        ListOrderByChangedCommand = new Command<ObservableQueryOrderBySetting>(async (orderby) => {
            if (orderby == null)
                return;
            if(CurrentQueryOrderBySetting == null)
            {
                CurrentQueryOrderBySetting = orderby;
                orderby.Direction = QueryOrderDirections.Ascending;
            }
            else if(orderby.PropertyName == CurrentQueryOrderBySetting.PropertyName)
            {
                // Toggle if same property changed?
                CurrentQueryOrderBySetting.Direction = CurrentQueryOrderBySetting.Direction == QueryOrderDirections.Ascending ? QueryOrderDirections.Descending : QueryOrderDirections.Ascending;
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

    public abstract void RegisterItemDataChangedMessage();
    public static void RegisterItemDataChangedMessage<TListVM>(TListVM listVM)
        where TListVM : ListVMBase<TAdvancedQuery, TIdentifier, TDataModel, TDataService, TDataChangedMessage>
    {
        WeakReferenceMessenger.Default.Register<TListVM, TDataChangedMessage>(
            listVM, (r, m) =>
            {
                if (m.ItemView == ViewItemTemplates.Delete)
                {
                    var theItem = listVM.Result.FirstOrDefault(t => t.GetIdentifier().Equals(m.Value.GetIdentifier()));
                    if (theItem != null)
                    {
                        listVM.Result.Remove(theItem);
                    }
                }
                else if (m.ItemView == ViewItemTemplates.Create)
                {
                    listVM.Result.Add(m.Value);
                }
                else if (m.ItemView == ViewItemTemplates.Edit)
                {
                    var theItem = listVM.Result.FirstOrDefault(t => t.GetIdentifier().Equals(m.Value.GetIdentifier()));
                    if (theItem != null)
                    {
                        m.Value.CopyTo(theItem);
                    }
                }
            });
    }
}

