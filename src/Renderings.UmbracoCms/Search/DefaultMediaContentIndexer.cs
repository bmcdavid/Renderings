namespace Renderings.UmbracoCms.Search
{
    using DotNetStarter.Abstractions;
    using System.Collections.Generic;
    using UmbracoExamine.DataServices;

    /// <summary>
    /// Default implementation of IMediaContentIndexer
    /// </summary>
    [Registration(typeof(IMediaContentIndexer), Lifecycle.Singleton)]
    public class DefaultMediaContentIndexer : IMediaContentIndexer
    {
        /// <summary>
        /// Tries to read the contents of given filePath, default does nothing
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="indexFields"></param>
        /// <param name="dataService"></param>
        /// <returns></returns>
        public string ReadMediaContents(string filePath, IReadOnlyDictionary<string, string> indexFields, IDataService dataService)
        {
            return null;
        }
    }
}