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
        public virtual ApplicationContext Current => ApplicationContext.Current;
    }
}