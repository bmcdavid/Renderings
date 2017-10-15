using Examine;
using System.Collections.Generic;
using Umbraco.Core.Models;

namespace Renderings.UmbracoCms.Search
{
    public interface IDocumentSearchService
    {
        /// <summary>
        /// Searches for document types using Examine.
        /// <para>Important: For paging do Skip and Take then ToList directly on the ISearchResults before converting to IPublishedContent</para>
        /// </summary>
        /// <param name="documentTypes"></param>
        /// <param name="searchGroups"></param>
        /// <param name="sortFields"></param>
        /// <param name="includeHiddenNavigation"></param>
        /// <param name="sortDescending"></param>
        /// <returns></returns>
        ISearchResults Search(IEnumerable<string> documentTypes, IEnumerable<SearchGroup> searchGroups, IEnumerable<string> sortFields = null, bool sortDescending = false);

        IEnumerable<IPublishedContent> ConvertSearchResultsToPublishedContent(ISearchResults allResults, int pageNumber, int pageSize);
    }
}