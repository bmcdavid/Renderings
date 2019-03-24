using System;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;

namespace Renderings.UmbracoCms
{
    /// <summary>
    /// Resolves site settings from a given homepage node id
    /// </summary>
    public interface ISiteSettingsResolver
    {
        /// <summary>
        /// Resolves site settings content for given type and homepage node id
        /// </summary>
        /// <param name="settingsViewModelType"></param>
        /// <param name="homepageNodeId"></param>
        /// <param name="umbracoHelper"></param>
        /// <returns></returns>
        IPublishedContent ResolveSettings(Type settingsViewModelType, int? homepageNodeId, UmbracoHelper umbracoHelper = null);

        /// <summary>
        /// Resolves site settings content for generic T and homepage node Id
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="homepageNodId"></param>
        /// <param name="umbracoHelper"></param>
        /// <returns></returns>
        IPublishedContent ResolveSettings<T>(int? homepageNodId, UmbracoHelper umbracoHelper = null);
    }
}