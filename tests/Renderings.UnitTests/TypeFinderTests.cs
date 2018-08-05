using DotNetStarter.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Renderings.UnitTests.Mocks;
using System.Linq;

namespace Renderings.UnitTests
{
    [TestClass]
    public class TypeFinderTests
    {
        private Import<IRenderingTypeFinder> TypeFinder;

        [TestMethod]
        public void ShouldFindISidebar()
        {
            Assert.IsNotNull(TypeFinder.Service);
            var sut = TypeFinder.Service.GetTypesFor<ISidebar>();

            Assert.IsTrue(sut.Where(x => !x.IsAbstract && !x.IsInterface).Count() == 1);
        }
    }
}