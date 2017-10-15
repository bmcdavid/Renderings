using System;

namespace Renderings.UmbracoCms.Search
{
    /// <summary>
    /// Allows customized lucene index fields
    /// </summary>
    public interface IContentIndexItem
    {
        /// <summary>
        /// For IsDate == false, determines if Value is Analyzed
        /// </summary>
        bool Analyzed { get; }

        /// <summary>
        /// Field name to add or modify
        /// </summary>
        string FieldName { get; }

        /// <summary>
        /// Indicates if __Sortable field is added to the index
        /// </summary>
        bool Sortable { get; }

        /// <summary>
        /// Determines if Value is stored in the lucene index, useful for sortable date fields
        /// </summary>
        bool Store { get; }

        /// <summary>
        /// Value to index
        /// </summary>
        string Value { get; }

        /// <summary>
        /// Indicates type for value
        /// </summary>
        Type ValueType { get; }
    }
}