
namespace Framework.MauiX.Helpers
{
    public class ApplicationPropertiesHelper
    {
        #region 1. CacheSignInData

        public const string Key_SignInData = "SignInData";
        private const string EncryptionKey = "!whatever!";

        public static Framework.MauiX.DataModels.SignInData GetSignInData()
        {
            var signInData = GetData<Framework.MauiX.DataModels.SignInData>(Key_SignInData);

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

            if (string.IsNullOrEmpty(signInData.UserName) || string.IsNullOrEmpty(signInData.Password))
            {
                signInData.AutoSignIn = false;
                signInData.RememberMe = false;
            }

            return signInData;
        }

        public static async Task SetSignInData(string userName, string password, bool rememberMe, bool autoSignIn, string token, long entityID, bool goToWelcomeWizard, string shortGuid)
        {
            string encryptedUsername = !string.IsNullOrEmpty(userName) ? userName.Encrypt(EncryptionKey) : null;
            string encryptedPassword = rememberMe && !string.IsNullOrEmpty(password) ? password?.Encrypt(EncryptionKey): null;

            var _sSignInData = new Framework.MauiX.DataModels.SignInData
            {
                UserName = encryptedUsername,
                Password = encryptedPassword,
                RememberMe = rememberMe,
                AutoSignIn = autoSignIn,
                Token = token,
                EntityID = entityID,
                ShortGuid = shortGuid,
                GoToWelcomeWizard = goToWelcomeWizard,
            };

            await SaveData(Key_SignInData, _sSignInData);
        }

        public static async Task ClearSignInData()
        {
            Application.Current.Properties.Remove(Key_SignInData);
            await Application.Current.SavePropertiesAsync();
        }

        #endregion 1. CacheSignInData

        #region 2. WelcomeWizardData

        public const string Key_WelcomeWizardData = "WelcomeWizardData";

        public static Framework.Models.WizardData GetWelcomeWizardData()
        {
            var wizardData = GetData<Framework.Models.WizardData>(Key_WelcomeWizardData);

            return wizardData;
        }

        public static async Task SetWelcomeWizardData(bool completed, string currentItemName, List<Framework.Models.WizardDataItem> wizardDataItems)
        {
            var wizardData = new Framework.Models.WizardData
            {
                Completed = completed
                ,
                CurrentItemName = currentItemName
                ,
                Items = wizardDataItems
            };

            await SaveData(Key_WelcomeWizardData, wizardData);
        }

        public static void ClearWelcomeWizardData()
        {
            Application.Current.Properties.Remove(Key_WelcomeWizardData);
        }
        public static bool HasWelcomeWizardData()
        {
            return HasData(Key_WelcomeWizardData);
        }

        #endregion 2. WelcomeWizardData

        #region 3. TableCachingData

        public const string Prefix_TableCachingData = "TCD_";

        public static Framework.Xaml.TableCachingItem GetTableCachingData(string tableName)
        {
            var data = GetData<Framework.Xaml.TableCachingItem>(Prefix_TableCachingData + tableName);
            if (string.IsNullOrEmpty(data.TableName))
            {
                data.TableName = tableName;
            }

            return data;
        }

        public static async Task SetTableCachingData(string tableName, DateTime syncDateTime, bool shouldRefresh)
        {
            var data = GetTableCachingData(tableName);
            data.TableName = tableName; data.SyncDateTime = syncDateTime; data.ShouldRefresh = shouldRefresh;

            await SaveData(Prefix_TableCachingData + tableName, data);
        }

        public static async Task SetTableUIListSetting(string tableName, bool bindToGroupedResults, List<Framework.Queries.QueryOrderBySetting> queryOrderBySettings)
        {
            var data = GetTableCachingData(tableName);
            data.TableName = tableName; data.UIListBindToGroupedResults = bindToGroupedResults; data.UIListQueryOrderBySettings = queryOrderBySettings;

            await SaveData(Prefix_TableCachingData + tableName, data);
        }

        public static void ClearTableCachingData(string tableName)
        {
            Application.Current.Properties.Remove(Prefix_TableCachingData + tableName);
        }
        public static bool HasTableCachingData(string tableName)
        {
            return HasData(Prefix_TableCachingData + tableName);
        }

        #endregion 3. TableCachingData

        #region 4. Themes

        public const string Key_Theme = "Theme";

        public static Framework.Themes.Theme GetTheme()
        {
            if (Application.Current.Properties.ContainsKey(Key_Theme))
            {
                try
                {
                    object cachedObject = Application.Current.Properties[Key_Theme];
                    if (cachedObject is string s)
                    {
                        if (!string.IsNullOrEmpty(s))
                        {
                            return (Framework.Themes.Theme)Enum.Parse(typeof(Framework.Themes.Theme), s);
                        }
                    }
                }
                catch //(Exception ex)
                { }
            }
            return Framework.Themes.Theme.Light;
        }

        public static async Task SetTheme(Framework.Themes.Theme theme)
        {
            if (theme == Framework.Themes.Theme.Light)
            {
                if (Application.Current.Properties.ContainsKey(Key_Theme))
                {
                    Application.Current.Properties.Remove(Key_Theme);
                    await Application.Current.SavePropertiesAsync();
                }
                return;
            }

            Application.Current.Properties[Key_Theme] = theme.ToString();
            await Application.Current.SavePropertiesAsync();
        }

        #endregion 4. Themes

        public static async Task ClearAll()
        {
            //var allKeys = Application.Current.Properties.Keys.ToArray();
            Application.Current.Properties.Clear();
            await Application.Current.SavePropertiesAsync();
        }

        #region z. Generic<T> Get or Save

        public static async Task SaveData(string key, object data)
        {
            string serializedData = JsonConvert.SerializeObject(data);
            Application.Current.Properties[key] = serializedData;
            await Application.Current.SavePropertiesAsync();
        }

        public static T GetData<T>(string key)
        {
            if (Application.Current.Properties.ContainsKey(key))
            {
                try
                {
                    object cachedObject = Application.Current.Properties[key];
                    if (cachedObject is string s)
                    {
                        if (!string.IsNullOrEmpty(s))
                        {
                            cachedObject = JsonConvert.DeserializeObject<T>(s);
                            Type typeParameterType = typeof(T);
                            if (cachedObject.GetType() == typeParameterType)
                                return (T)cachedObject;
                        }
                    }
                }
                catch //(Exception ex)
                {
                    Application.Current.Properties.Remove(key);
                }
            }
            return default(T);
        }
        public static bool HasData(string key)
        {
            return Application.Current.Properties.ContainsKey(key);
        }
        #endregion z. Generic<T> Get or Save
    }
}

