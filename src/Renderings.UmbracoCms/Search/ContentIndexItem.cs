using System;

namespace Renderings.UmbracoCms.Search
{
    public class ContentIndexItem : IContentIndexItem
    {
        public ContentIndexItem(string searchIndexName, string value, bool store = false, Type valueType = null, bool sortable = false, bool analyzed = false)
        {
            FieldName = searchIndexName;
            Value = value;
            Store = store;
            ValueType = valueType ?? typeof(string);
            Analyzed = analyzed;
            Sortable = sortable;
        }

        public string FieldName { get; }

        public string Value { get; }

        public bool Store { get; }

        public Type ValueType { get; }

        public bool Analyzed { get; }

        public bool Sortable { get; }
    }
}