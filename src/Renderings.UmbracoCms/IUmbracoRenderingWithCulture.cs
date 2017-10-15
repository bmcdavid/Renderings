using System.Globalization;

namespace Renderings.UmbracoCms
{
    public interface IUmbracoRenderingWithCulture : IUmbracoRendering
    {
        CultureInfo CurrentCulture { get; set; }
    }
}