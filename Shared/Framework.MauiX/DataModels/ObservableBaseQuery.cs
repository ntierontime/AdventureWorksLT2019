using CommunityToolkit.Mvvm.ComponentModel;

namespace Framework.MauiX.DataModels;

public class ObservableBaseQuery: ObservableObject
{
    private string m_TextSearch;
    /// <summary>
    /// will query all text columns in this table, or developer can develop a customized pattern to recognize/search in more fields
    /// </summary>
    public string TextSearch
    {
        get => m_TextSearch;
        set => SetProperty(ref m_TextSearch, value);
    }

    private Framework.Models.TextSearchTypes m_TextSearchType;
    public Framework.Models.TextSearchTypes TextSearchType
    {
        get => m_TextSearchType;
        set => SetProperty(ref m_TextSearchType, value);
    }

    private int m_PageSize = 10;
    public int PageSize
    {
        get => m_PageSize;
        set => SetProperty(ref m_PageSize, value);
    }

    private int m_PageIndex = 1;
    public int PageIndex
    {
        get => m_PageIndex;
        set => SetProperty(ref m_PageIndex, value);
    }

    private string m_OrderBys;
    public string OrderBys
    {
        get => m_OrderBys;
        set => SetProperty(ref m_OrderBys, value);
    }

    private Framework.Models.PaginationOptions m_PaginationOption;
    public Framework.Models.PaginationOptions PaginationOption
    {
        get => m_PaginationOption;
        set => SetProperty(ref m_PaginationOption, value);
    }
}
