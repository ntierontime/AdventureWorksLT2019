using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Models
{
    public interface ICopyTo<T>
        where T : class
    {
        void CopyTo(T destination);
    }
}
