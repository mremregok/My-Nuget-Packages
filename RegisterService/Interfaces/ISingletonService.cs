using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegisterService.Interfaces
{
    /// <summary>
    /// Bu interface'i kalıtarak, 'T' olarak belirtmiş olduğunuz servisin, kalıtım alan servis ile
    /// 'singleton lifetime' olarak kaydı yapılacağını belirtmiş olursunuz.
    /// </summary>
    /// <typeparam name="T">Kaydı yapılacak servisin arayüzünü içerir.</typeparam>
    public interface ISingletonService<T> : IServiceLifetime where T : class { }
}
