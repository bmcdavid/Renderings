using DotNetStarter.Abstractions;
using System.Web;
using Umbraco.Core.Configuration;
using Umbraco.Web;
using Umbraco.Web.Routing;

namespace Renderings.UmbracoCms
{
    /// <summary>
    /// Registers UmbracoContext and UmbracoHelper for scoped lifetime
    /// </summary>
    [StartupModule]
    public class UmbracoLocatorConfigure : ILocatorConfigure
    {
        void ILocatorConfigure.Configure(ILocatorRegistry locator, IStartupEngine engine)
        {
            // use scoped to not create many per web request
            locator.Add(typeof(UmbracoContext), _ => EnsureUmbracoContext(), LifeTime.Scoped);
            locator.Add(typeof(UmbracoHelper), _ => new UmbracoHelper(_.Get<UmbracoContext>()), LifeTime.Scoped);
        }

        private UmbracoContext EnsureUmbracoContext()
        {
            if (UmbracoContext.Current == null)
            {
                var baseHttpContext = new HttpContextWrapper(HttpContext.Current ?? EnsureHttpContext());
                var appContext = Umbraco.Core.ApplicationContext.Current;

                return UmbracoContext.EnsureContext
                (
                    baseHttpContext,
                    appContext,
                    new Umbraco.Web.Security.WebSecurity(baseHttpContext, appContext),
                    UmbracoConfig.For.UmbracoSettings(),
                    UrlProviderResolver.Current.Providers,
                    false
                );
            }

            return UmbracoContext.Current;
        }

        private HttpContext EnsureHttpContext()
        {
            if (HttpContext.Current == null)
            {
                var fakeContext = new HttpContext
                (
                    //todo: make a setting for url?
                    new HttpRequest("", "http://null-current-context", ""),
                    new HttpResponse(new System.IO.StringWriter())
                );

                HttpContext.Current = fakeContext;
                //new HttpContext(new System.Web.Hosting.SimpleWorkerRequest("temp.aspx", "", new System.IO.StringWriter());
            }

            return HttpContext.Current;
        }
    }
}