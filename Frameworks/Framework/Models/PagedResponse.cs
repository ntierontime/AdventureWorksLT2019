using System;
namespace Framework.Models
{
    public class PagedResponse<TStatus, TRequestBody> : Response<TStatus, TRequestBody>
    {
        public PaginationResponse? Pagination { get; set; }
    }
    public class PagedResponse<TResponseBody> : PagedResponse<System.Net.HttpStatusCode, TResponseBody>
    {
    }
}

