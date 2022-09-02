using System.Text.Json;
using System.Text.Json.Serialization;

namespace Framework.MauiX.Services
{
    public class SecureStorageService
    {
        #region 1. CacheSignInData

        public const string Key_SignInData = "SignInData";
        private const string EncryptionKey = "!whatever!";

        public async Task<Framework.MauiX.DataModels.SignInData> GetSignInData()
        {
            var signInData = await GetSerializedData<Framework.MauiX.DataModels.SignInData>(Key_SignInData);

            if (!string.IsNullOrEmpty(signInData.UserName))
            {
                string encryptedUserName = signInData.UserName;
                signInData.UserName = encryptedUserName.Decrypt(EncryptionKey);
            }
            if (!string.IsNullOrEmpty(signInData.Password))
            {
                string encryptedPassword = signInData.Password;
                signInData.Password = encryptedPassword.Decrypt(EncryptionKey);
            }

            return signInData;
        }

        public async Task<Framework.MauiX.DataModels.SignInData> SetSignInData(Framework.MauiX.DataModels.SignInData signInData)
        {
            string encryptedUsername = !string.IsNullOrEmpty(signInData.UserName) ? signInData.UserName.Encrypt(EncryptionKey) : null;
            string encryptedPassword = !string.IsNullOrEmpty(signInData.Password) ? signInData.Password.Encrypt(EncryptionKey): null;

            var newSignInData = new Framework.MauiX.DataModels.SignInData
            {
                UserName = encryptedUsername,
                Password = encryptedPassword,
                Token = signInData.Token,
                ShortGuid = signInData.ShortGuid,
                LastLoggedInDateTime = signInData.LastLoggedInDateTime,
                TokenExpireDateTime = signInData.TokenExpireDateTime,
                GoToWelcomeWizard = signInData.GoToWelcomeWizard,
            };

            await SaveSerializedData(Key_SignInData, newSignInData);
            return newSignInData;
        }

        public void ClearSignInData()
        {
            SecureStorage.Default.Remove(Key_SignInData);
        }

        #endregion 1. CacheSignInData

        #region 2. WelcomeWizardData

        //public const string Key_WelcomeWizardData = "WelcomeWizardData";

        //public Framework.Models.WizardData GetWelcomeWizardData()
        //{
        //    var wizardData = GetData<Framework.Models.WizardData>(Key_WelcomeWizardData);

        //    return wizardData;
        //}

        //public async Task SetWelcomeWizardData(bool completed, string currentItemName, List<Framework.Models.WizardDataItem> wizardDataItems)
        //{
        //    var wizardData = new Framework.Models.WizardData
        //    {
        //        Completed = completed
        //        ,
        //        CurrentItemName = currentItemName
        //        ,
        //        Items = wizardDataItems
        //    };

        //    await SaveData(Key_WelcomeWizardData, wizardData);
        //}

        //public void ClearWelcomeWizardData()
        //{
        //    Application.Current.Properties.Remove(Key_WelcomeWizardData);
        //}
        //public bool HasWelcomeWizardData()
        //{
        //    return HasData(Key_WelcomeWizardData);
        //}

        #endregion 2. WelcomeWizardData

        #region 3. TableCachingData

        //public const string Prefix_TableCachingData = "TCD_";

        //public Framework.Xaml.TableCachingItem GetTableCachingData(string tableName)
        //{
        //    var data = GetData<Framework.Xaml.TableCachingItem>(Prefix_TableCachingData + tableName);
        //    if (string.IsNullOrEmpty(data.TableName))
        //    {
        //        data.TableName = tableName;
        //    }

        //    return data;
        //}

        //public async Task SetTableCachingData(string tableName, DateTime syncDateTime, bool shouldRefresh)
        //{
        //    var data = GetTableCachingData(tableName);
        //    data.TableName = tableName; data.SyncDateTime = syncDateTime; data.ShouldRefresh = shouldRefresh;

        //    await SaveData(Prefix_TableCachingData + tableName, data);
        //}

        //public async Task SetTableUIListSetting(string tableName, bool bindToGroupedResults, List<Framework.Queries.QueryOrderBySetting> queryOrderBySettings)
        //{
        //    var data = GetTableCachingData(tableName);
        //    data.TableName = tableName; data.UIListBindToGroupedResults = bindToGroupedResults; data.UIListQueryOrderBySettings = queryOrderBySettings;

        //    await SaveData(Prefix_TableCachingData + tableName, data);
        //}

        //public void ClearTableCachingData(string tableName)
        //{
        //    Application.Current.Properties.Remove(Prefix_TableCachingData + tableName);
        //}
        //public bool HasTableCachingData(string tableName)
        //{
        //    return HasData(Prefix_TableCachingData + tableName);
        //}

        #endregion 3. TableCachingData

        #region 4. Themes

        public const string Key_Theme = "Theme";

        public async Task<AppTheme> GetCurrentTheme()
        {
            var currentThemeInSecureStorage = await SecureStorage.Default.GetAsync(Key_Theme);
            if (!string.IsNullOrEmpty(currentThemeInSecureStorage) && Enum.TryParse<AppTheme>(currentThemeInSecureStorage, out AppTheme currentTheme))
            {
                return currentTheme;
            }
            await SecureStorage.Default.SetAsync(Key_Theme, AppTheme.Light.ToString());
            return AppTheme.Light;
        }

        public async Task SetTheme(AppTheme theme)
        {
            await SecureStorage.Default.SetAsync(Key_Theme, theme.ToString());
        }

        #endregion 4. Themes

        #region z. Generic<T> Get or Save

        public void ClearAll()
        {
            SecureStorage.Default.RemoveAll();
        }

        public async Task SaveSerializedData(string key, object data)
        {
            await SecureStorage.Default.SetAsync(key, JsonSerializer.Serialize(data));
        }

        public async Task<T> GetSerializedData<T>(string key)
        {
            var serializedData = await SecureStorage.Default.GetAsync(key);
            if(string.IsNullOrEmpty(serializedData))
            {
                return default;
            }
            try
            {
                return JsonSerializer.Deserialize<T>(serializedData);
            }
            catch
            {
                return default;
            }
        }

        #endregion z. Generic<T> Get or Save
    }
}

