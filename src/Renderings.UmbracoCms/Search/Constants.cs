namespace Renderings.UmbracoCms.Search
{
    /// <summary>
    /// Constants to help with Examine
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// Default indexer names
        /// </summary>
        public static class Indexers
        {
            /// <summary>
            /// Default for external indexer
            /// </summary>
            public const string ExternalIndexer = "ExternalIndexer";

            /// <summary>
            /// Default for internal indexer
            /// </summary>
            public const string InternalIndexer = "InternalIndexer";
        }

        /// <summary>
        /// Useful internal search fields
        /// </summary>
        public static class InternalSearchField
        {
            /// <summary>
            /// Index type
            /// </summary>
            public const string IndexType = "__IndexType";

            /// <summary>
            /// Node Id
            /// </summary>
            public const string NodeId = "__NodeId";
        }

        /// <summary>
        /// Common Property Aliases
        /// </summary>
        public static class PropertyAlias
        {
            /// <summary>
            /// Node type alias default
            /// </summary>
            public const string NodeTypeAlias = "nodeTypeAlias";

            /// <summary>
            /// Node name default
            /// </summary>
            public const string NodeName = "nodeName";

            /// <summary>
            /// Node path default
            /// </summary>
            public const string NodePath = "path";

            /// <summary>
            /// UmbracoNaviHide
            /// </summary>
            public const string UmbracoNavigationHide = "umbracoNaviHide";

            /// <summary>
            /// UmbracoFilePath
            /// </summary>
            public const string UmbracoFilePath = "umbracoFile";
        }

        /// <summary>
        /// Common property values or formats
        /// </summary>
        public static class PropertyValue
        {
            /// <summary>
            /// True string value
            /// </summary>
            public const string TrueString = "1";

            /// <summary>
            /// False string value
            /// </summary>
            public const string FalseString = "0";

            /// <summary>
            /// Date time format for sorting in milliseconds
            /// </summary>
            public const string DateTimeMillisecondFormat = "yyyyMMddHHmmssfff";

            /// <summary>
            /// Date time format for sorting in seconds
            /// </summary>
            public const string DateTimeSecondFormat = "yyyyMMddHHmmss";

            /// <summary>
            /// Date time format no time
            /// </summary>
            public const string DateTimeDateNoTimeFormat = "yyyyMMdd";
        }
    }
}