namespace Framework.Models
{
    public class PagedViewModel<TResponseBody>
    {
        public UIListSettingModel UIListSetting { get; set; } = null!;

        public PagedResponse<TResponseBody> Result { get; set; } = null!;
        /// <summary>
        /// the Key comes from {SolutionName}.Models.Definitions.TopLevelDropDownLists
        /// </summary>
        public Dictionary<string, List<NameValuePair>>? TopLevelDropDownListsFromDatabase { get; set; }
    }
}

