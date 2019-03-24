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
        /// <summary>
        /// Provides current umbraco context
        /// </summary>
        public virtual UmbracoContext Current => Umbraco.Web.Composing.Current.UmbracoContext;
    }
}