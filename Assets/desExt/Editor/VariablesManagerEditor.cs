using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using desExt.Variables;
using UnityEditor;
using UnityEngine;
using UnityEngine.Profiling;
using Object = UnityEngine.Object;

namespace desExt.Editor
{
    public class VariableData
    {
        public string VariablePath;
        public bool FoldOut;

        public VariableData(string variablePath, bool foldOut = false)
        {
            VariablePath = variablePath;
            FoldOut = foldOut;
        }
    }

    public partial class VariablesManagerEditor : EditorWindow
    {
        private static Dictionary<BaseVariable, VariableData> _variables;
        private static Vector2 _scrollPosition;

        private static readonly string[] SkipFields = {"m_Script", "VariableCategory"};

        private static Dictionary<BaseVariable, VariableData> Variables =>
            _variables ?? (_variables = new Dictionary<BaseVariable, VariableData>());

        [MenuItem("Tools/Game Manager")]
        public static void Init()
        {
            var gameManagerWindow = (VariablesManagerEditor) GetWindow(typeof(VariablesManagerEditor));
            gameManagerWindow.titleContent = new GUIContent("Game Manager");

            LoadVariables();

            gameManagerWindow.Show();
        }

        private static void AddVariable(BaseVariable variable, VariableData variableData)
        {
            if (Variables.ContainsKey(variable))
            {
                Variables[variable] = variableData;
            }
            else
            {
                Variables.Add(variable, variableData);
            }
        }

        private static VariableData GetVariableData(BaseVariable variable)
        {
            return Variables[variable];
        }

        private static void TryRemoveVariable(string assetPath)
        {
            var variable = Variables.Keys.FirstOrDefault(key => Variables[key].VariablePath == assetPath);
            if (variable != null)
            {
                Variables.Remove(variable);
            }
        }

        private static void LoadVariables()
        {
            foreach (var baseVariable in LoadAllAssetsAndPathOfType<BaseVariable>())
            {
                Variables.Add(baseVariable.Item1, new VariableData(baseVariable.Item2));
            }
        }

        private static HashSet<(T, string)> LoadAllAssetsAndPathOfType<T>()
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

        private void OnGUI()
        {
            Profiler.BeginSample("desExt");

            if (GUILayout.Button(new GUIContent("Refresh objects")))
            {
                Variables.Clear();
                LoadVariables();
            }

            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);

            var keys = new List<BaseVariable>(Variables.Keys);
            foreach (var variable in keys)
            {
                if (variable == null)
                    continue;

                Variables[variable].FoldOut = EditorGUILayout.Foldout(Variables[variable].FoldOut, variable.name);
                if (Variables[variable].FoldOut)
                {
                    var serializedObject = new SerializedObject(variable);
                    serializedObject.Update();
                    EditorGUI.indentLevel++;

                    var property = serializedObject.GetIterator();
                    property.NextVisible(true);

                    do
                    {
                        if (!SkipFields.Contains(property.name))
                        {
                            EditorGUILayout.PropertyField(property);
                        }
                    } while (property.NextVisible(false));


                    EditorGUI.indentLevel--;

                    serializedObject.ApplyModifiedProperties();
                }
            }

            EditorGUILayout.EndScrollView();

            Profiler.EndSample();
        }
    }
}