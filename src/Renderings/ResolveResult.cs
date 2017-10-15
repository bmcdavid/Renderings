using System;

namespace Renderings
{
    /// <summary>
    /// Provides information about a resolve request
    /// </summary>
    public class ResolveResult
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="modelType"></param>
        /// <param name="descriptor"></param>
        /// <param name="alias"></param>
        public ResolveResult(Type modelType = null, RenderingDocumentAliasAttribute descriptor = null, string alias = null)
        {
            ModelType = modelType;
            Descriptor = descriptor;
            DocumentAlias = alias;
        }

        /// <summary>
        /// Matching model type for string alias or null
        /// </summary>
        public Type ModelType { get; }

        /// <summary>
        /// Model type attribute or null
        /// </summary>
        public RenderingDocumentAliasAttribute Descriptor { get; }

        /// <summary>
        /// String alias for model type
        /// </summary>
        public string DocumentAlias { get; }

        /// <summary>
        /// True if either ModelType or Descriptor are null
        /// </summary>
        public bool HasErrors
        {
            get
            {
                return ModelType == null || Descriptor == null;
            }
        }
    }
}
