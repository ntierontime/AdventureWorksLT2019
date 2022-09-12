using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Framework.MauiX.ViewModels
{
    public abstract class ListVMBase<TQuery, TDataModel>: ObservableObject 
        where TQuery : class, new()
        where TDataModel : class
    {
        private TQuery m_Query = new TQuery();
        public TQuery Query
        {
            get => m_Query;
            set => SetProperty(ref m_Query, value);
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

        public ObservableCollection<Framework.MauiX.DataModels.ObservableQueryOrderBySetting> QueryOrderBySettings { get; protected set; } = new();
        private Framework.MauiX.DataModels.ObservableQueryOrderBySetting m_CurrentQueryOrderBySetting;
        public Framework.MauiX.DataModels.ObservableQueryOrderBySetting CurrentQueryOrderBySetting
        {
            get => m_CurrentQueryOrderBySetting;
            set => SetProperty(ref m_CurrentQueryOrderBySetting, value);
        }

        public ICommand TextSearchCommand { get; protected set; }
        public ICommand LaunchAdvancedSearchCommand { get; protected set; }
        public ICommand ApplyAdvancedSearchCommand { get; protected set; }
        public ICommand LoadMoreCommand { get; protected set; }

        protected abstract Task DoSearch(bool clearExisting);
    }
}

