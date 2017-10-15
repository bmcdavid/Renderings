using System;

namespace Renderings
{
    /// <summary>
    /// Creates functions to create renderings with a source object, scoped suffix is convention implementations should be registered as a scoped lifetime
    /// </summary>
    public interface IRenderingCreatorScoped
    {
        /// <summary>
        /// Gets a func creator given an alias
        /// </summary>
        /// <param name="alias"></param>
        /// <returns></returns>
        Func<T, object> GetCreator<T>(string alias);
    }
}