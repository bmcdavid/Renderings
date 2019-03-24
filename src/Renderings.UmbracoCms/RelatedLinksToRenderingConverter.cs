using DotNetStarter.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;
using Umbraco.Web.Models;

namespace Renderings.UmbracoCms
{
    /// <summary>
    /// Default IRelatedLinksToRenderingConverterScoped implementation
    /// </summary>
    [Registration(typeof(IRelatedLinksToRenderingConverterScoped), Lifecycle.Scoped)]
    public class RelatedLinksToRenderingConverter : IRelatedLinksToRenderingConverterScoped
    {
        private readonly IRenderingAliasResolver _DescriptorResolver;
        private readonly IRenderingCreatorScoped _ViewModelCreatorScoped;
        private readonly UmbracoHelper _umbracoHelper;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="publishedContentAliasResolver"></param>
        /// <param name="viewModelCreatorScoped"></param>
        /// <param name="umbracoHelper"></param>
        public RelatedLinksToRenderingConverter(IRenderingAliasResolver publishedContentAliasResolver, IRenderingCreatorScoped viewModelCreatorScoped, UmbracoHelper umbracoHelper)
        {
            _DescriptorResolver = publishedContentAliasResolver;
            _ViewModelCreatorScoped = viewModelCreatorScoped;
            _umbracoHelper = umbracoHelper;
        }

        /// <summary>
        /// Converts related links to IRendering instances
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="relatedLinks"></param>
        /// <param name="allowedTypes"></param>
        /// <returns></returns>
        public virtual IList<T> ConvertLinks<T>(IEnumerable<Link> relatedLinks, ICollection<Type> allowedTypes)
        {
            relatedLinks = (relatedLinks ?? Enumerable.Empty<Link>())
                .Where(x => x.Type == LinkType.Content).ToList();
            var list = new List<T>();
            var aliases = _DescriptorResolver.ResolveTypes(allowedTypes);

            foreach (var item in relatedLinks)
            {
                var publishedContent = _umbracoHelper.Content(item.Udi);
                if (aliases.Any(a => a == publishedContent.ContentType.Alias))
                {
                    T content = (T)_ViewModelCreatorScoped.GetCreator<IPublishedContent>(publishedContent.ContentType.Alias).Invoke(publishedContent);
                    var relatedContentLink = content as ISetRelatedLink;
                    relatedContentLink?.SetLink(item); // give the link data to the model

                    list.Add(content);
                }
            }

            return list;
        }
    }
}