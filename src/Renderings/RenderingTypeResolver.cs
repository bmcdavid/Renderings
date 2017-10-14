using DotNetStarter.Abstractions;
using System;
using System.Collections.Generic;

namespace Renderings
{
    [Registration(typeof(IRenderingTypeResolver), Lifecycle.Singleton)]
    public class RenderingTypeResolver : IRenderingTypeResolver
    {
        private readonly IRenderingAliasResolver _RenderingAliasResolver;
        private Dictionary<string, ResolveResult> _CreatorDictionary;

        public RenderingTypeResolver(IRenderingAliasResolver renderingAliasResolver)
        {
            _RenderingAliasResolver = renderingAliasResolver;
            _CreatorDictionary = new Dictionary<string, ResolveResult>();
        }

        public virtual Type ResolveCreator<TSource>(string alias, bool allowBackEnd = false)
        {
            if (!_CreatorDictionary.TryGetValue(alias, out ResolveResult result))
            {
                result = _RenderingAliasResolver.Resolve(alias);

                if (result.HasErrors == false)
                {
                    result.CreatorType = typeof(Func<,>).MakeGenericType(typeof(TSource), result.ModelType);
                }

                _CreatorDictionary.Add(alias, result);
            }

            if (result.HasErrors || allowBackEnd && result.Descriptor.BackendDocument)
                return null;

            return result.CreatorType;
        }
    }

}