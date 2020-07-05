using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace desExt.Runtime.Utils
{
    public static class Utils
    {
        /// <summary>
        /// Changes the string color by surrounding the given string with HTML color tag of supplied color.
        /// </summary>
        /// <param name="color">Color to apply to the string.</param>
        public static string Color(this string stringToColor, Color color)
        {
            return $"<color=#{ColorUtility.ToHtmlStringRGBA(color)}>{stringToColor}</color>";
        }

        public static string Bracketize(this string inputString)
        {
            if (string.IsNullOrEmpty(inputString))
            {
                return "";
            }

            return '[' + inputString + ']';
        }

        public static IEnumerable<T> GetEnumValues<T>() where T : Enum
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }
    }
}