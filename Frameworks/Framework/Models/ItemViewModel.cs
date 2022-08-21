namespace Framework.Models
{
    public class ItemViewModel<TModel>
        where TModel : class
    {
        public System.Net.HttpStatusCode Status { get; set; }
        public string? StatusMessage { get; set; }

        /// <summary>
        /// It is a ToString() for known TemplateName
        /// <seealso cref="ViewItemTemplateNames"/>
        /// </summary>
        public string? Template { get; set; }

        // this is used for inline-editing
        public bool IsCurrentItem { get; set; } = false;

        public bool BulkSelected { get; set; } = false;
        /// <summary>
        /// the Key comes from {SolutionName}.Models.Definitions.TopLevelDropDownLists
        /// </summary>
        public Dictionary<string, List<NameValuePair>>? TopLevelDropDownListsFromDatabase { get; set; }
        /// <summary>
        /// Item1 is the partial view url
        /// Item2 is the Modal
        /// </summary>
        public TModel? Model { get; set; } = null;
    }
}

