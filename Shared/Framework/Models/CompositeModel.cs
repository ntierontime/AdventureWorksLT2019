using System.Text.Json.Serialization;

namespace Framework.Models
{
    public class CompositeModel<TMaster, TPropertyEnum>
        where TMaster : class
        where TPropertyEnum : System.Enum
    {
        // TODO: temporary here to pass compile. to-be-removed
        [JsonIgnore]
        public UIListSettingModel UIListSetting { get; set; } = null!;

        public TMaster __Master__ { get; set; } = null!;
        public Dictionary<TPropertyEnum, Response<PaginationResponse>> Responses { get; set; } = new Dictionary<TPropertyEnum, Response<PaginationResponse>>();
        // this is for Mvc for now, wil be populated in Mvc Controller
        [JsonIgnore]
        public Dictionary<TPropertyEnum, UIParams> UIParamsList { get; set; } = new Dictionary<TPropertyEnum, UIParams>();

        public CompositeItemModel Get(TPropertyEnum key)
        {
            return new CompositeItemModel
            {
                Key = key.ToString(),
                Response = Responses[key],
                UIParams = UIParamsList[key],
            };
        }
    }
}

