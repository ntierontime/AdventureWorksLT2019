using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;

namespace Framework.MauiX.DataModels
{
    /// <summary>
    /// a SQLite table "CacheDataStatus" will be created.
    /// </summary>
    public class CacheDataStatusItem: ObservableObject
    {
        private string m_Key;
        /// <summary>
        /// The table name if load from serve side
        /// </summary>
        [PrimaryKey]
        public string Key
        {
            get => m_Key;
            set => SetProperty(ref m_Key, value);
        }

        private DateTime? m_LastSyncDateTime = null;
        public DateTime? LastSyncDateTime
        {
            get => m_LastSyncDateTime;
            set => SetProperty(ref m_LastSyncDateTime, value);
        }

        private bool m_UpdatedInServer = true;
        /// <summary>
        /// this flag is from Server side notification
        /// </summary>
        public bool UpdatedInServer
        {
            get => m_UpdatedInServer;
            set => SetProperty(ref m_UpdatedInServer, value);
        }

        private bool m_UpdatedInClient = false;
        /// <summary>
        /// this flag is from client side, when client side CUD action failed
        /// </summary>
        public bool UpdatedInClient
        {
            get => m_UpdatedInClient;
            set => SetProperty(ref m_UpdatedInClient, value);
        }
        private string m_CurrentOrderBy;
        /// <summary>
        /// The table name if load from serve side
        /// </summary>
        public string CurrentOrderBy
        {
            get => m_CurrentOrderBy;
            set => SetProperty(ref m_CurrentOrderBy, value);
        }
    }
}
