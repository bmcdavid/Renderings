using System;

namespace Renderings.UmbracoCms.Search
{
    public interface IContentIndexItem
    {
        /// <summary>
        /// Field name to add or modify
        /// </summary>
        string FieldName { get; }

        /// <summary>
        /// Value to index
        /// </summary>
        string Value { get; }

        /// <summary>
        /// Determines if Value is stored in the lucene index, useful for sortable date fields
        /// </summary>
        bool Store { get; }

        /// <summary>
        /// Indicates type for value
        /// </summary>
        Type ValueType { get; }

        /// <summary>
        /// Indicates if __Sortable field is added to the index
        /// </summary>
        bool Sortable { get; }

        /// <summary>
        /// For IsDate == false, determines if Value is Analyzed
        /// </summary>
        bool Analyzed { get; }
    }
}