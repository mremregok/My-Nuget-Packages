using RegisterService;
using RegisterService.Common;
using RegisterService.Interfaces;
using Tryout.Services.Interfaces;

namespace Tryout.Services.Implementations
{
    [DependencyInjection(Lifetime.Scoped)]
    public class TestService : ITestService
    {
    }
}
