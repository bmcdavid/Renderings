using System;
using System.Collections.Generic;
using Umbraco.Web.Models;

namespace Renderings.UmbracoCms
{
    public interface IRelatedLinksToRenderingConverterScoped
    {
        IList<T> ConvertLinks<T>(IEnumerable<RelatedLink> relatedLinks, ICollection<Type> allowedViewModelTypes);
    }
}