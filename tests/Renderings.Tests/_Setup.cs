using Microsoft.VisualStudio.TestTools.UnitTesting;

[assembly: DotNetStarter.Abstractions.DiscoverableAssembly]

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
}
