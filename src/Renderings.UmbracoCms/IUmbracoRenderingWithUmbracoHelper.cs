using Umbraco.Web;

namespace Renderings.UmbracoCms
{
    /// <summary>
    /// Umbraco rendering with an Umbraco Helper
    /// </summary>
    public interface IUmbracoRenderingWithUmbracoHelper : IUmbracoRendering
    {
        /// <summary>
        /// Umbraco helper instance
        /// </summary>
        UmbracoHelper Umbraco { get; }
    }
}