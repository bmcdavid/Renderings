namespace Renderings.UmbracoCms.Search
{
    using DotNetStarter.Abstractions;
    using System.Collections.Generic;
    using UmbracoExamine.DataServices;

    [Register(typeof(IMediaContentIndexer), LifeTime.Singleton)]
    public class DefaultMediaContentIndexer : IMediaContentIndexer
    {
        public string ReadMediaContents(string filePath, IReadOnlyDictionary<string, string> indexFields, IDataService dataService)
        {
            return null;
        }
    }
}