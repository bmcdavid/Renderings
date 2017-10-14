using System;

namespace Renderings
{
    public class ResolveResult
    {
        public ResolveResult(Type modelType = null, RenderingDocumentAliasAttribute descriptor = null, string alias = null)
        {
            ModelType = modelType;
            Descriptor = descriptor;
            DocumentAlias = alias;
        }

        public Type ModelType { get; }

        public RenderingDocumentAliasAttribute Descriptor { get; }

        public Type CreatorType { get; set; }

        public string DocumentAlias { get; }

        public bool HasErrors
        {
            get
            {
                return ModelType == null && Descriptor == null;
            }
        }
    }
}
