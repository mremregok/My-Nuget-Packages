using RegisterService.Common;

namespace RegisterService
{
    /// <summary>
    /// Bu attribute'i kayıt etmek istediğiniz servise eklemelisiniz.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class DependencyInjectionAttribute : Attribute
    {
        /// <summary>
        /// Dependency injection için kullanılacak arayüz.
        /// </summary>
        public Type? ImplementedInterface { get; }
        /// <summary>
        /// Kayıt edileceği yaşam döngüsü.
        /// </summary>
        public Lifetime Scope { get; }
            
        public DependencyInjectionAttribute(Type implementedInterface, Lifetime scope)
        {
            ImplementedInterface = implementedInterface;
            Scope = scope;
        }

        public DependencyInjectionAttribute(Lifetime scope)
        {
            Scope = scope;
        }
    }
}