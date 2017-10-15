using System;

namespace Renderings.UmbracoCms.Search
{
    /// <summary>
    /// Default implementation for IContentIndexItem used to customize lucene indexes
    /// </summary>
    public class ContentIndexItem : IContentIndexItem
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="searchIndexName"></param>
        /// <param name="value"></param>
        /// <param name="store"></param>
        /// <param name="valueType"></param>
        /// <param name="sortable"></param>
        /// <param name="analyzed"></param>
        public ContentIndexItem(string searchIndexName, string value, bool store = false, Type valueType = null, bool sortable = false, bool analyzed = false)
        {
            FieldName = searchIndexName;
            Value = value;
            Store = store;
            ValueType = valueType ?? typeof(string);
            Analyzed = analyzed;
            Sortable = sortable;
        }

        /// <summary>
        /// Fieldname for lucene index
        /// </summary>
        public string FieldName { get; }

        /// <summary>
        /// Value for lucene field
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// Keep the value in the lucene index
        /// </summary>
        public bool Store { get; }

        /// <summary>
        /// The type of value, useful for DateTime conversions
        /// </summary>
        public Type ValueType { get; }

        /// <summary>
        /// Determines if value is analyzed or not
        /// </summary>
        public bool Analyzed { get; }

        /// <summary>
        /// Determines if value is sortable, which prefixes with fieldname with  __Sort_ 
        /// </summary>
        public bool Sortable { get; }
    }
}