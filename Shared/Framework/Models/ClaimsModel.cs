using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Models
{
    /// <summary>
    /// please see Microsoft Identity Framework
    /// </summary>
    public class ClaimsModel
    {
        public string? AspNetUserName { get; set; }
        public string AspNetUserID { get; set; } = null!;
        public string? Email { get; set; }
        public string[] Roles { get; set; } = null!;

        /// <summary>
        /// We are using long as PersonID datatype. please change it accordingly
        /// </summary>
        public long PersonID { get; set; }
    }
}

