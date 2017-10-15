using DotNetStarter.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Renderings.Tests;

[assembly: DiscoverableAssembly]
[assembly: LocatorRegistryFactory(typeof(CustomLocatorFactory), typeof(DotNetStarter.DryIocLocatorFactory))]

namespace Renderings.Tests
{
    [TestClass]
    public class _Setup
    {
        [AssemblyInitialize]
        public static void Initialize(TestContext testContext)
        {
            var assemblies = DotNetStarter.ApplicationContext.GetScannableAssemblies();

            DotNetStarter.ApplicationContext.Startup(assemblies: assemblies);
        }
    }

    public class CustomLocatorFactory : ILocatorRegistryFactory
    {
        public ILocatorRegistry CreateRegistry()
        {
            return new DotNetStarter.DryIocLocator();
            //return new DotNetStarter.StructureMapLocator();
        }
    }
}
