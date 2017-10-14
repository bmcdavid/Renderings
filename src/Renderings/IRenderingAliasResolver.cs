using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Renderings
{
    public interface IRenderingAliasResolver
    {
        Type ResolveAlias(string documentAlias);

        IEnumerable<Type> ResolveAliases(ICollection<string> documentAliases);

        string ResolveType(Type renderingType);

        IEnumerable<string> ResolveTypes(ICollection<Type> renderingTypes);

        ResolveResult Resolve(string documentAlias);

        string ResolvePropertyAlias<T>(Expression<Func<T, object>> expression);
    }
}
