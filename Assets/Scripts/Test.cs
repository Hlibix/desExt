using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
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

        if (count <= 1)
            return collection.First();

        var t = 0.7f;

        if (t < 0.1)
        {
        }
        else if (t < 0.2)
        {
        }
        else if (t < 0.3)
        {
        }
        


        return collection.ElementAt(Random.Range(0, count));
    }
    
    static readonly ColorBlock a=new ColorBlock(){};
}