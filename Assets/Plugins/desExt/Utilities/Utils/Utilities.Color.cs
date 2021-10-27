using UnityEngine;

public static partial class Utilities
{
    /// <summary>
    /// Changes the string color by surrounding the given string with HTML color tag of supplied color.
    /// </summary>
    /// <param name="color">Color to apply to the string.</param>
    public static string Color(this string stringToColor, Color color)
    {
        return $"<color=#{ColorUtility.ToHtmlStringRGBA(color)}>{stringToColor}</color>";
    }
}