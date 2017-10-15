using System.Collections.Generic;

namespace Renderings.UmbracoCms.Search
{
    /// <summary>
    /// Defines a GroupedOr for Examine searching
    /// </summary>
    public class SearchGroup
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="fieldsToSearchIn"></param>
        /// <param name="searchTerms"></param>
        public SearchGroup(IEnumerable<string> fieldsToSearchIn, IEnumerable<string> searchTerms)
        {
            FieldsToSearchIn = fieldsToSearchIn;
            SearchTerms = searchTerms;
        }

        /// <summary>
        /// Searchable field aliases
        /// </summary>
        public IEnumerable<string> FieldsToSearchIn { get; set; }

        /// <summary>
        /// Terms to search for
        /// </summary>
        public IEnumerable<string> SearchTerms { get; set; }
    }
}