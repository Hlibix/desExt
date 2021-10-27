using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public static partial class Utilities
{
    /// <summary>
    /// Returns a dynamic list of child objects.
    /// If the object has no children, returns null.
    /// </summary>
    /// <param name="parent"></param>
    /// <returns></returns>
    public static List<GameObject> GetChildrenInList(this GameObject parent)
    {
        List<GameObject> list = new List<GameObject>();

        if (parent.transform.childCount == 0) return null;

        for (int i = 0; i < parent.transform.childCount; i++)
        {
            list.Add(parent.transform.GetChild(i).gameObject);
        }

        return list;
    }

    public static List<T> GetChildrenInList<T>(this GameObject parent)
    {
        List<T> list = new List<T>();

        if (parent.transform.childCount == 0) return null;

        for (int i = 0; i < parent.transform.childCount; i++)
        {
            list.Add(parent.transform.GetChild(i).GetComponent<T>());
        }

        return list;
    }

    public static List<T> Collect<T>(this IEnumerable enumerable)
    {
        var list = new List<T>();
        foreach (T item in enumerable) list.Add(item);
        return list;
    }
}