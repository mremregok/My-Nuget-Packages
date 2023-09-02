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
    public static class DependencyInjectionExtensions
    {
        public static void RegisterDependencies(this IServiceCollection services)
        {
            var assembly = Assembly.GetCallingAssembly();
            RegisterWithAttribute(services, assembly);
            RegisterWithInterface(services, assembly);
        }

        private static void RegisterWithAttribute(IServiceCollection services, Assembly assembly)
        {
            var types = assembly
                .ExportedTypes
                .Where(x => x.GetCustomAttributes(typeof(DIClassAttribute), true).Length > 0)
                .ToList();

            foreach ( var type in types )
            {
                var attribute = type.GetCustomAttribute<DIClassAttribute>();

                var implementedInterface = attribute.ImplementedInterface;

                if ( implementedInterface != null )
                {
                    switch(attribute.Scope)
                    {
                        case DependencyInjectionScope.Singleton:
                            services.AddSingleton(implementedInterface, type);
                            break;
                        case DependencyInjectionScope.Scoped:
                            services.AddScoped(implementedInterface, type);
                            break;
                        case DependencyInjectionScope.Transient:
                            services.AddTransient(implementedInterface, type);
                            break;
                        default: break;
                    }
                }

            }
        }

        private static void RegisterWithInterface(IServiceCollection services, Assembly assembly)
        {
            Type serviceLifetime = typeof(IServiceLifetime);

            var types = assembly
                .ExportedTypes
                .Where(x => serviceLifetime.IsAssignableFrom(x))
                .Where(x => x.GetInterfaces().Any(y => y.GenericTypeArguments.Any()))
                .ToList();
            
            foreach (var type in types)
            {
                var lifeTimeType = type.GetInterfaces()
                    .Where(x => x.GenericTypeArguments.Any())
                    .FirstOrDefault();

                var assignedInterface = lifeTimeType
                    .GetGenericArguments()
                    .Where (x => x.IsInterface)
                    .FirstOrDefault();

                var temp = lifeTimeType.GetType();


                if ( assignedInterface != null )
                {
                    switch (lifeTimeType.Name)
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
