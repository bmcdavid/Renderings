using DotNetStarter.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Renderings.Tests.Mocks;
using System;

namespace Renderings.Tests
{
    [TestClass]
    public class CreatorAndTypeResolverTests
    {
        private Import<ILocator> Locator;
        private Import<IRenderingTypeResolver> TypeResolver;

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