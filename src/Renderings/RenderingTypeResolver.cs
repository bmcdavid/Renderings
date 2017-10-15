using DotNetStarter.Abstractions;
using System;
using System.Collections.Generic;

namespace Renderings
{
    [Registration(typeof(IRenderingTypeResolver), Lifecycle.Singleton)]
    public class RenderingTypeResolver : IRenderingTypeResolver
    {
        private readonly IRenderingAliasResolver _RenderingAliasResolver;
        private Dictionary<string, Type> _CreatorDictionary;

        public RenderingTypeResolver(IRenderingAliasResolver renderingAliasResolver)
        {
            _RenderingAliasResolver = renderingAliasResolver;
            _CreatorDictionary = new Dictionary<string, Type>();
        }

        public virtual Type ResolveCreator<TSource>(string alias)
        {
            if (!_CreatorDictionary.TryGetValue(alias, out Type creator))
            {
                var result = _RenderingAliasResolver.Resolve(alias);

                if (result.HasErrors == false)
                {
                    creator = typeof(Func<,>).MakeGenericType(typeof(TSource), result.ModelType);
                }

                _CreatorDictionary.Add(alias, creator);
            }
            
            return creator;
        }
    }

}