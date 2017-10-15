namespace Renderings.UmbracoCms.Search
{
    using DotNetStarter.Abstractions;
    using System.Collections.Generic;
    using UmbracoExamine.DataServices;
    using static Constants;

    /// <summary>
    /// The default search path has commas, this replaces them with a whitespace
    /// </summary>
    [Registration(typeof(IContentIndexCustomizer), Lifecycle.Singleton)]
    public class DefaultSearchIndexCustomizer : IContentIndexCustomizer
    {
        public const string CustomSearchPathFieldName = "__CustomizedSearchablePath";

        /// <summary>
        /// When true, disables the indexing
        /// </summary>
        public static bool DisableCustomizer { get; set; }

        public bool CanIndex(string documentAlias, string indexerName)
        {
            return string.IsNullOrWhiteSpace(documentAlias) == false && DisableCustomizer == false;
        }

        public IEnumerable<IContentIndexItem> CustomizeItems(Dictionary<string, string> fields, IDataService dataService)
        {
            List<IContentIndexItem> customizers = new List<IContentIndexItem>();

            if (fields.TryGetValue(PropertyAlias.NodePath, out string path))
            {
                customizers.Add(new ContentIndexItem(CustomSearchPathFieldName, path.Replace(",", " "), analyzed: true));
            }

            // for IDocumentSearchService compatibility
            if (!fields.ContainsKey(PropertyAlias.UmbracoNavigationHide))
            {
                customizers.Add(new ContentIndexItem(PropertyAlias.UmbracoNavigationHide, PropertyValue.FalseString, analyzed: true));
            }

            return customizers;
        }
    }
}