using System;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace Renderings.UmbracoCms
{
    public interface IHomepageResolver
    {
        int? ResolveHomepageNodeId(IContentBase content);

        int? ResolveHomepageNodeId(UmbracoHelper umbracoHelper = null);

        int? ResolveHomepageNodeId(Uri requestUrl, UmbracoHelper umbracoHelper = null);

        string ResolveHomepageUrl(int? homepageId);
    }
}