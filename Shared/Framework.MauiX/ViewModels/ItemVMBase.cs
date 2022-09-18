using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using System.Windows.Input;

namespace Framework.MauiX.ViewModels;

public abstract class ItemVMBase<TIdentifier, TDataModel, TDataService, TDataChangedMessage, TItemRequestMessage>: ObservableObject
    where TIdentifier : class
    where TDataModel : class, Framework.Models.IClone<TDataModel>
    where TDataService : class, Framework.MauiX.Services.IDataServiceBase<TIdentifier, TDataModel>
    where TDataChangedMessage : Framework.MauiX.ComponentModels.ValueChangedMessageExt<TDataModel>
    where TItemRequestMessage : RequestMessage<TDataModel>, new()
{
    private TIdentifier m_Identifier;
    public TIdentifier Identifier
    {
        get => m_Identifier;
        set => SetProperty(ref m_Identifier, value);
    }

    private TDataModel m_Item;
    public TDataModel Item
    {
        get => m_Item;
        set => SetProperty(ref m_Item, value);
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

    public ICommand EditConfirmCommand { get; protected set; }
    public ICommand EditCancelCommand { get; protected set; }
    public ICommand CreateConfirmCommand { get; protected set; }
    public ICommand CreateCancelCommand { get; protected set; }
    public ICommand DeleteConfirmCommand { get; protected set; }
    public ICommand DeleteCancelCommand { get; protected set; }
    public ICommand DetailsCloseCommand { get; protected set; }

    public ICommand EditPageConfirmCommand { get; protected set; }
    public ICommand EditPageCancelCommand { get; protected set; }
    public ICommand CreatePageConfirmCommand { get; protected set; }
    public ICommand CreatePageCancelCommand { get; protected set; }
    public ICommand DeletePageConfirmCommand { get; protected set; }
    public ICommand DeletePageCancelCommand { get; protected set; }
    public ICommand DetailsPageCloseCommand { get; protected set; }

    protected readonly TDataService _dataService;

    public ItemVMBase(TDataService dataService)
    {
        _dataService = dataService;
    }

    public void RequestItem(Framework.Models.ViewItemTemplates itemView)
    {
        if (itemView == Framework.Models.ViewItemTemplates.Create)
        {
            Item = _dataService.GetDefault();
        }
        else
        {
            var messagge = WeakReferenceMessenger.Default.Send<TItemRequestMessage>();
            Item = messagge.Response.Clone();
        }
    }
    
    public abstract void SendDataChangedMessage(Framework.Models.ViewItemTemplates itemView);

    public void AttachCreateViewCommands(ICommand cancelCommand, object commandParameter = null)
    {
        CreateConfirmCommand = new Command(async () =>
        {
            await _dataService.Create(Item);
            SendDataChangedMessage(Framework.Models.ViewItemTemplates.Create);
            cancelCommand.Execute(commandParameter);
            CreateConfirmCommand = null;
            CreateCancelCommand = null;
        });
        CreateCancelCommand = new Command(() =>
        {
            SendDataChangedMessage(Framework.Models.ViewItemTemplates.Details);
            cancelCommand.Execute(commandParameter);
            CreateConfirmCommand = null;
            CreateCancelCommand = null;
        });
    }

    public void AttachEditViewCommands(ICommand cancelCommand, object commandParameter = null)
    {
        EditConfirmCommand = new Command(async () =>
        {
            await _dataService.Update(Identifier, Item);
            SendDataChangedMessage(Framework.Models.ViewItemTemplates.Edit);
            cancelCommand.Execute(commandParameter);
            EditConfirmCommand = null;
            EditCancelCommand = null;
        });
        EditCancelCommand = new Command(() =>
        {
            SendDataChangedMessage(Framework.Models.ViewItemTemplates.Details);
            cancelCommand.Execute(commandParameter);
            EditConfirmCommand = null;
            EditCancelCommand = null;
        });
    }

    public void AttachDeleteViewCommands(ICommand cancelCommand, object commandParameter = null)
    {
        DeleteConfirmCommand = new Command(async () =>
        {
            await _dataService.Delete(Identifier);
            SendDataChangedMessage(Framework.Models.ViewItemTemplates.Delete);
            cancelCommand.Execute(commandParameter);
            DeleteConfirmCommand = null;
            DeleteCancelCommand = null;
        });
        DeleteCancelCommand = new Command(() =>
        {
            SendDataChangedMessage(Framework.Models.ViewItemTemplates.Details);
            cancelCommand.Execute(commandParameter);
            DeleteConfirmCommand = null;
            DeleteCancelCommand = null;
        });
    }

    public void AttachDetailsViewCommands(ICommand cancelCommand, object commandParameter = null)
    {
        DetailsCloseCommand = new Command(() =>
        {
            cancelCommand.Execute(commandParameter);
            DetailsCloseCommand = null;
        });
    }
}
