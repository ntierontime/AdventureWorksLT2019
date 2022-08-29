using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Models
{
    /// <summary>
    /// for deletion
    /// </summary>
    /// <typeparam name="TIdentifier"></typeparam>
    public class BatchActionRequest<TIdentifier>
    {
        public List<TIdentifier> Ids { get; set; } = null!;
        // public HttpMethod? ActionType { get; set; }// = HttpMethod.Put; // Update
    }

    /// <summary>
    /// for BulkUpdate
    /// </summary>
    /// <typeparam name="TIdentifier"></typeparam>
    /// <typeparam name="TActionData"></typeparam>
    public class BatchActionRequest<TIdentifier, TActionData>: BatchActionRequest<TIdentifier>
    {
        // ActionName, is assigned in controller method, used in Repository to control which properties should be updated.
        public string ActionName { get; set; } = String.Empty;
        // Use Table class or DefaultView class,
        // there is only one BulkUpdate method in Service/Repository/ServiceContract/RepositoryContract
        // but multiple BulkUpdate{ActionName} method in controller, because of Mvc Core [Bind] attribute
        public TActionData? ActionData { get; set; }
    }
}

