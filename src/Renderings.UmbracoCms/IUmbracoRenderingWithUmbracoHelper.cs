using Umbraco.Web;

namespace Renderings.UmbracoCms
{
    public interface IUmbracoRenderingWithUmbracoHelper : IUmbracoRendering
    {
        UmbracoHelper Umbraco { get; }
    }
}