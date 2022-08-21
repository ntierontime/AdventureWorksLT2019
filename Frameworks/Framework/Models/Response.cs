using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Models
{
    public class Response<TStatus, TResponseBody>
    {
        public TStatus Status { get; set; } = default(TStatus)!;
        public TResponseBody? ResponseBody { get; set; }
        public string? StatusMessage { get; set; }
    }
    public class Response<TResponseBody> : Response<System.Net.HttpStatusCode, TResponseBody>
    {
    }

    public class Response
    {
        public System.Net.HttpStatusCode Status { get; set; } = System.Net.HttpStatusCode.NoContent;
        public string? StatusMessage { get; set; }
    }
}

