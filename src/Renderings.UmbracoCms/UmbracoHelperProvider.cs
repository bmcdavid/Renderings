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
        /// Creates umbraco helper
        /// </summary>
        /// <param name="umbracoContext"></param>
        /// <returns></returns>
        public virtual UmbracoHelper CreateHelper(UmbracoContext umbracoContext = null) => new UmbracoHelper(umbracoContext ?? UmbracoContext.Current);
    }
}