using DotNetStarter.Abstractions;
using Umbraco.Web;

namespace Renderings.UmbracoCms
{
    /// <summary>
    /// This is safe to inject into singletons
    /// </summary>
    [Registration(typeof(UmbracoHelperProvider), Lifecycle.Singleton)]
    public class UmbracoHelperProvider
    {
        /// <summary>
        /// Gets an umbraco helper
        /// </summary>
        /// <returns></returns>
        public virtual UmbracoHelper Current => Umbraco.Web.Composing.Current.UmbracoHelper;
    }
}