using System;
using desExt.Editor.Model;
using desExt.Runtime.Variables;
using UnityEditor;

namespace desExt.Editor.Editors
{
    public partial class VariablesManagerEditor
    {
        private class VariablePostProcessor : AssetPostprocessor
        {
            private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets,
                string[] movedAssets,
                string[] movedFromAssetPaths)
            {
                // New variables
                foreach (var assetPath in importedAssets)
                {
                    ProcessBaseVariable(assetPath,
                        t => { VariablesManagerEditor.AddVariable(t, new VariableData(assetPath)); });
                }

                // Removed variables
                foreach (var assetPath in deletedAssets)
                {
                    TryRemoveVariable(assetPath);
                }

                // Move assets
                var movedLength = movedAssets.Length == movedFromAssetPaths.Length
                    ? movedAssets.Length
                    : throw new ArgumentException("MovedAssets array Length not equal to MovedFromAssetPaths length!");

                for (int i = 0; i < movedLength; i++)
                {
                    ProcessBaseVariable(movedAssets[i], t =>
                    {
                        var variableData = GetVariableData(t);
                        if (variableData.VariablePath.Equals(movedFromAssetPaths[i]))
                            variableData.VariablePath = movedAssets[i];
                    });
                }
            }

            private static void ProcessBaseVariable(string path, Action<BaseVariable> process)
            {
                var assetType = AssetDatabase.GetMainAssetTypeAtPath(path);

                if (assetType.IsSubclassOf(typeof(BaseVariable)))
                {
                    var asset = AssetDatabase.LoadAssetAtPath<BaseVariable>(path);

                    process(asset);
                }
            }
        }
    }
}