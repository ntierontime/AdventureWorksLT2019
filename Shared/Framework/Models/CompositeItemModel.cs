namespace Framework.Models
{
    public class CompositeItemModel
    {
        public string Key { get; set; } = null!;

        public Response<PaginationResponse> Response { get; set; } = null!;

        public UIParams UIParams { get; set; } = null;
    }
}

