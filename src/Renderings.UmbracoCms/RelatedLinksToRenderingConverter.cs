using DotNetStarter.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
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

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="publishedContentAliasResolver"></param>
        /// <param name="viewModelCreatorScoped"></param>
        public RelatedLinksToRenderingConverter(IRenderingAliasResolver publishedContentAliasResolver, IRenderingCreatorScoped viewModelCreatorScoped)        {
            _DescriptorResolver = publishedContentAliasResolver;
            _ViewModelCreatorScoped = viewModelCreatorScoped;
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

            //todo: brad figure out how to handle
            //_umbracoContextFactory.EnsureUmbracoContext().UmbracoContext.g

            //foreach (var item in relatedLinks)
            //{
            //    if (aliases.Any(a => a == item.Udi.ToPublishedContent();.?.Conte))
            //    {
            //        T content = (T)_ViewModelCreatorScoped.GetCreator<IPublishedContent>(item.Content.DocumentTypeAlias).Invoke(item.Content);
            //        var relatedContentLink = content as ISetRelatedLink;
            //        relatedContentLink?.SetLink(item); // give the link data to the model

            //        list.Add(content);
            //    }
            //}

            return list;
        }
    }
}