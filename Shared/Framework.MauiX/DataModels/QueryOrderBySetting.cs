

namespace Framework.MauiX.DataModels
{
    /// <summary>
    /// Query OrderBy Setting
    /// </summary>
    public class QueryOrderBySetting: Framework.MauiX.PropertyChangedNotifier
    {
        /// <summary>
        /// Gets or sets the name of the property.
        /// </summary>
        /// <value>
        /// The name of the property.
        /// </value>

        public string PropertyName { get; set; }

        private Framework.Models.QueryOrderDirections m_Direction = Framework.Models.QueryOrderDirections.Ascending;
        public Framework.Models.QueryOrderDirections Direction
        {
            get { return m_Direction; }
            set
            {
                Set(nameof(Direction), ref m_Direction, value);
            }
        }

        /// <summary>
        /// this is used in Xamarin.Forms
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// this is used in Xamarin.Forms
        /// </summary>
        public string FontIcon { get; set; }
        /// <summary>
        /// this is used in Xamarin.Forms
        /// </summary>
        public string FontIconFamily { get; set; }

        private bool m_IsSelected = false;
        public bool IsSelected
        {
            get { return m_IsSelected; }
            set
            {
                Set(nameof(IsSelected), ref m_IsSelected, value);
            }
        }

        /// <summary>
        /// This is used in Xamarin.Forms only for now.
        /// </summary>
        [JsonIgnore]
        public object ClientSideActions { get; set; }

        public void ToggleDirection()
        {
            Direction = Direction == Framework.Models.QueryOrderDirections.Ascending ?
                Framework.Models.QueryOrderDirections.Descending : Framework.Models.QueryOrderDirections.Ascending;
        }

        //public Expression OrderByExpression { get; set; }
    }

    /// <summary>
    /// a list/collection can have several order by clause
    /// </summary>
    public class QueryOrderBySettingCollection : List<QueryOrderBySetting>
    {
        #region constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryOrderBySettingCollection"/> class.
        /// </summary>
        public QueryOrderBySettingCollection()
        {
        }

        public QueryOrderBySettingCollection(IEnumerable<QueryOrderBySetting> list)
            : base(list)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryOrderBySettingCollection"/> class.
        /// </summary>
        /// <param name="queryOrderByExpression">The query order by expression can be parsed to <see cref="QueryOrderBySettingCollection"/> .</param>
        public QueryOrderBySettingCollection(string queryOrderByExpression)
        {
            if (string.IsNullOrWhiteSpace(queryOrderByExpression) == false)
            {
                string[] _Splitted1 = queryOrderByExpression.Split("|".ToCharArray());
                if (_Splitted1 != null && _Splitted1.Length > 0)
                {
                    foreach (string _Splitted1Item in _Splitted1)
                    {
                        if (string.IsNullOrWhiteSpace(_Splitted1Item) == false)
                        {
                            string[] _Splitted2 = _Splitted1Item.Trim().Split("~".ToCharArray());
                            if (_Splitted2.Length == 1)
                            {
                                this.Add(_Splitted2[0], QueryOrderDirections.Ascending);
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
                                this.Add(_Splitted2[0], _ListSortDirection);
                            }
                        }
                    }
                }
            }
        }

        #endregion constructors

        /// <summary>
        /// Adds one item of QueryOrderBySetting to this collection
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="direction">The direction.</param>
        public void Add(
            string propertyName
            , QueryOrderDirections direction)
        {
            this.Add(new QueryOrderBySetting{ DisplayName = propertyName, Direction = direction });
        }

        /// <summary>
        /// Gets the OrderBy expression.
        /// </summary>
        /// <returns>order by expression in string</returns>
        public string GetOrderByExpression()
        {
            StringBuilder _SB = new StringBuilder();

            int _Count = 0;
            foreach (QueryOrderBySetting _QueryOrderBySetting in this)
            {
                if (_Count == 0)
                {
                    _Count++;
                }
                else
                {
                    _SB.Append(",");
                }
                _SB.Append(string.Format("{0}{1}", _QueryOrderBySetting.PropertyName ?? _QueryOrderBySetting.DisplayName, _QueryOrderBySetting.Direction == QueryOrderDirections.Ascending ? "" : " DESC"));
            }

            return _SB.ToString();
        }

        public void DeSelectAll()
        {
            foreach (var queryOrderBySetting in this.Where(t => t.IsSelected))
            {
                queryOrderBySetting.IsSelected = false;
            }
        }
    }
}

