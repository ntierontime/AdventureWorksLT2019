using Framework.Models;
namespace Framework.Mvc.Models
{
    public class ListViewModel<TResponseBody>
    {
        public UIListSettingModel UIListSetting { get; set; } = null!;

        public ListResponse<TResponseBody> Result { get; set; } = null!;
        /// <summary>
        /// the Key comes from {SolutionName}.Models.Definitions.TopLevelDropDownLists
        /// </summary>
        public Dictionary<string, List<NameValuePair>>? TopLevelDropDownListsFromDatabase { get; set; }
    }
}

