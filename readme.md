# Renderings Read Me

[![Build status](https://ci.appveyor.com/api/projects/status/pa469knyqeha6rrt?svg=true)](https://ci.appveyor.com/project/bmcdavid/renderings)

Package  | Version 
-------- | :------------ 
[Renderings](https://www.nuget.org/packages/Renderings/) |  [![NuGet version](https://badge.fury.io/nu/Renderings.svg)](https://badge.fury.io/nu/Renderings)
[Renderings.UmbracoCms](https://www.nuget.org/packages/Renderings.UmbracoCms/) |  [![NuGet version](https://badge.fury.io/nu/Renderings.UmbracoCms.svg)](https://badge.fury.io/nu/Renderings.UmbracoCms)

Requires [DotNetStarter](https://bmcdavid.github.io/DotNetStarter/) to run.

The goal of this package is to provide developers a framework for creating view models that support DI (dependency injection) using DotNetStarter. This package will not create document types from code, it will rather wrap IPublishedContent in a POCO for strongly typed view models to use in razor files.

The POCO view models need the following attribute:

```cs
[RenderingDocumentAlias("aliasString")]
```

These models will then get discovered by the startup process and registered to the DotNetStarter container.

## Umbraco Getting started

To use this package you need to install the following NuGet packages:

* Renderings.UmbracoCms
* DotNetStarter.Extensions.WebApi
* DotNetStarter.Extensions.Mvc
* DotNetStarter.DryIoc or DotnetStarter.StructureMap (either one is fine)
  * **NOTE** The container package dependencies may need to be updated to resolve issues.

Create a custom global.asax class inheriting from **Umbraco.Web.UmbracoApplication** and in the constructor execute DotNetStarter.ApplicationContext.Startup.

```cs
using DotNetStarter.Abstractions;
using DotNetStarter.Configure;
using System.Configuration;

namespace ExampleNamespace
{
    public class Application : Umbraco.Web.UmbracoApplication
    {
        private static StartupBuilder _startupBuilder; // to avoid trying to start twice

        /// <summary>
        /// Executs DotNetStarter.ApplicationContext.Startup, this class is used in the global.asax inherits
        /// </summary>
        public Application()
        {
            if (_startupBuilder != null) return;
            _startupBuilder = StartupBuilder.Create()
                .UseEnvironment(new DotNetStarter.StartupEnvironmentWeb(environmentName: ConfigurationManager.AppSettings["UmbracoEnv"]))
                .ConfigureAssemblies(assemblies =>
                {
                    assemblies
                    .WithDiscoverableAssemblies()
                    .WithAssemblyFromType<Umbraco.Web.UmbracoApplication>()// Scan for backoffice controllers
                    // add additional umbraco plugins, which inject controllers
                    //.WithAssemblyFromType<Umbraco.Forms.Web.Controllers.UmbracoFormsController>()
                    //.WithAssemblyFromType<Diplo.TraceLogViewer.Controllers.TraceLogTreeController>()
                    .WithAssemblyFromType<Application>();//types in this project
                })
                .OverrideDefaults(defaults =>
                {
                    defaults
                    // note: Only one locator is needed, and each of these implementations may also be passed an already configured DI container instance
                    //.UseLocatorRegistryFactory(new DotNetStarter.Locators.DryIocLocatorFactory())
                    //.UseLocatorRegistryFactory(new DotNetStarter.Locators.StructureMapFactory())
                    .UseLocatorRegistryFactory(new DotNetStarter.Locators.LightInjectLocatorRegistryFactory())
                    .UseLogger(new DotNetStarter.StringLogger(LogLevel.Error, 1024000)); // clears log after 1MB
                })
                .Build();
            _startupBuilder.Run();
        }
    }
}
```

Update the **global.asax** file's **Inherits** to use the full namespace of our custom class as noted below:

```cs
<%@ Application Inherits="Full.Namespace.Of.Class.Application" Language="C#" %>
```

Then create a custom default controller to create the rendering models
```cs
public class CustomApplicationBaseController : RenderMvcController
{
    private readonly IRenderingCreatorScoped _RenderingCreator;

    public CustomApplicationBaseController() { }

    public CustomApplicationBaseController(IRenderingCreatorScoped renderingCreator)
    {
        _RenderingCreator = renderingCreator;
    }

    public override ActionResult Index(RenderModel model)
    {
        var rendering = BuildRendering(model.Content, model.CurrentCulture);

        if (rendering == null)
        {
            return CurrentTemplate(model); // Fallback to default behaviour
        }

        if (rendering.IsFullPage == false)
        {
            return new HttpNotFoundResult(); // don't allow non full page models to return
        }

        return CurrentTemplate(rendering);
    }

    private IUmbracoRendering BuildRendering(IPublishedContent content, CultureInfo cultureInfo)
    {
        var creator = _RenderingCreator.GetCreator<IPublishedContent>(content.DocumentTypeAlias);
        var returnModel = creator.Invoke(content) as IUmbracoRendering;

        if (returnModel is IUmbracoRenderingWithCulture cultureModel)
        {
            cultureModel.CurrentCulture = cultureInfo;
        }

        return returnModel;
    }
}
```

Finally hijack the default MVC controller for Umbraco page content
```cs
/// <summary>
/// This class registers the base application controller and setups up error page routing
/// </summary>
public class ApplicationSetupMvc : ApplicationEventHandler
{
    protected override void ApplicationStarting(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
    {
        base.ApplicationStarting(umbracoApplication, applicationContext);

        // note: this will set all routes to be hijacked by this base controller
        Umbraco.Web.Mvc.DefaultRenderMvcControllerResolver.Current.SetDefaultControllerType(typeof(CustomApplicationBaseController));
    }
}
```
Also note, razor views will need to use one of the follow instead of **@inherits Umbraco.Web.Mvc.TemplatePage**
```cs
@model RenderingsExample.Models.ViewModels.Home // where class implements IUmbracoRendering
```
or
```cs
@inherits Umbraco.Web.Mvc.UmbracoViewPage<T> // where T is class implementing IUmbracoRendering
```

## Rendering Example

Renderings can be as simple or as complex as needed, below is a simple document type used to build hero slides:

```cs
[RenderingDocumentAlias("heroSlide")]
public class HeroSlide : IUmbracoRendering
{
    public HeroSlide(IPublishedContent content)
    {
        Content = content;
    }

    ///<summary>
    /// Mapped property to given IPublishedContent CMS content
    ///</summary>
    [RenderingPropertyAlias("title")]
    public string Title
    {
        get { return Content.GetPropertyValue<string>("title"); }
    }

    [RenderingPropertyAlias("description")]
    public string Description
    {
        get { return Content.GetPropertyValue<string>("description"); }
    }

    [RenderingPropertyAlias("link")]
    public RelatedLink Link
    {
        get { return Content.GetPropertyValue<RelatedLinks>("link")?.FirstOrDefault() ?? new RelatedLink() { Caption = "Link not set", Link = "#notset" }; }
    }

    [RenderingPropertyAlias("image")]
    public IPublishedContent Image
    {
        get { return Content.GetPropertyValue<IPublishedContent>("image"); }
    }

    /// <summary>
    /// Instructs the default controller to throw a HTTP 404 message
    /// </summary>
    public bool IsFullPage => false;

    public IPublishedContent Content { get; }

    /// <summary>
    /// Part of the IViewModel interface, which is a simplified template engine,
    /// allowing view models to decide how to render in partial views.
    /// </summary>
    /// <param name="renderTag"></param>
    /// <returns></returns>
    public string GetPartialView(string renderTag = null)
    {
        return "~/Views/Partials/HeroSlide.cshtml";
    }
}
```

View models can then be referenced using the built-in related links property as shown below on an example homepage document type:

```cs
[RenderingDocumentAlias("home")]
public class HomeViewModel : IUmbracoRendering
{
    public HomeViewModel(IPublishedContent content, IRelatedLinksToRenderingConverterScoped relatedLinksConverterScoped)
    {
        Content = content;
        _RelatedLinksConverter = relatedLinksConverterScoped;
    }

    public bool IsFullPage => true;

    public IPublishedContent Content { get; }

    private readonly IRelatedLinksToRenderingConverterScoped _RelatedLinksConverter;

    private IEnumerable<HeroSlide> _HeroSlides;

    /// <summary>
    /// Converts a RelatedLinks property to HeroSlide sequence.
    /// </summary>
    [RenderingPropertyAlias("heroSlider")]
    public IEnumerable<HeroSlide> HeroSlides
    {
        get
        {
            if (_HeroSlides == null)
            {
                _HeroSlides = _RelatedLinksConverter
                    .ConvertLinks<HeroSlide>(Content.GetPropertyValue<RelatedLinks>("heroSlider"),
                        new Type[] { typeof(HeroSlide) }
                    );
            }

            return _HeroSlides;
        }
    }

    /// <summary>
    /// We don't want to reuse homepage, so return "Empty" which corresponds to /Views/Partials/Empty.cshtml
    /// </summary>
    /// <param name="renderTag"></param>
    /// <returns></returns>
    public string GetPartialView(string renderTag = null)
    {
        return "Empty";
    }
}
```

## Using IRenderingAliasResolver to reduce magic strings

The sole purpose of the IRenderingAliasResolver is eliminate retyping document and property aliases throughout the code base. It can return a view model type from a string alias (hint: use IPublishedContent for this) or return a string alias for a given view model type.

For example getting a rendering type from alias:

```cs
// where _RenderingliasResolver is injected IRenderingAliasResolver
Type renderingType = _RenderingliasResolver.ResolveAlias(content.DocumentTypeAlias);
```
Or from type to string alias (useful for searching, filtering, etc)

```cs
// returns 'heroSlide' from previous view model example
string alias = _RenderingliasResolver.ResolveType(typeof(HeroSlide));
```

Or get a property alias from a view model
```cs
// returns 'title' from previous HeroSlide view model example
string propertyAlias = _RenderingliasResolver.ResolvePropertyAlias<HeroSlide>(slide => slide.Title);
```