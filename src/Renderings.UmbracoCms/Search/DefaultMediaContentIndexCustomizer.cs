namespace Renderings.UmbracoCms.Search
{
    using DotNetStarter.Abstractions;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using UmbracoExamine;
    using UmbracoExamine.DataServices;
    using static Constants;

    /// <summary>
    /// The default search path has commas, this replaces them with a whitespace
    /// </summary>
    [Registration(typeof(IContentIndexCustomizer), Lifecycle.Singleton)]
    public class DefaultMediaContentIndexCustomizer : IContentIndexCustomizer
    {
        /// <summary>
        /// Default allowed is 'ExternalIndexer', set to null to disable this customizer
        /// </summary>
        public static IEnumerable<string> AllowedIndexers { get; set; } = new string[] { "ExternalIndexer" };

        public const string FileContentSearchField = "__FileContents";

        private readonly IMediaContentIndexer _MediaContentIndexer;

        public DefaultMediaContentIndexCustomizer(IMediaContentIndexer mediaContentIndexer)
        {
            _MediaContentIndexer = mediaContentIndexer;
        }

        public bool CanIndex(string documentAlias, string indexerName)
        {
            return string.IsNullOrWhiteSpace(documentAlias) == false && AllowedIndexers?.Contains(indexerName) == true;
        }

        public IEnumerable<IContentIndexItem> CustomizeItems(Dictionary<string, string> fields, IDataService dataService)
        {
            List<IContentIndexItem> customizers = new List<IContentIndexItem>();

            if (fields.TryGetValue(InternalSearchField.IndexType, out string indexType))
            {
                switch (indexType)
                {
                    case IndexTypes.Media:
                        try
                        {
                            //note: images have json with src for source path
                            if (fields.TryGetValue(PropertyAlias.UmbracoFilePath, out string filePath))
                            {
                                if (filePath.StartsWith("{"))
                                {
                                    var jsonData = JObject.Parse(filePath);
                                    filePath = (string)jsonData["src"];
                                }
                            };

                            var mediaContent = _MediaContentIndexer.ReadMediaContents(filePath ?? "", new ReadOnlyDictionary<string, string>(fields), dataService);

                            if (!string.IsNullOrWhiteSpace(mediaContent))
                            {
                                customizers.Add(new ContentIndexItem(FileContentSearchField, mediaContent, analyzed: true));
                            }
                        }
                        catch (Exception ex)
                        {
                            int nodeId = -100;

                            if (fields.TryGetValue(InternalSearchField.NodeId, out string nodeIdString))
                            {
                                int.TryParse(nodeIdString, out nodeId);
                            }

                            dataService.LogService.AddErrorLog(nodeId, "An error occurred reading media contents: " + ex);
                        }
                        break;

                    default:
                        break;
                }
            }

            return customizers;
        }
    }
}