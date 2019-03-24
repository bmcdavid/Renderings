using System;
using System.Collections.Generic;
using Umbraco.Web.Models;

namespace Renderings.UmbracoCms
{
    /// <summary>
    /// Converts relatedlink content to allowed rendering types
    /// </summary>
    public interface IRelatedLinksToRenderingConverterScoped
    {
        /// <summary>
        /// Converts relatedlink content to allowed rendering types
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="relatedLinks"></param>
        /// <param name="allowedViewModelTypes"></param>
        /// <returns></returns>
        IList<T> ConvertLinks<T>(IEnumerable<Link> relatedLinks, ICollection<Type> allowedViewModelTypes);
    }
}