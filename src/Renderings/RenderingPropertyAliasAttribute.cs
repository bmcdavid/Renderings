using System;

namespace Renderings
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public class RenderingPropertyAliasAttribute : Attribute
    {
        public RenderingPropertyAliasAttribute(string propertyAlias)
        {
            PropertyAlias = propertyAlias;
        }

        public string PropertyAlias { get; private set; }
    }
}
