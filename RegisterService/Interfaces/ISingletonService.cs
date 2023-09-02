using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegisterService.Interfaces
{
    public interface ISingletonService<T> : IServiceLifetime where T : class { }
}
