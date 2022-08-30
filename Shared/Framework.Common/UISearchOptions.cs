namespace Framework.Common
{
    public enum UISearchOptions
    {
        /// <summary>
        /// by default: search into text fields datatype: varchar/char/nvarchar/nchar/text/ntext/xml
        /// developer can develop TextSearch syntax to search into other datatypes: e.g. datetime/boolean/: parse Text Search Bar content, e.g. parse datetime... Enabled
        /// </summary>
        TextSearch,
        // DropDowns for boolean, and ForignKeys, DateTimeRange(e.g. today/this year...)
        RegularSearch,
        // Customizable DateTimeRange
        AdvancedSearch,
        /// <summary>
        /// Based on search result, available search criteria, e.g. Countries/Provinces available in current search result
        /// </summary>
        DynamicSearch,
        /// <summary>
        /// e.g. a ForigneKey table list in Dashboard of PrimaryKey table, the PrimaryKey will be always a condition.
        /// </summary>
        SpecialSearchParameters,
    }
}

