using RegisterService.Common;

namespace RegisterService
{
    /// <summary>
    /// Bu attribute'i kayıt etmek istediğiniz servise eklemelisiniz.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class DIClassAttribute : Attribute
    {
        /// <summary>
        /// Dependency injection için kullanılacak arayüz.
        /// </summary>
        public Type ImplementedInterface { get; }
        /// <summary>
        /// Kayıt edileceği yaşam döngüsü.
        /// </summary>
        public DependencyInjectionScope Scope { get; }

        public DIClassAttribute(Type implementedInterface, DependencyInjectionScope scope)
        {
            ImplementedInterface = implementedInterface;
            Scope = scope;
        }
    }
}