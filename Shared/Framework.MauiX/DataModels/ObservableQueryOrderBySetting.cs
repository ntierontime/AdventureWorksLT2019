using CommunityToolkit.Mvvm.ComponentModel;
using Framework.Models;

namespace Framework.MauiX.DataModels;

public class ObservableQueryOrderBySetting: ObservableObject
{
    private string m_PropertyName;
    public string PropertyName
    {
        get => m_PropertyName;
        set => SetProperty(ref m_PropertyName, value);
    }

    private string m_DisplayName;
    public string DisplayName
    {
        get => m_DisplayName;
        set => SetProperty(ref m_DisplayName, value);
    }

    private Framework.Models.QueryOrderDirections m_Direction;
    public Framework.Models.QueryOrderDirections Direction
    {
        get => m_Direction;
        set => SetProperty(ref m_Direction, value);
    }

    private string m_FontIcon;
    public string FontIcon
    {
        get => m_FontIcon;
        set => SetProperty(ref m_FontIcon, value);
    }

    private string m_FontIconFamily;
    public string FontIconFamily
    {
        get => m_FontIconFamily;
        set => SetProperty(ref m_FontIconFamily, value);
    }

    private bool m_IsSelected;
    public bool IsSelected
    {
        get => m_IsSelected;
        set => SetProperty(ref m_IsSelected, value);
    }

    /// <summary>
    /// For SQLite: Func&lt;TableQuery&lt;TItem&gt;, Framework.Models.QueryOrderDirections, TableQuery&lt;TItem&gt;&gt;
    /// </summary>
    public object SortFunc { get; set; }
    public void ToggleDirection()
    {
        Direction = Direction == QueryOrderDirections.Ascending ?
            QueryOrderDirections.Descending : QueryOrderDirections.Ascending;
    }

    public static IEnumerable<QueryOrderBySetting> Parse(string queryOrderByExpression)
    {
        if (string.IsNullOrWhiteSpace(queryOrderByExpression))
            return Enumerable.Empty<QueryOrderBySetting>();
        string[] _Splitted1 = queryOrderByExpression.Split("|".ToCharArray());
        if (_Splitted1 == null || _Splitted1?.Length == 0)
            return Enumerable.Empty<QueryOrderBySetting>();

        var result = new List<QueryOrderBySetting>();
        foreach (string _Splitted1Item in _Splitted1!)
        {
            if (string.IsNullOrWhiteSpace(_Splitted1Item) == false)
            {
                string[] _Splitted2 = _Splitted1Item.Trim().Split("~".ToCharArray());
                if (_Splitted2.Length == 1)
                {
                    result.Add(new QueryOrderBySetting { PropertyName = _Splitted2[0], DisplayName = _Splitted2[0], Direction = QueryOrderDirections.Ascending });
                }
                else if (_Splitted2.Length > 1)
                {
                    QueryOrderDirections _ListSortDirection;
                    if (_Splitted2[1].Trim().ToLower() == "DESC".ToLower())
                    {
                        _ListSortDirection = QueryOrderDirections.Descending;
                    }
                    else
                    {
                        _ListSortDirection = QueryOrderDirections.Ascending;
                    }
                    result.Add(new QueryOrderBySetting { PropertyName = _Splitted2[0], DisplayName = _Splitted2[0], Direction = _ListSortDirection });
                }
            }
        }
        return result;
    }

    public static string GetOrderByExpression(IEnumerable<Framework.MauiX.DataModels.ObservableQueryOrderBySetting> orderBys)
    {
        var orderByExpressions =
            from t in orderBys
            let propertyName = t.PropertyName ?? t.DisplayName
            select t.Direction == QueryOrderDirections.Ascending ? propertyName : propertyName + " DESC";
        return string.Join(",", orderByExpressions);
    }

    public override string ToString()
    {
        return GetOrderByExpression(new List<Framework.MauiX.DataModels.ObservableQueryOrderBySetting> { this });
    }
}
