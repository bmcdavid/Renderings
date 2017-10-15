using System;
using System.Web;
using System.Web.Mvc;

namespace Renderings.UmbracoCms
{
    public static class UmbracoHtmlHelperExtensions
    {
        private static readonly Guid CurrentPageViewModelKey = Guid.NewGuid();

        private static readonly Guid CurrentSiteHomePageId = Guid.NewGuid();

        private static readonly Guid CurrentSiteSettings = Guid.NewGuid();

        public static int GetCurrentHomePageId(this HtmlHelper helper)
        {
            return GetCurrentHomePageId(helper.ViewContext.HttpContext);
        }

        public static int GetCurrentHomePageId(this HttpContextBase httpContext)
        {
            return GetHttpItem<int>(httpContext, CurrentSiteHomePageId);
        }

        public static TModel GetCurrentPageModel<TModel>(this HtmlHelper helper) where TModel : IUmbracoRendering
        {
            return GetHttpItem<TModel>(helper.ViewContext.HttpContext, CurrentPageViewModelKey);
        }

        public static TModel GetCurrentSiteSettings<TModel>(this HtmlHelper helper) where TModel : IUmbracoRendering
        {
            return GetCurrentSiteSettings<TModel>(helper.ViewContext.HttpContext);
        }

        public static TModel GetCurrentSiteSettings<TModel>(this HttpContextBase httpContext) where TModel : IUmbracoRendering
        {
            return GetHttpItem<TModel>(httpContext, CurrentSiteSettings);
        }

        public static void SetCurrentHomePageId(this HttpContextBase httpContext, int homePageId)
        {
            ThrowIfNullHttpContext(httpContext);

            SetHttpItem(httpContext, CurrentSiteHomePageId, homePageId);
        }

        public static void SetCurrentPageModel<TModel>(this HttpContextBase httpContext, TModel model) where TModel : IUmbracoRendering
        {
            ThrowIfNullHttpContext(httpContext);

            if (model == null)
                throw new ArgumentException(nameof(model));

            SetHttpItem(httpContext, CurrentPageViewModelKey, model);
        }

        public static void SetCurrentSiteSettings<TModel>(this HttpContextBase httpContext, TModel model) where TModel : IUmbracoRendering
        {
            ThrowIfNullHttpContext(httpContext);

            if (model == null)
                throw new ArgumentException(nameof(model));

            SetHttpItem(httpContext, CurrentSiteSettings, model);
        }

        private static T GetHttpItem<T>(HttpContextBase httpContext, object key)
        {
            return (T)httpContext.Items[key];
        }

        private static void SetHttpItem(HttpContextBase httpContext, object key, object value)
        {
            httpContext.Items[key] = value;
        }

        private static void ThrowIfNullHttpContext(HttpContextBase httpContext)
        {
            if (httpContext == null)
                throw new ArgumentNullException(nameof(httpContext));
        }
    }
}