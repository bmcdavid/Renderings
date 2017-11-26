using DotNetStarter.Abstractions;
using DotNetStarter.Abstractions.Internal;
using Renderings;
using System;
using System.Collections.Generic;
using System.Linq;

[assembly: DiscoverTypes(typeof(RenderingDocumentAliasAttribute))]

namespace Renderings
{
    /// <summary>
    /// This startup module register all classes with RenderingDocumentAliasAttribute
    /// </summary>
    [StartupModule]
    public class RegisterRenderings : ILocatorConfigure
    {
        void ILocatorConfigure.Configure(ILocatorRegistry registry, IStartupEngine engine)
        {
            engine.OnLocatorStartupComplete += () =>
            {
                IEnumerable<Type> renderingTypes = engine.Configuration
                            .AssemblyScanner.GetTypesFor(typeof(RenderingDocumentAliasAttribute));

                foreach (var rendering in renderingTypes.Where(x => !x.IsAbstract() && !x.IsInterface()))
                {
                    registry?.Add(rendering, rendering, lifecycle: Lifecycle.Transient);
                }
            };
        }
    }
}
