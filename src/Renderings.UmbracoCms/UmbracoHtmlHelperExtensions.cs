using System;
using System.Web;
using System.Web.Mvc;

namespace Renderings.UmbracoCms
{
    /// <summary>
    /// Extensions for getting/settings items in current http context
    /// </summary>
    public static class UmbracoHtmlHelperExtensions
    {
        private static readonly Guid CurrentPageViewModelKey = Guid.NewGuid();

        private static readonly Guid CurrentSiteHomePageId = Guid.NewGuid();

        private static readonly Guid CurrentSiteSettings = Guid.NewGuid();

        /// <summary>
        /// Gets the current homepage id, if set
        /// </summary>
        /// <param name="helper"></param>
        /// <returns></returns>
        public static int GetCurrentHomePageId(this HtmlHelper helper)
        {
            return GetCurrentHomePageId(helper.ViewContext.HttpContext);
        }

        /// <summary>
        /// Gets the current homepage id, if set
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public static int GetCurrentHomePageId(this HttpContextBase httpContext)
        {
            return GetHttpItem<int>(httpContext, CurrentSiteHomePageId);
        }

        /// <summary>
        /// Gets the current page model, if set
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="helper"></param>
        /// <returns></returns>
        public static TModel GetCurrentPageModel<TModel>(this HtmlHelper helper) where TModel : IUmbracoRendering
        {
            return GetHttpItem<TModel>(helper.ViewContext.HttpContext, CurrentPageViewModelKey);
        }

        /// <summary>
        /// Gets the current site settings, if set
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="helper"></param>
        /// <returns></returns>
        public static TModel GetCurrentSiteSettings<TModel>(this HtmlHelper helper) where TModel : IUmbracoRendering
        {
            return GetCurrentSiteSettings<TModel>(helper.ViewContext.HttpContext);
        }

        /// <summary>
        /// Gets the current site settings, if set
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public static TModel GetCurrentSiteSettings<TModel>(this HttpContextBase httpContext) where TModel : IUmbracoRendering
        {
            return GetHttpItem<TModel>(httpContext, CurrentSiteSettings);
        }

        /// <summary>
        /// Sets current homepage node id
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="homePageId"></param>
        public static void SetCurrentHomePageId(this HttpContextBase httpContext, int homePageId)
        {
            ThrowIfNullHttpContext(httpContext);

            SetHttpItem(httpContext, CurrentSiteHomePageId, homePageId);
        }

        /// <summary>
        /// Sets current IRendering model
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="httpContext"></param>
        /// <param name="model"></param>
        public static void SetCurrentPageModel<TModel>(this HttpContextBase httpContext, TModel model) where TModel : IUmbracoRendering
        {
            ThrowIfNullHttpContext(httpContext);

            if (model == null)
                throw new ArgumentException(nameof(model));

            SetHttpItem(httpContext, CurrentPageViewModelKey, model);
        }

        /// <summary>
        /// Sets current site settings instance
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="httpContext"></param>
        /// <param name="model"></param>
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