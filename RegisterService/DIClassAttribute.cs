using RegisterService.Common;

namespace RegisterService
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DIClassAttribute : Attribute
    {
        public Type ImplementedInterface { get; }
        public DependencyInjectionScope Scope { get; }

        public DIClassAttribute(Type implementedInterface, DependencyInjectionScope scope)
        {
            ImplementedInterface = implementedInterface;
            Scope = scope;
        }
    }
}