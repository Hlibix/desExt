using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace desExt.Editor.Utils
{
    public static class EditorUtils
    {
        public static HashSet<(T, string)> LoadAllAssetsAndPathsOfType<T>()
            where T : Object
        {
            var guids = AssetDatabase.FindAssets($"t:{typeof(T)}");

            var assets = new HashSet<(T, string)>();

            foreach (var guid in guids)
            {
                var assetPath = AssetDatabase.GUIDToAssetPath(guid);
                var asset = AssetDatabase.LoadAssetAtPath<T>(assetPath);

                if (asset != null)
                {
                    assets.Add((asset, assetPath));
                }
            }

            return assets;
        }

        public static GUIContent ToGuiContent(this string label)
        {
            return new GUIContent(label);
        }
    }
}