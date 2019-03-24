using DotNetStarter.Abstractions;
using System;
using System.Linq;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;

namespace Renderings.UmbracoCms
{
    /// <summary>
    /// Default ISiteSettingsResolver implementation
    /// </summary>
    [Registration(typeof(ISiteSettingsResolver), Lifecycle.Singleton)]
    public class SiteSettingsResolver : ISiteSettingsResolver
    {
        private readonly IHomepageResolver _HomepageResolver;
        private readonly IRenderingAliasResolver _PublishedContentDescriptorResolver;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="homepageResolver"></param>
        /// <param name="aliasResolver"></param>
        public SiteSettingsResolver(IHomepageResolver homepageResolver, IRenderingAliasResolver aliasResolver)
        {
            _HomepageResolver = homepageResolver;
            _PublishedContentDescriptorResolver = aliasResolver;
        }

        /// <summary>
        /// Resolve site settings for given type and homepage node id
        /// </summary>
        /// <param name="settingsViewModelType"></param>
        /// <param name="homepageNodeId"></param>
        /// <param name="umbracoHelper"></param>
        /// <returns></returns>
        public virtual IPublishedContent ResolveSettings(Type settingsViewModelType, int? homepageNodeId, UmbracoHelper umbracoHelper = null)
        {
            if (settingsViewModelType == null)
                throw new ArgumentNullException(nameof(settingsViewModelType));

            if (homepageNodeId == null)
                throw new ArgumentNullException(nameof(homepageNodeId));

            umbracoHelper = ResolveHelper(umbracoHelper);
            string settingsDocTypeAlias = _PublishedContentDescriptorResolver.ResolveType(settingsViewModelType);

            IPublishedContent settingsContent = umbracoHelper
                .Content(homepageNodeId.Value)
                .Children
                .FirstOrDefault(child => child.ContentType.Alias == settingsDocTypeAlias);

            return settingsContent;
        }

        /// <summary>
        /// Resolve site settings from generic type and given home page node id
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="homepageNodId"></param>
        /// <param name="umbracoHelper"></param>
        /// <returns></returns>
        public virtual IPublishedContent ResolveSettings<T>(int? homepageNodId, UmbracoHelper umbracoHelper = null)
        {
            return ResolveSettings(typeof(T), homepageNodId, umbracoHelper);
        }

        private UmbracoHelper ResolveHelper(UmbracoHelper umbracoHelper)
        {
            return umbracoHelper ?? Umbraco.Web.Composing.Current.UmbracoHelper;
        }
    }
}