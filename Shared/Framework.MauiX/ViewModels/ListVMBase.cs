using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace Framework.MauiX.ViewModels
{
    public class ListVMBase<TCriteria, TDataModel>: ObservableObject 
        where TCriteria : class
        where TDataModel : class
    {
        private TCriteria m_Criteria;
        public TCriteria Criteria
        {
            get => m_Criteria;
            set => SetProperty(ref m_Criteria, value);
        }

        protected ObservableCollection<TDataModel> m_Result = new ObservableCollection<TDataModel>();
        public ObservableCollection<TDataModel> Result
        {
            get => m_Result;
            set => SetProperty(ref m_Result, value);
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

