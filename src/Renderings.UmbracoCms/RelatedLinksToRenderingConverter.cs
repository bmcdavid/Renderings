using DotNetStarter.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Models;
using Umbraco.Web.Models;

namespace Renderings.UmbracoCms
{
    [Registration(typeof(IRelatedLinksToRenderingConverterScoped), Lifecycle.Scoped)]
    public class RelatedLinksToRenderingConverter : IRelatedLinksToRenderingConverterScoped
    {
        private readonly IRenderingAliasResolver _DescriptorResolver;
        private readonly IRenderingCreatorScoped _ViewModelCreatorScoped;

        public RelatedLinksToRenderingConverter(IRenderingAliasResolver publishedContentAliasResolver, IRenderingCreatorScoped viewModelCreatorScoped)
        {
            _DescriptorResolver = publishedContentAliasResolver;
            _ViewModelCreatorScoped = viewModelCreatorScoped;
        }

        public IList<T> ConvertLinks<T>(IEnumerable<RelatedLink> relatedLinks, ICollection<Type> allowedTypes)
        {
            relatedLinks = relatedLinks ?? Enumerable.Empty<RelatedLink>();
            var list = new List<T>();
            var aliases = _DescriptorResolver.ResolveTypes(allowedTypes);

            foreach (var item in relatedLinks)
            {
                if (aliases.Any(a => a == item.Content?.DocumentTypeAlias))
                {
                    T content = (T)_ViewModelCreatorScoped.GetCreator<IPublishedContent>(item.Content.DocumentTypeAlias).Invoke(item.Content);
                    var relatedContentLink = content as ISetRelatedLink;
                    relatedContentLink?.SetLink(item); // give the link data to the model

                    list.Add(content);
                }
            }

            return list;
        }
    }
}