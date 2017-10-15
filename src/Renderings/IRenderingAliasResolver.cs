using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Renderings
{
    /// <summary>
    /// Resolves string to Type and Type to string, in addition to getting property aliases from Type's
    /// </summary>
    public interface IRenderingAliasResolver
    {
        /// <summary>
        /// Resolve a string alias into a Type
        /// </summary>
        /// <param name="documentAlias"></param>
        /// <returns></returns>
        Type ResolveAlias(string documentAlias);

        /// <summary>
        /// Resolves multiple string aliases into Types
        /// </summary>
        /// <param name="documentAliases"></param>
        /// <returns></returns>
        IEnumerable<Type> ResolveAliases(ICollection<string> documentAliases);

        /// <summary>
        /// Resolves a Type into a string alias
        /// </summary>
        /// <param name="renderingType"></param>
        /// <returns></returns>
        string ResolveType(Type renderingType);

        /// <summary>
        /// Resolves multiple Types into string aliases
        /// </summary>
        /// <param name="renderingTypes"></param>
        /// <returns></returns>
        IEnumerable<string> ResolveTypes(ICollection<Type> renderingTypes);

        /// <summary>
        /// Resolves a string into a resolve result which may or may not have errors
        /// </summary>
        /// <param name="documentAlias"></param>
        /// <returns></returns>
        ResolveResult Resolve(string documentAlias);

        /// <summary>
        /// Resolves a property string alias from a Type expression
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        string ResolvePropertyAlias<T>(Expression<Func<T, object>> expression);
    }
}
