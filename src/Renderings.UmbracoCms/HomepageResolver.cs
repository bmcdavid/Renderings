using DotNetStarter.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Models;
using Umbraco.Web;
using Umbraco.Web.Routing;

namespace Renderings.UmbracoCms
{
    /// <summary>
    /// Default IHomepageResolver implementation
    /// </summary>
    [Registration(typeof(IHomepageResolver), Lifecycle.Singleton)]
    public class HomepageResolver : IHomepageResolver
    {
        private readonly IUmbracoContextAccessor _umbracoContextAccessor;
        private readonly UrlProvider _urlProvider;
        private Dictionary<string, int?> _ResolvedNodeIds = new Dictionary<string, int?>();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="umbracoContextAccessor"></param>
        /// <param name="urlProvider"></param>
        public HomepageResolver(IUmbracoContextAccessor umbracoContextAccessor, UrlProvider urlProvider)
        {
            _umbracoContextAccessor = umbracoContextAccessor;
            _urlProvider = urlProvider;
        }

        /// <summary>
        /// Gets homepage by Id
        /// </summary>
        /// <param name="requestUrl"></param>
        /// <param name="umbracoHelper"></param>
        /// <returns></returns>
        public virtual int? ResolveHomepageNodeId(Uri requestUrl, UmbracoHelper umbracoHelper = null)
        {
            if (requestUrl == null)
                throw new ArgumentNullException(nameof(requestUrl));

            umbracoHelper = EnsureUmbracoHelper(umbracoHelper);

            string urlToCompare = requestUrl.GetLeftPart(UriPartial.Authority) + "/";

            if (!_ResolvedNodeIds.TryGetValue(urlToCompare, out int? id))
            {
                // check by current content request if set
                var currentNode = _umbracoContextAccessor.UmbracoContext.PublishedRequest?.PublishedContent ?? null;

                if (currentNode != null)
                {
                    var current = ResolveFromPath(currentNode.Path);

                    if (current != null)
                    {
                        id = current;
                        _ResolvedNodeIds.Add(urlToCompare, id);
                    }
                }

                // fallback to URL matching on root nodes
                if (id == null)
                {
                    var rootNodes = umbracoHelper.ContentAtRoot();

                    foreach (var node in rootNodes)
                    {
                        List<string> urls = new List<string>(_urlProvider.GetOtherUrls(node.Id).Select(x => x.Text))//todo: verify text assumption
                        {
                            _urlProvider.GetUrl(node.Id, absolute: true) // last url defined in UI
                        };

                        var match = urls.FirstOrDefault(x => string.CompareOrdinal(x, urlToCompare) == 0);

                        if (match != null)
                        {
                            id = node.Id;

                            _ResolvedNodeIds.Add(urlToCompare, id);
                            break;
                        }
                    }
                }
            }

            return id;
        }

        /// <summary>
        /// Tries to resolve homepage from given umbracoHelper or currents request url
        /// </summary>
        /// <param name="umbracoHelper"></param>
        /// <returns></returns>
        public int? ResolveHomepageNodeId(UmbracoHelper umbracoHelper = null)
        {
            var helper = EnsureUmbracoHelper(umbracoHelper);

            return ResolveHomepageNodeId(_umbracoContextAccessor.UmbracoContext.HttpContext.Request.Url, helper);
        }

        /// <summary>
        /// Tries to resolve homepage from IContentBase path
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public virtual int? ResolveHomepageNodeId(IContentBase content)
        {
            return ResolveFromPath(content.Path);
        }

        /// <summary>
        /// Resolves url from given homepage Id
        /// </summary>
        /// <param name="homepageId"></param>
        /// <returns></returns>
        public virtual string ResolveHomepageUrl(int? homepageId)
        {
            var resolved = _ResolvedNodeIds.FirstOrDefault(x => x.Value == homepageId).Key;

            return resolved;
        }

        private UmbracoHelper EnsureUmbracoHelper(UmbracoHelper umbracoHelper)
        {
            return umbracoHelper ?? Umbraco.Web.Composing.Current.UmbracoHelper;
        }

        private int? ResolveFromPath(string path)
        {
            if (int.TryParse(path.Split(',')[1], out int currentHomeId))
            {
                return currentHomeId;
            }

            return null;
        }
    }
}