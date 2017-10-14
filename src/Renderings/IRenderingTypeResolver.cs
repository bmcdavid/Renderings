using System;

namespace Renderings
{
    /// <summary>
    /// Resolves a Func&lt;TSource,object> type for creating renderings
    /// </summary>
    public interface IRenderingTypeResolver
    {
        /// <summary>
        /// Converts document alias into a Func creator that requires IPublishedContent and returns an object
        /// </summary>
        /// <param name="alias">document type string alias</param>
        /// <param name="allowBackEndTypes">if false, and resolved creator is backendonly then null is returned</param>
        /// <returns>function to create view models or null</returns>
        Type ResolveCreator<TSource>(string alias, bool allowBackEndTypes = false);
    }
}