using DotNetStarter.Abstractions;
using Umbraco.Web;

namespace Renderings.UmbracoCms
{
    /// <summary>
    /// Injectable UmbracoContext accessor
    /// </summary>
    [Registration(typeof(UmbracoContextProvider), Lifecycle.Singleton)]
    public class UmbracoContextProvider
    {
        public virtual UmbracoContext Current => UmbracoContext.Current;
    }
}