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

        private static string GetFormattedString(string error)
        {
            return Header.Color(Color.blue).Bracketize() + ": " + error;
        }
    }
}