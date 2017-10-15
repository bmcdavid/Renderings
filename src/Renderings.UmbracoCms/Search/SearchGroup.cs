using System.Collections.Generic;

namespace Renderings.UmbracoCms.Search
{
    public class SearchGroup
    {
        public SearchGroup(IEnumerable<string> fieldsToSearchIn, IEnumerable<string> searchTerms)
        {
            FieldsToSearchIn = fieldsToSearchIn;
            SearchTerms = searchTerms;
        }

        public IEnumerable<string> FieldsToSearchIn { get; set; }

        public IEnumerable<string> SearchTerms { get; set; }
    }
}