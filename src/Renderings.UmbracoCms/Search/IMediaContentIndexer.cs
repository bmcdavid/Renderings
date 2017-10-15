namespace Renderings.UmbracoCms.Search
{
    using System.Collections.Generic;
    using UmbracoExamine.DataServices;

    public interface IMediaContentIndexer
    {
        string ReadMediaContents(string filePath, IReadOnlyDictionary<string, string> indexFields, IDataService dataService);
    }
}