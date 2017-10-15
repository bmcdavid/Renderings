using DotNetStarter.Abstractions;
using System;
using System.Linq;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace Renderings.UmbracoCms
{
    [Registration(typeof(ISiteSettingsResolver), Lifecycle.Singleton)]
    public class SiteSettingsResolver : ISiteSettingsResolver
    {
        private readonly IHomepageResolver _HomepageResolver;
        private readonly IRenderingAliasResolver _PublishedContentDescriptorResolver;

        public SiteSettingsResolver(IHomepageResolver homepageResolver, IRenderingAliasResolver aliasResolver)
        {
            _HomepageResolver = homepageResolver;
            _PublishedContentDescriptorResolver = aliasResolver;
        }

        public virtual IPublishedContent ResolveSettings(Type settingsViewModelType, int? homepageNodeId, UmbracoHelper umbracoHelper = null)
        {
            if (settingsViewModelType == null)
                throw new ArgumentNullException(nameof(settingsViewModelType));

            if (homepageNodeId == null)
                throw new ArgumentNullException(nameof(homepageNodeId));

            umbracoHelper = ResolveHelper(umbracoHelper);
            string settingsDocTypeAlias = _PublishedContentDescriptorResolver.ResolveType(settingsViewModelType);

            IPublishedContent settingsContent = umbracoHelper
                .TypedContent(homepageNodeId.Value)
                .Children
                .FirstOrDefault(child => child.DocumentTypeAlias == settingsDocTypeAlias);

            return settingsContent;
        }

        public virtual IPublishedContent ResolveSettings<T>(int? homepageNodId, UmbracoHelper umbracoHelper = null)
        {
            return ResolveSettings(typeof(T), homepageNodId, umbracoHelper);
        }

        private UmbracoHelper ResolveHelper(UmbracoHelper umbracoHelper)
        {
            return umbracoHelper ?? new UmbracoHelper(UmbracoContext.Current);
        }
    }
}