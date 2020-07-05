using UnityEngine;

namespace desExt.Runtime.Utils
{
    internal static class Logging
    {
        private const string Header = "desExt";

        public static void LogError(string error)
        {
            Debug.LogError(GetFormattedString(error));
        }

        public static void LogWarning(string error)
        {
            Debug.LogWarning(GetFormattedString(error));
        }

        public static void LogWarning(string error, Object context)
        {
            Debug.LogWarning(GetFormattedString(error), context);
        }

        private static string GetFormattedString(string error)
        {
            return Header.Color(Color.blue).Bracketize() + ": " + error;
        }
    }
}