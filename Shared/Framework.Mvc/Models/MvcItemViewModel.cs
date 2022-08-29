using Framework.Models;

namespace Framework.Mvc.Models
{
    public class MvcItemViewModel<TModel>: ItemViewModel<TModel>
        where TModel : class
    {
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

