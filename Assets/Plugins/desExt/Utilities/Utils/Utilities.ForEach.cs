using System;
using System.Collections.Generic;
using System.Linq;

public static partial class Utilities
{
    /// <summary>
    /// Shorthand for calling ForEach for any collection.
    /// </summary>
    public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
    {
        collection.ToList().ForEach(action);
    }
}