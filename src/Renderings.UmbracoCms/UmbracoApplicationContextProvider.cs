using DotNetStarter.Abstractions;
using Umbraco.Core;

namespace Renderings.UmbracoCms
{
    /// <summary>
    /// Injectable ApplicationContext access
    /// </summary>
    [Registration(typeof(UmbracoApplicationContextProvider), Lifecycle.Singleton)]
    public class UmbracoApplicationContextProvider
    {
        /// <summary>
        /// Provides the current application context, which can be mocked for unit testing
        /// </summary>
        public virtual ApplicationContext Current => ApplicationContext.Current;
    }
}