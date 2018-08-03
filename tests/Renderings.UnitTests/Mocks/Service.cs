using DotNetStarter.Abstractions;

namespace Renderings.UnitTests.Mocks
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