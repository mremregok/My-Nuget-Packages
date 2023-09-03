using Microsoft.Extensions.DependencyInjection;
using RegisterService;
using RegisterService.Common;
using RegisterService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Bu Extension ile projenizde servislerin kaydedilmesini otomatikleştirebilirsiniz.
    /// </summary>
    public static class DependencyInjectionExtensions
    {
        /// <summary>
        /// Bu metodu, servis kaydı alanında çağırarak, servislerinizin kaydını otomatik olarak yaptırabilirsiniz.
        /// </summary>
        /// <param name="services">Proje başlatılırken almış olduğunuz 'IServiceCollection' tipindeki servisiniz.</param>
        public static void RegisterServices(this IServiceCollection services)
        {
            var assembly = Assembly.GetCallingAssembly(); //Metodun çağrılmış olduğu Assembly bilgisini alıyoruz.
            RegisterWithAttribute(services, assembly);
            RegisterWithInterface(services, assembly);
        }
        /// <summary>
        /// DIClass Attribute'ine sahip sınıfları algılayıp onları belirtilen servis ile kaydını yapan metottur.
        /// </summary>
        /// <param name="services">IServiceCollection tipindeki, projenize ait services nesnesidir.</param>
        /// <param name="assembly">Çağıran projenin Assembly bilgileri.</param>
        private static void RegisterWithAttribute(IServiceCollection services, Assembly assembly)
        {
            var types = assembly
                .ExportedTypes //Projede export edilmiş tipleri alıyoruz.
                .Where(x => x.GetCustomAttributes(typeof(DependencyInjectionAttribute), true).Length > 0)
                //DIClassAttribute'e sahip sınıfları alıyoruz.
                .ToList();

            foreach ( var type in types )
            {
                var attribute = type.GetCustomAttribute<DependencyInjectionAttribute>();

                var implementedInterface = attribute.ImplementedInterface;//Bu attribute içerisinde göndermiş olduğumuz
                //Arayüz tipini alıyoruz.

                if ( implementedInterface == null )
                {
                    implementedInterface = type.GetInterface($"I{type.Name}");
                }

                switch (attribute.Scope)//Attribute içerisinde yer alan scope değerine göre Dependency Injection
                                        //Yapılacaktır.
                {
                    case Lifetime.Singleton:
                        services.AddSingleton(implementedInterface, type);
                        break;
                    case Lifetime.Scoped:
                        services.AddScoped(implementedInterface, type);
                        break;
                    case Lifetime.Transient:
                        services.AddTransient(implementedInterface, type);
                        break;
                    default: break;
                }

            }
        }
        /// <summary>
        /// Lifetime arayüzlere sahip sınıfları algılayıp onları belirtilen servis ile kaydını yapan metottur.
        /// </summary>
        /// <param name="services">IServiceCollection tipindeki, projenize ait services nesnesidir.</param>
        /// <param name="assembly">Çağıran projenin Assembly bilgileri.</param>
        private static void RegisterWithInterface(IServiceCollection services, Assembly assembly)
        {
            Type serviceLifetime = typeof(IServiceLifetime);//Tip belirtiyoruz.

            var types = assembly
                .ExportedTypes
                .Where(x => serviceLifetime.IsAssignableFrom(x))//Tip uyumluluğunu kontrol ediyoruz.
                .Where(x => x.GetInterfaces().Any(y => y.GenericTypeArguments.Any()))//Herhangi bir sınıfının Generic olarak
                //Argüman alıp almadığını kontrol ediyoruz.
                .ToList();
            
            foreach (var type in types)
            {
                var lifeTimeType = type.GetInterfaces()//Lifetime arayüzü alıyoruz.
                    .Where(x => x.GenericTypeArguments.Any() && serviceLifetime.IsAssignableFrom(x))
                    .FirstOrDefault();

                var assignedInterface = lifeTimeType//Lifetime arayüzde Generic Argüman olarak kaydettiğimiz
                    //Sınıfımızın kalıtım aldığı asıl arayüzü alıyoruz.
                    .GetGenericArguments()
                    .Where (x => x.IsInterface)//Arayüz mü diye kontrol ederek sağlaması yapılıyor.
                    .FirstOrDefault();


                if ( assignedInterface != null )
                {
                    switch (lifeTimeType.Name)//Lifetime arayüzün ismini kontrol ederek hangi lifetime arayüz
                        //Olduğunu tespit ediyoruz.
                    {
                        case "ITransientService`1":
                            services.AddTransient(assignedInterface, type);
                            break;
                        case "IScopedService`1":
                            services.AddScoped(assignedInterface, type);
                            break;
                        case "ISingletonService`1":
                            services.AddSingleton(assignedInterface, type);
                            break;
                        default: break;
                    }
                }
            }
        }
    }
}
