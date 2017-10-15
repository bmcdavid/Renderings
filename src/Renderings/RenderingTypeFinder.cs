using DotNetStarter.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Renderings
{
    /// <summary>
    /// Default implementation for IRenderingTypeFinder
    /// </summary>
    [Registration(typeof(IRenderingTypeFinder), Lifecycle.Singleton)]
    public class RenderingTypeFinder : IRenderingTypeFinder
    {
        private readonly IRenderingTypeFinderSettings _RenderingTypeFinderSettings;

        private IAssemblyScanner _TypeFinder;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="renderingTypeFinderSettings"></param>
        public RenderingTypeFinder(IRenderingTypeFinderSettings renderingTypeFinderSettings)
        {
            _RenderingTypeFinderSettings = renderingTypeFinderSettings;
        }

        /// <summary>
        /// Gets types for given IRenderings
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IEnumerable<Type> GetTypesFor<T>() where T : IRendering
        {
            EnsureTypes();
            var types = _TypeFinder.GetTypesFor(typeof(T));

            if (types.Any() == false)
            {
                throw new Exception($"Unable to find types for: {typeof(T).FullName}, please review IRenderingTypeFinderSettings implementation: {_RenderingTypeFinderSettings.GetType().FullName}!");
            }

            return types;
        }

        private void EnsureTypes()
        {
            if (_TypeFinder == null)
            {
                _TypeFinder = _RenderingTypeFinderSettings.AssemblyScannerFactory();
                _TypeFinder.Scan(_RenderingTypeFinderSettings.AssembliesToScan, _RenderingTypeFinderSettings.TypesToFind);
            }
        }
    }
}
