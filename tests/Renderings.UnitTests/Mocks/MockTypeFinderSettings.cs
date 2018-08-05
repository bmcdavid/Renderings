using DotNetStarter.Abstractions;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Renderings.UnitTests.Mocks
{
    [Registration(typeof(IRenderingTypeFinderSettings), Lifecycle.Singleton)]
    public class MockTypeFinderSettings : IRenderingTypeFinderSettings
    {
        public IEnumerable<Type> TypesToFind => new Type[] { typeof(ISidebar) };

        public IEnumerable<Assembly> AssembliesToScan => new Assembly[] { typeof(IService).Assembly };

        public IAssemblyScanner AssemblyScannerFactory()
        {
            return new DotNetStarter.AssemblyScanner();
        }
    }
}