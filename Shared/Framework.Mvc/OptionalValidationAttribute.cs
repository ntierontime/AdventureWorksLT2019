using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Mvc
{
    [AttributeUsage(AttributeTargets.Method)]
    public class OptionalValidationAttribute : Attribute
    {
        public OptionalValidationAttribute(string shouldEnableDataValidationParameterName)
        {
            ShouldEnableDataValidationParameterName = shouldEnableDataValidationParameterName;
        }

        public string ShouldEnableDataValidationParameterName { get; }
    }
}
