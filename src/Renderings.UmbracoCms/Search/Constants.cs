namespace Renderings.UmbracoCms.Search
{
    public static class Constants
    {
        public static class Indexers
        {
            public const string ExternalIndexer = "ExternalIndexer";

            public const string InternalIndexer = "InternalIndexer";
        }

        public static class InternalSearchField
        {
            public const string IndexType = "__IndexType";

            public const string NodeId = "__NodeId";
        }

        public static class PropertyAlias
        {
            public const string NodeTypeAlias = "nodeTypeAlias";

            public const string NodeName = "nodeName";

            public const string NodePath = "path";

            public const string UmbracoNavigationHide = "umbracoNaviHide";

            public const string UmbracoFilePath = "umbracoFile";
        }

        public static class PropertyValue
        {
            public const string TrueString = "1";

            public const string FalseString = "0";

            public const string DateTimeMillisecondFormat = "yyyyMMddHHmmssfff";

            public const string DateTimeSecondFormat = "yyyyMMddHHmmss";

            public const string DateTimeDateNoTimeFormat = "yyyyMMdd";
        }
    }
}