using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

public static partial class Utilities
{
    /// <summary>
    /// Returns a random element from the given collection.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown if the collection is empty.</exception>
    public static T RandomElement<T>(this IEnumerable<T> collection)
    {
        var count = collection.Count();

        switch (count)
        {
            case 0:
                throw new InvalidOperationException("Provided collection is empty!");
            case 1:
                return collection.First();
            default:
                return collection.ElementAt(Random.Range(0, count));
        }
    }
}