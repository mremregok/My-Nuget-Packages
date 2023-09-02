using RegisterService.Interfaces;
using Tryout.Services.Interfaces;

namespace Tryout.Services.Implementations
{
    public class InterfaceMarkerTest : IInterfaceMarkerTest, IScopedService<IInterfaceMarkerTest>
    {
    }
}
