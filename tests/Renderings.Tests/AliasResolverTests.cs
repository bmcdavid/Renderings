using Microsoft.VisualStudio.TestTools.UnitTesting;
using DotNetStarter.Abstractions;
using System;
using Renderings.Tests.Mocks;

namespace Renderings.Tests
{
    [TestClass]
    public class AliasResolverTests
    {
        Import<IRenderingAliasResolver> AliasResolver;

        Import<IRenderingTypeResolver> TypeResolver;

        Import<ILocator> Locator;

        [TestMethod]
        public void ShouldResolveAliasByType()
        {
            Assert.IsNotNull(AliasResolver.Service);
            var sut = AliasResolver.Service.ResolveType(typeof(Mocks.MockRendering));

            Assert.IsTrue(sut == "test");
        }

        [TestMethod]
        public void ShouldResolveTypeByAlias()
        {
            Assert.IsNotNull(AliasResolver.Service);
            var sut = AliasResolver.Service.ResolveAlias("test");

            Assert.IsTrue(sut == typeof(Mocks.MockRendering));
        }

        [TestMethod]
        public void ShouldResolvePropertyAlias()
        {
            var sut = AliasResolver.Service.ResolvePropertyAlias<MockRendering>(x => x.Name);

            Assert.IsTrue(sut == "testName");
        }

        [ExpectedException(typeof(Exception))]
        [TestMethod]
        public void ShouldThrowErrorResolvingPropertyAlias()
        {
            var sut = AliasResolver.Service.ResolvePropertyAlias<MockRendering>(x => x.UnmappedProperty);
        }

        [TestMethod]
        public void ShouldResolveRenderingType()
        {
            var sut = TypeResolver.Service.ResolveCreator<MockSource>("test");

            Assert.IsNotNull(sut);
            Assert.IsTrue(sut == typeof(Func<MockSource, MockRendering>));
        }

        [TestMethod]
        public void ShouldResolveFuncCreator()
        {
            var creator = TypeResolver.Service.ResolveCreator<MockSource>("test");
            var sut = DotNetStarter.ApplicationContext.Default.Locator.Get(creator) as Func<MockSource, MockRendering>;
            Assert.IsNotNull(sut);
        }

        [TestMethod]
        public void ShouldResolveCreatorFromScopedLocatorAndCreateInstance()
        {
            using (var scoped = Locator.Service.OpenScope())
            {
                var creator = scoped.Get<IRenderingCreatorScoped>().GetCreator<MockSource>("test");
                var sut = DotNetStarter.ApplicationContext.Default.Locator.Get(creator.GetType()) as Func<MockSource, MockRendering>;
                var t = new MockSource() { Id = 100, Name = "Test Soruce" };

                Assert.IsNotNull(sut);

                var instance = sut(t);

                Assert.IsTrue(instance.Mock.Id == 100);
            }
        }
    }
}
