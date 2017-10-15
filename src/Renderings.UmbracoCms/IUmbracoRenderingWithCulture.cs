using System.Globalization;

namespace Renderings.UmbracoCms
{
    /// <summary>
    /// Umbraco rendering with settable culture
    /// </summary>
    public interface IUmbracoRenderingWithCulture : IUmbracoRendering
    {
        /// <summary>
        /// Umbraco rendering with culture
        /// </summary>
        CultureInfo CurrentCulture { get; set; }
    }
}