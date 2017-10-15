using System;

namespace Renderings
{
    /// <summary>
    /// Resolves a Func&lt;TSource,object> type for creating renderings
    /// </summary>
    public interface IRenderingTypeResolver
    {
        /// <summary>
        /// Converts rendering alias into a Func creator that requires TSource constructor parameter and returns an object
        /// </summary>
        /// <param name="alias">document type string alias</param>
        /// <returns>function to create view models or null</returns>
        Type ResolveCreator<TSource>(string alias);
    }
}