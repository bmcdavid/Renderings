using Renderings;
using System;

[assembly: DotNetStarter.Abstractions.DiscoverTypes(typeof(RenderingDocumentAliasAttribute))]

namespace Renderings
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class RenderingDocumentAliasAttribute : Attribute
    {
        public RenderingDocumentAliasAttribute(string documentAlias, string description = null)
        {
            DocumentAlias = documentAlias;
            Description = description ?? "Genral document type";
        }

        public string DocumentAlias { get; private set; }

        public string Description { get; private set; }


        public override string ToString()
        {
            return string.Concat(DocumentAlias, ", ", Description);
        }
    }
}
