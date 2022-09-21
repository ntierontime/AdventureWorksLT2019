using Framework.Models;
using AdventureWorksLT2019.Resx.Resources;
using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;
using System.ComponentModel.DataAnnotations;

namespace AdventureWorksLT2019.MauiXApp.DataModels;

public class ErrorLogDataModel : ObservableValidator, IClone<ErrorLogDataModel>, ICopyTo<ErrorLogDataModel>
{
    public string Avatar__
    {
        get => GetAvatar();
    }

    private string GetAvatar()
    {
        if (string.IsNullOrEmpty(UserName) || UserName.Length == 0)
            return "?";
        if (UserName.Length == 1)
            return UserName.Substring(0, 1);
        return UserName.Substring(0, 2);
    }

    protected ItemUIStatus m_ItemUIStatus______;
    public ItemUIStatus ItemUIStatus______
    {
        get => m_ItemUIStatus______;
        set => SetProperty(ref m_ItemUIStatus______, value);
    }
    protected System.Boolean m_IsDeleted______;
    public System.Boolean IsDeleted______
    {
        get => m_IsDeleted______;
        set => SetProperty(ref m_IsDeleted______, value);
    }

    protected int m_ErrorLogID;
    [Display(Name = "ErrorLogID", ResourceType = typeof(UIStrings))]
    [PrimaryKey]
    public int ErrorLogID
    {
        get => m_ErrorLogID;
        set
        {
            SetProperty(ref m_ErrorLogID, value);
        }
    }

    protected System.DateTime m_ErrorTime;
    [Display(Name = "ErrorTime", ResourceType = typeof(UIStrings))]
    [DataType(DataType.DateTime)]
    [Required(ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="ErrorTime_is_required")]
    public System.DateTime ErrorTime
    {
        get => m_ErrorTime;
        set
        {
            SetProperty(ref m_ErrorTime, value);
        }
    }

    protected string m_UserName;
    [Display(Name = "UserName", ResourceType = typeof(UIStrings))]
    [StringLength(128, ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="The_length_of_UserName_should_be_1_to_128", MinimumLength = 1)]
    public string UserName
    {
        get => m_UserName;
        set
        {
            SetProperty(ref m_UserName, value);
            OnPropertyChanged(nameof(Avatar__));
        }
    }

    protected int m_ErrorNumber;
    [Display(Name = "ErrorNumber", ResourceType = typeof(UIStrings))]
    [Required(ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="ErrorNumber_is_required")]
    public int ErrorNumber
    {
        get => m_ErrorNumber;
        set
        {
            SetProperty(ref m_ErrorNumber, value);
        }
    }

    protected int? m_ErrorSeverity;
    [Display(Name = "ErrorSeverity", ResourceType = typeof(UIStrings))]
    public int? ErrorSeverity
    {
        get => m_ErrorSeverity;
        set
        {
            SetProperty(ref m_ErrorSeverity, value);
        }
    }

    protected int? m_ErrorState;
    [Display(Name = "ErrorState", ResourceType = typeof(UIStrings))]
    public int? ErrorState
    {
        get => m_ErrorState;
        set
        {
            SetProperty(ref m_ErrorState, value);
        }
    }

    protected string m_ErrorProcedure;
    [Display(Name = "ErrorProcedure", ResourceType = typeof(UIStrings))]
    [StringLength(126, ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="The_length_of_ErrorProcedure_should_be_0_to_126")]
    public string ErrorProcedure
    {
        get => m_ErrorProcedure;
        set
        {
            SetProperty(ref m_ErrorProcedure, value);
        }
    }

    protected int? m_ErrorLine;
    [Display(Name = "ErrorLine", ResourceType = typeof(UIStrings))]
    public int? ErrorLine
    {
        get => m_ErrorLine;
        set
        {
            SetProperty(ref m_ErrorLine, value);
        }
    }

    protected string m_ErrorMessage;
    [Display(Name = "ErrorMessage", ResourceType = typeof(UIStrings))]
    [StringLength(4000, ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="The_length_of_ErrorMessage_should_be_1_to_4000", MinimumLength = 1)]
    public string ErrorMessage
    {
        get => m_ErrorMessage;
        set
        {
            SetProperty(ref m_ErrorMessage, value);
        }
    }

    public ErrorLogDataModel Clone()
    {
        return new ErrorLogDataModel
        {
            m_ItemUIStatus______ = m_ItemUIStatus______,
            m_IsDeleted______ = m_IsDeleted______,
            m_ErrorLogID = m_ErrorLogID,
            m_ErrorTime = m_ErrorTime,
            m_UserName = m_UserName,
            m_ErrorNumber = m_ErrorNumber,
            m_ErrorSeverity = m_ErrorSeverity,
            m_ErrorState = m_ErrorState,
            m_ErrorProcedure = m_ErrorProcedure,
            m_ErrorLine = m_ErrorLine,
            m_ErrorMessage = m_ErrorMessage,
        };
    }

    public void CopyTo(ErrorLogDataModel destination)
    {
        destination.ItemUIStatus______ = ItemUIStatus______;
        destination.IsDeleted______ = IsDeleted______;
        destination.ErrorLogID = ErrorLogID;
        destination.ErrorTime = ErrorTime;
        destination.UserName = UserName;
        destination.ErrorNumber = ErrorNumber;
        destination.ErrorSeverity = ErrorSeverity;
        destination.ErrorState = ErrorState;
        destination.ErrorProcedure = ErrorProcedure;
        destination.ErrorLine = ErrorLine;
        destination.ErrorMessage = ErrorMessage;
    }
}

