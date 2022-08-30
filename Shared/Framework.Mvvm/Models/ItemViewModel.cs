namespace Framework.Mvvm.Models
{
    public class ItemViewModel<TDataModel>
        where TDataModel : class
    {
        public System.Net.HttpStatusCode Status { get; set; }
        public string? StatusMessage { get; set; }

        /// <summary>
        /// It is a ToString() for known TemplateName
        /// <seealso cref="ViewItemTemplates"/>
        /// </summary>
        public string? Template { get; set; }

        // this is used for inline-editing
        public bool IsCurrentItem { get; set; } = false;

        public bool BulkSelected { get; set; } = false;

        /// <summary>
        /// Item1 is the partial view url
        /// Item2 is the Modal
        /// </summary>
        public TDataModel? Model { get; set; } = null;
    }
}

