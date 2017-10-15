# Renderings Read Me

[![Build status](https://ci.appveyor.com/api/projects/status/a907wfniy73sk5de?svg=true)](https://ci.appveyor.com/project/bmcdavid/renderings)

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
* DotNetStarter.DryIoc or DotnetStarter.StructureMap (either one is fine)

Next create a custom global.asax inheriting from **Umbraco.Web.UmbracoApplication** and in the constructor execute DotNetStarter.ApplicationContext.Startup.

```cs
    public class Application : Umbraco.Web.UmbracoApplication
    {
        public Application()
        {
            IList<Assembly> discoverableAssemblies = DotNetStarter.ApplicationContext.GetScannableAssemblies();
            IEnumerable<Assembly> preFilteredAssemblies = new Assembly[]
            {
                    // needed for Umbraco UI with custom dependency resolver
                    typeof(Umbraco.Web.UmbracoApplication).Assembly,
                    typeof(Umbraco.Forms.Web.Controllers.UmbracoFormsController).Assembly,

                    // umbraco plugin, needing a nuget install...
                    typeof(Diplo.TraceLogViewer.Controllers.TraceLogTreeController).Assembly,

                    // types in this project
                    typeof(Application).Assembly
            };

            preFilteredAssemblies = preFilteredAssemblies.Union(discoverableAssemblies);

            DotNetStarter.ApplicationContext.Startup(assemblies: preFilteredAssemblies);
        }
    }
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