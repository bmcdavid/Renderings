using DotNetStarter.Abstractions;
using Examine;
using Examine.SearchCriteria;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace Renderings.UmbracoCms.Search
{
    /// <summary>
    /// Default implementation of IDocumentSearchService
    /// </summary>
    [Registration(typeof(IDocumentSearchService), Lifecycle.Transient)]
    public class DefaultDocumentSearchService : IDocumentSearchService
    {
        private readonly UmbracoHelper _uHelper;

        /// <summary>
        /// Default constructor for SearchHelper
        /// </summary>
        /// <param name="uHelper">An umbraco helper to use in your class</param>
        public DefaultDocumentSearchService(UmbracoHelperProvider uHelper)
        {
            _uHelper = uHelper.CreateHelper();
        }

        /// <summary>
        /// Performs a lucene search using Examine.
        /// </summary>
        /// <param name="documentTypes">Document type aliases to search for.</param>
        /// <param name="searchGroups">A list of search groupings, if you have more than one group it will apply an and to the search criteria</param>
        /// <param name="sortFields">optional sort fields.</param>
        /// <param name="sortDescending">sort direction</param>
        /// <returns>Examine search results</returns>
        public ISearchResults Search(IEnumerable<string> documentTypes, IEnumerable<SearchGroup> searchGroups, IEnumerable<string> sortFields = null, bool sortDescending = false)
        {
            var searchManager = ExamineManager.Instance; // uses the default providers
            ISearchCriteria searchCriteria = searchManager.CreateSearchCriteria(BooleanOperation.And);
            IBooleanOperation queryNodes;

            // initialize query
            if (sortFields == null)
            {
                queryNodes = searchCriteria.GroupedNot(new string[] { "__ignoreMe" }, new string[] { "ignoreMe" });
            }
            else if (sortDescending)
            {
                queryNodes = searchCriteria.OrderByDescending(sortFields.ToArray());
            }
            else
            {
                queryNodes = searchCriteria.OrderBy(sortFields.ToArray());
            }

            if (documentTypes?.Any() == true)
            {
                //only get results for documents of a certain type
                queryNodes = queryNodes.And().GroupedOr(new string[] { Constants.PropertyAlias.NodeTypeAlias }, documentTypes.ToArray());
            }

            if (searchGroups?.Any() == true)
            {
                //in each search group it looks for a match where the specified fields contain any of the specified search terms
                //usually would only have 1 search group, unless you want to filter out further, i.e. using categories as well as search terms
                foreach (SearchGroup searchGroup in searchGroups)
                {
                    queryNodes = queryNodes.And().GroupedOr(searchGroup.FieldsToSearchIn, searchGroup.SearchTerms.ToArray());
                }
            }

            var compileQuery = queryNodes.Compile();

            //return the results of the search
            return searchManager.Search(compileQuery);
        }

        /// <summary>
        /// Takes the examine search results and return the content for each page
        /// </summary>
        /// <param name="allResults">The examine search results</param>
        /// <param name="pageNumber">The page number of results to return</param>
        /// <param name="pageSize">The number of items per page</param>
        /// <returns>A collection of content pages for the page of results</returns>
        public IEnumerable<IPublishedContent> ConvertSearchResultsToPublishedContent(ISearchResults allResults, int pageNumber, int pageSize)
        {
            var pagedSet = allResults.Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(x => x.Id).ToList();

            return _uHelper.TypedContent(pagedSet)
                        .Union(_uHelper.TypedMedia(pagedSet));
        }
    }
}