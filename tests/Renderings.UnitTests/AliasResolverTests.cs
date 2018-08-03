using DotNetStarter.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Renderings.UnitTests.Mocks;
using System;

namespace Renderings.UnitTests
{
    [TestClass]
    public class AliasResolverTests
    {
        private Import<IRenderingAliasResolver> AliasResolver;

        [TestMethod]
        public void ShouldResolveAliasByType()
        {
            Assert.IsNotNull(AliasResolver.Service);
            var sut = AliasResolver.Service.ResolveType(typeof(MockRendering));

            Assert.IsTrue(sut == "test");
        }

        [TestMethod]
        public void ShouldResolveTypeByAlias()
        {
            Assert.IsNotNull(AliasResolver.Service);
            var sut = AliasResolver.Service.ResolveAlias("test");

            Assert.IsTrue(sut == typeof(MockRendering));
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
    }
}