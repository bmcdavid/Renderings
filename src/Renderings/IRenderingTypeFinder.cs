using System;
using System.Collections.Generic;

namespace Renderings
{
    /// <summary>
    /// Finds types that implement IRendering
    /// </summary>
    public interface IRenderingTypeFinder
    {
        /// <summary>
        /// Get types for given IRendering implementation
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEnumerable<Type> GetTypesFor<T>() where T : IRendering;
    }
}
