using DotNetStarter.Abstractions;
using System;

namespace Renderings
{
    /// <summary>
    /// Default implemenation of IRenderingCreatorScoped
    /// </summary>
    [Registration(typeof(IRenderingCreatorScoped), Lifecycle.Scoped)]
    public class RenderingCreatorScoped : IRenderingCreatorScoped
    {
        private readonly IRenderingTypeResolver _RenderingTypeResolver;
        private readonly ILocator _ScopedLocator; // Important: this func should come from a scoped locator.

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="renderingTypeResolver"></param>
        /// <param name="scopedLocator"></param>
        [Obsolete]
        public RenderingCreatorScoped(IRenderingTypeResolver renderingTypeResolver, ILocator scopedLocator)
        {
            _RenderingTypeResolver = renderingTypeResolver;
            _ScopedLocator = scopedLocator;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="renderingTypeResolver"></param>
        /// <param name="locator"></param>
        /// <param name="scopedLocator"></param>
        public RenderingCreatorScoped(IRenderingTypeResolver renderingTypeResolver, ILocator locator, ILocatorScopedAccessor scopedLocator)
        {
            _RenderingTypeResolver = renderingTypeResolver;
            _ScopedLocator = scopedLocator.CurrentScope ?? throw new ArgumentNullException(nameof(scopedLocator));
        }

        /// <summary>
        /// Gets a creator Func for given Type, not if not in a scoped context, there will be issues
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="alias"></param>
        /// <returns></returns>
        public Func<T, object> GetCreator<T>(string alias)
        {
            var creatorType = _RenderingTypeResolver.ResolveCreator<T>(alias);

            if (creatorType == null)
                return null;

            return _ScopedLocator.Get(creatorType) as Func<T, object>;
        }
    }    
}