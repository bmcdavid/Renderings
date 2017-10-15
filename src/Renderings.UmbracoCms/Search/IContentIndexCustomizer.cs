using System.Collections.Generic;
using UmbracoExamine.DataServices;

namespace Renderings.UmbracoCms.Search
{
    /// <summary>
    /// Default indexer names are usually 'ExternalIndexer' and 'InternalIndexer'
    /// </summary>
    public interface IContentIndexCustomizer
    {
        /// <summary>
        /// If true, executes CustomizeItems
        /// </summary>
        /// <param name="documentAlias"></param>
        /// <param name="indexerName"></param>
        /// <returns></returns>
        bool CanIndex(string documentAlias, string indexerName);

        /// <summary>
        /// Returned ISearchIndexItems must have unique names and not use existing ones!
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="dataService"></param>
        /// <returns></returns>
        IEnumerable<IContentIndexItem> CustomizeItems(Dictionary<string, string> fields, IDataService dataService);
    }
}