using System.Collections.Generic;
using System.Linq;
using desExt.Editor.Utils;
using desExt.Runtime.Variables;
using UnityEditor;
using UnityEngine;
using UnityEngine.Profiling;

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

        [MenuItem("desExt/Variables Manager")]
        public static void Init()
        {
            var gameManagerWindow = (VariablesManagerEditor) GetWindow(typeof(VariablesManagerEditor));
            gameManagerWindow.titleContent = new GUIContent("Variables Manager");

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
            foreach (var baseVariable in EditorUtils.LoadAllAssetsAndPathsOfType<BaseVariable>())
            {
                Variables.Add(baseVariable.Item1, new VariableData(baseVariable.Item2));
            }
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