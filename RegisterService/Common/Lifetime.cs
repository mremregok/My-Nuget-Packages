using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegisterService.Common
{
    /// <summary>
    /// Inject edilecek servisin, lifetime tipinin belirtilmesinde kullanılır.
    /// </summary>
    public enum Lifetime
    {
        Singleton,
        Transient,
        Scoped
    }
}
