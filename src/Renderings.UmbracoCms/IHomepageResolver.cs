using System;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace Renderings.UmbracoCms
{
    /// <summary>
    /// Resolves Umbraco Homepage node Ids
    /// </summary>
    public interface IHomepageResolver
    {
        /// <summary>
        /// Resolves homepage node ID from given content
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        int? ResolveHomepageNodeId(IContentBase content);

        /// <summary>
        /// Resolves homepage node id from umbracohelper
        /// </summary>
        /// <param name="umbracoHelper"></param>
        /// <returns></returns>
        int? ResolveHomepageNodeId(UmbracoHelper umbracoHelper = null);

        /// <summary>
        /// Resolves homepage node id from request Uri
        /// </summary>
        /// <param name="requestUrl"></param>
        /// <param name="umbracoHelper"></param>
        /// <returns></returns>
        int? ResolveHomepageNodeId(Uri requestUrl, UmbracoHelper umbracoHelper = null);

        /// <summary>
        /// Resolves homepage url from homepage id
        /// </summary>
        /// <param name="homepageId"></param>
        /// <returns></returns>
        string ResolveHomepageUrl(int? homepageId);
    }
}