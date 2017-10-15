using System;
using System.Collections.Generic;

namespace Renderings
{
    public interface IRenderingTypeFinder
    {
        IEnumerable<Type> GetTypesFor<T>() where T : IRendering;
    }
}
