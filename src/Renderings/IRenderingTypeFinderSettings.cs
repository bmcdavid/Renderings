using DotNetStarter.Abstractions;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Renderings
{
    public interface IRenderingTypeFinderSettings
    {
        /// <summary>
        /// Types to discover implementations for
        /// </summary>
        IEnumerable<Type> TypesToFind { get; }

        /// <summary>
        /// Assemblies to scan for view models, typically on the assembly with the view models
        /// </summary>
        IEnumerable<Assembly> AssembliesToScan { get; }

        /// <summary>
        /// Creates new IAssemblyScanner instance
        /// </summary>
        /// <returns></returns>
        IAssemblyScanner AssemblyScannerFactory();
    }
}
