using DotNetStarter.Abstractions;
using System;
using System.Collections.Generic;

namespace Renderings
{
    /// <summary>
    /// Default for IRenderingTypeResolver
    /// </summary>
    [Registration(typeof(IRenderingTypeResolver), Lifecycle.Singleton)]
    public class RenderingTypeResolver : IRenderingTypeResolver
    {
        private readonly IRenderingAliasResolver _RenderingAliasResolver;
        private Dictionary<string, Type> _CreatorDictionary;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="renderingAliasResolver"></param>
        public RenderingTypeResolver(IRenderingAliasResolver renderingAliasResolver)
        {
            _RenderingAliasResolver = renderingAliasResolver;
            _CreatorDictionary = new Dictionary<string, Type>();
        }

        /// <summary>
        /// Resolves a Func creator for given alias
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="alias"></param>
        /// <returns></returns>
        public virtual Type ResolveCreator<TSource>(string alias)
        {
            if (!_CreatorDictionary.TryGetValue(alias, out Type creator))
            {
                var result = _RenderingAliasResolver.Resolve(alias);

                if (result.HasErrors == false)
                {
                    creator = typeof(Func<,>).MakeGenericType(typeof(TSource), result.ModelType);
                }

                _CreatorDictionary[alias] = creator;
            }
            
            return creator;
        }
    }

}