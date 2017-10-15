using Umbraco.Web.Models;

namespace Renderings.UmbracoCms
{
    /// <summary>
    /// Used by related link to model converter to have additional information
    /// </summary>
    public interface ISetRelatedLink
    {
        /// <summary>
        /// Provides the related link information to the rendering model
        /// </summary>
        /// <param name="link"></param>
        void SetLink(RelatedLink link);
    }
}