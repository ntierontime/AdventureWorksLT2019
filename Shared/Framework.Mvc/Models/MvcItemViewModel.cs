using Framework.Common;
using Framework.Models;

namespace Framework.Mvc.Models
{
    public class MvcItemViewModel<TModel>
        where TModel : class
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
        /// the Key comes from {SolutionName}.Models.Definitions.TopLevelDropDownLists
        /// </summary>
        public Dictionary<string, List<NameValuePair>>? TopLevelDropDownListsFromDatabase { get; set; }
        /// <summary>
        /// Item1 is the partial view url
        /// Item2 is the Modal
        /// </summary>
        public TModel? Model { get; set; } = null;

        public UIItemFeatures UIItemFeatures { get; set; } = null!;
        public int IndexInArray { get; set; }

        public string HtmlId(string propertyName)
        {
            var name = HtmlName(propertyName);
            return name.Replace("[", "_").Replace("]", "_").Replace(".", "_");
        }
        public string HtmlName(string propertyName)
        {
            if(!UIItemFeatures.IsArrayBinding)
            {
                return string.IsNullOrEmpty(UIItemFeatures.BindingPath)
                    ? propertyName
                    : string.Format("{0}.{1}", UIItemFeatures.BindingPath, propertyName);
            }

            return string.Format("{0}[{1}].{2}", UIItemFeatures.BindingPath, IndexInArray, propertyName);
        }
    }
}

