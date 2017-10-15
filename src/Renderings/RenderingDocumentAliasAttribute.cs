using Renderings;
using System;

[assembly: DotNetStarter.Abstractions.DiscoverTypes(typeof(RenderingDocumentAliasAttribute))]

namespace Renderings
{
    /// <summary>
    /// Decorates classes used for renderings
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class RenderingDocumentAliasAttribute : Attribute
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="documentAlias"></param>
        /// <param name="description"></param>
        public RenderingDocumentAliasAttribute(string documentAlias, string description = null)
        {
            DocumentAlias = documentAlias;
            Description = description ?? "Genral document type";
        }

        /// <summary>
        /// Document alias string
        /// </summary>
        public string DocumentAlias { get; private set; }

        /// <summary>
        /// Document description
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// String conversion
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Concat(DocumentAlias, ", ", Description);
        }
    }
}
