using Framework.Models;
using AdventureWorksLT2019.Resx.Resources;
using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;
using System.ComponentModel.DataAnnotations;

namespace AdventureWorksLT2019.MauiXApp.DataModels;

public class BuildVersionDataModel : ObservableValidator, IClone<BuildVersionDataModel>, ICopyTo<BuildVersionDataModel>, IGetIdentifier<BuildVersionIdentifier>
{
    public string Avatar__
    {
        get => GetAvatar();
    }

    private string GetAvatar()
    {
        if (string.IsNullOrEmpty(Database_Version) || Database_Version.Length == 0)
            return "?";
        if (Database_Version.Length == 1)
            return Database_Version[..1];
        return Database_Version[..2];
    }

    private ItemUIStatus m_ItemUIStatus______;
    public ItemUIStatus ItemUIStatus______
    {
        get => m_ItemUIStatus______;
        set => SetProperty(ref m_ItemUIStatus______, value);
    }
    private System.Boolean m_IsDeleted______;
    public System.Boolean IsDeleted______
    {
        get => m_IsDeleted______;
        set => SetProperty(ref m_IsDeleted______, value);
    }

    private Int32 m___DBId__;
    [PrimaryKey, AutoIncrement]
    public Int32 __DBId__
    {
        get => m___DBId__;
        set => SetProperty(ref m___DBId__, value);
    }

    private byte m_SystemInformationID;
    [Display(Name = "SystemInformationID", ResourceType = typeof(UIStrings))]
    public byte SystemInformationID
    {
        get => m_SystemInformationID;
        set
        {
            SetProperty(ref m_SystemInformationID, value);
        }
    }

    private string m_Database_Version;
    [Display(Name = "Database_Version", ResourceType = typeof(UIStrings))]
    [StringLength(25, ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="The_length_of_Database_Version_should_be_1_to_25", MinimumLength = 1)]
    public string Database_Version
    {
        get => m_Database_Version;
        set
        {
            SetProperty(ref m_Database_Version, value);
            OnPropertyChanged(nameof(Avatar__));
        }
    }

    private System.DateTime m_VersionDate;
    [Display(Name = "VersionDate", ResourceType = typeof(UIStrings))]
    [DataType(DataType.DateTime)]
    [Required(ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="VersionDate_is_required")]
    public System.DateTime VersionDate
    {
        get => m_VersionDate;
        set
        {
            SetProperty(ref m_VersionDate, value);
        }
    }

    private System.DateTime m_ModifiedDate;
    [Display(Name = "ModifiedDate", ResourceType = typeof(UIStrings))]
    [DataType(DataType.DateTime)]
    [Required(ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="ModifiedDate_is_required")]
    public System.DateTime ModifiedDate
    {
        get => m_ModifiedDate;
        set
        {
            SetProperty(ref m_ModifiedDate, value);
        }
    }

    public BuildVersionDataModel Clone()
    {
        return new BuildVersionDataModel
        {
            m_ItemUIStatus______ = m_ItemUIStatus______,
            m_IsDeleted______ = m_IsDeleted______,
            m___DBId__ = m___DBId__,
            m_SystemInformationID = m_SystemInformationID,
            m_Database_Version = m_Database_Version,
            m_VersionDate = m_VersionDate,
            m_ModifiedDate = m_ModifiedDate,
        };
    }

    public void CopyTo(BuildVersionDataModel destination)
    {
        destination.ItemUIStatus______ = ItemUIStatus______;
        destination.IsDeleted______ = IsDeleted______;
        destination.__DBId__ = __DBId__;
        destination.SystemInformationID = SystemInformationID;
        destination.Database_Version = Database_Version;
        destination.VersionDate = VersionDate;
        destination.ModifiedDate = ModifiedDate;
    }

    public BuildVersionIdentifier GetIdentifier()
    {
        return new BuildVersionIdentifier
        {
            SystemInformationID = SystemInformationID,
            VersionDate = VersionDate,
            ModifiedDate = ModifiedDate,
        };
    }
}

