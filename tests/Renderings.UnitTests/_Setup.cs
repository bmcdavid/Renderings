using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Renderings.UnitTests
{
    [TestClass]
    public class _Setup
    {
        [AssemblyInitialize]
        public static void Initialize(TestContext testContext)
        {
            var builder = DotNetStarter.Configure.StartupBuilder.Create();
            builder
                .ConfigureAssemblies(a =>
                {
                    a.WithDiscoverableAssemblies()
                    .WithAssemblyFromType<_Setup>();
                })
                .OverrideDefaults(d =>
                {
                    d.UseLocatorRegistryFactory(new DotNetStarter.Locators.DryIocLocatorFactory());
                })
                .Build()
                .Run();
        }
    }
}