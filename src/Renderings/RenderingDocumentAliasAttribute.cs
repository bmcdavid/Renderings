using Renderings;
using System;

[assembly: DotNetStarter.Abstractions.DiscoverableAssembly]
[assembly: DotNetStarter.Abstractions.DiscoverTypes(typeof(RenderingDocumentAliasAttribute))]

namespace Renderings
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class RenderingDocumentAliasAttribute : Attribute
    {
        public RenderingDocumentAliasAttribute(string documentAlias, bool backendDocument = false, string description = null)
        {
            DocumentAlias = documentAlias;
            Description = description ?? "Genral document type";
            BackendDocument = backendDocument;
        }

        //todo: should there be long,guid stored conversions?

        public string DocumentAlias { get; private set; }

        public string Description { get; private set; }

        public bool BackendDocument { get; private set; }

        public override string ToString()
        {
            return string.Concat(DocumentAlias, BackendDocument ? "(backend type only)" : string.Empty, ", ", Description);
        }
    }
}
