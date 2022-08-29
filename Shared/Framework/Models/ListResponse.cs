using System;
namespace Framework.Models
{
    public class ListResponse<TStatus, TRequestBody> : Response<TStatus, TRequestBody>
    {
        public PaginationResponse? Pagination { get; set; }
    }
    public class ListResponse<TResponseBody> : ListResponse<System.Net.HttpStatusCode, TResponseBody>
    {
    }
}

