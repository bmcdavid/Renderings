using System;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace Renderings.UmbracoCms
{
    public interface ISiteSettingsResolver
    {
        IPublishedContent ResolveSettings(Type settingsViewModelType, int? homepageNodeId, UmbracoHelper umbracoHelper = null);

        IPublishedContent ResolveSettings<T>(int? homepageNodId, UmbracoHelper umbracoHelper = null);
    }
}