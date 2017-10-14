using DotNetStarter.Abstractions;
using System;

namespace Renderings
{
    [Registration(typeof(IRenderingCreatorScoped), Lifecycle.Scoped)]
    public class RenderingCreatorScoped : IRenderingCreatorScoped
    {
        private readonly IRenderingTypeResolver _RenderingTypeResolver;
        private readonly ILocator _ScopedLocator; // Important: this func should come from a scoped locator.

        public RenderingCreatorScoped(IRenderingTypeResolver renderingTypeResolver, ILocator scopedLocator)
        {
            _RenderingTypeResolver = renderingTypeResolver;
            _ScopedLocator = scopedLocator;
        }

        public Func<T, object> GetCreator<T>(string alias)
        {
            var creatorType = _RenderingTypeResolver.ResolveCreator<object>(alias);

            return _ScopedLocator.Get(creatorType) as Func<T, object>;
        }
    }    
}