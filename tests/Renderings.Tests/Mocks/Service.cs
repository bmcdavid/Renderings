using DotNetStarter.Abstractions;

namespace Renderings.Tests.Mocks
{
    [Registration(typeof(IService), Lifecycle.Singleton)]
    public class Service : IService
    {
        public string Test()
        {
            return "Testing";
        }
    }
}
