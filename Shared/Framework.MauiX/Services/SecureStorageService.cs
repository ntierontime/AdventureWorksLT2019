using Framework.MauiX.DataModels;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Framework.MauiX.Services;

public class SecureStorageService
{
    #region 1. CacheSignInData

    public const string Key_SignInData = "SignInData";
    private const string EncryptionKey = "!whatever!";

    public async Task<SignInData> GetSignInData()
    {
        var signInData = await GetSerializedData<SignInData>(Key_SignInData);

        if (signInData == null)
            return new SignInData();

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

    public async Task<SignInData> SetSignInData(SignInData signInData)
    {
        string encryptedUsername = !string.IsNullOrEmpty(signInData.UserName) ? signInData.UserName.Encrypt(EncryptionKey) : null;
        string encryptedPassword = !string.IsNullOrEmpty(signInData.Password) ? signInData.Password.Encrypt(EncryptionKey): null;

        var newSignInData = new SignInData
        {
            UserName = encryptedUsername,
            Password = encryptedPassword,
            Token = signInData.Token,
            ShortGuid = signInData.ShortGuid,
            LastLoggedInDateTime = signInData.LastLoggedInDateTime,
            TokenExpireDateTime = signInData.TokenExpireDateTime,
            //GoToWelcomeWizard = signInData.GoToWelcomeWizard,
        };

        await SaveSerializedData(Key_SignInData, newSignInData);
        return newSignInData;
    }

    public void ClearSignInData()
    {
        SecureStorage.Default.Remove(Key_SignInData);
    }

    #endregion 1. CacheSignInData

    #region z. Generic<T> Get or Save

    public void ClearAll()
    {
        SecureStorage.Default.RemoveAll();
    }

    public async Task SaveSerializedData<T>(string key, T data)
    {
        var serialized = JsonSerializer.Serialize<T>(data);
        await SecureStorage.Default.SetAsync(key, serialized);
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

