using RegisterService;
using RegisterService.Common;
using Tryout.Services.Interfaces;

namespace Tryout.Services.Implementations
{
    [DIClass(typeof(ITestService), DependencyInjectionScope.Scoped)]
    public class TestService : ITestService
    {
    }
}
