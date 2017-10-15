using System;

namespace Renderings
{
    /// <summary>
    /// Decorates rendering properties for string aliases
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public class RenderingPropertyAliasAttribute : Attribute
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="propertyAlias"></param>
        public RenderingPropertyAliasAttribute(string propertyAlias)
        {
            PropertyAlias = propertyAlias;
        }

        /// <summary>
        /// String alias for property
        /// </summary>
        public string PropertyAlias { get; private set; }
    }
}
