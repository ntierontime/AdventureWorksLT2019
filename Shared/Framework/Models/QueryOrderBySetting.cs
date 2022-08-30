using Framework.Common;

namespace Framework.Models
{
    public class QueryOrderBySetting
    {
        public string PropertyName { get; set; } = null!;
        public string DisplayName { get; set; } = null!;

        public QueryOrderDirections Direction { get; set; } = QueryOrderDirections.Ascending;

        public static IEnumerable<QueryOrderBySetting> Parse(string? queryOrderByExpression)
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

        public static string GetOrderByExpression(IEnumerable<QueryOrderBySetting> orderBys)
        {
            var orderByExpressions =
                from t in orderBys
                let propertyName = t.PropertyName ?? t.DisplayName
                select t.Direction == QueryOrderDirections.Ascending ? propertyName : propertyName + " DESC";
            return string.Join(",", orderByExpressions);
        }
    }
}

