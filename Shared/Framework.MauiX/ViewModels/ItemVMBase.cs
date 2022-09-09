using CommunityToolkit.Mvvm.ComponentModel;

namespace Framework.MauiX.ViewModels
{
    public class ItemVMBase<TIdentifier, TDataModel>: ObservableObject
        where TIdentifier : class
        where TDataModel : class
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
    }
}

