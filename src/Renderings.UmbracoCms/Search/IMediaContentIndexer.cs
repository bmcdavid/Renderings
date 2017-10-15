namespace Renderings.UmbracoCms.Search
{
    using System.Collections.Generic;
    using UmbracoExamine.DataServices;

    /// <summary>
    /// Allows for reading of uploaded file contents
    /// </summary>
    public interface IMediaContentIndexer
    {
        /// <summary>
        /// Read file contents for indexing
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="indexFields"></param>
        /// <param name="dataService"></param>
        /// <returns></returns>
        string ReadMediaContents(string filePath, IReadOnlyDictionary<string, string> indexFields, IDataService dataService);
    }
}