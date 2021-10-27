using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using desExt.Editor.Model;
using desExt.Editor.Utils;
using desExt.Runtime.Variables;
using UnityEditor;
using UnityEngine;
using UnityEngine.Profiling;

namespace desExt.Editor.Editors
{
    public partial class VariablesManagerEditor : EditorWindow
    {
        private static VariableGroup _groupBy;

        private static Dictionary<BaseVariable, VariableData> _variables;
        private static Vector2 _scrollPosition;

        private static readonly string[] SkipFields = {"m_Script", BaseVariable.SerializedVariableCategoryName};

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

            _groupBy = (VariableGroup) EditorGUILayout.EnumPopup("Group by:", _groupBy);

            EditorGUILayout.Separator();

            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);

            var keys = new List<BaseVariable>(Variables.Keys);

            switch (_groupBy)
            {
                case VariableGroup.Type:
                    var types = new HashSet<Type>(keys.Select(v => v.GetType()));

                    foreach (var type in types)
                    {
                        var typedVariables = keys.Where(v => v.GetType() == type);
                        ShowGroup(type.Name.Replace("Variable", ""), typedVariables);
                    }

                    break;
                case VariableGroup.Category:

                    foreach (var category in Runtime.Utils.Utils.GetEnumValues<VariableCategory>())
                    {
                        var categorizedVariables = Variables.Keys.Where(v => v.VariableCategory == category);

                        ShowGroup(category.ToString(), categorizedVariables);
                    }

                    break;
                case VariableGroup.None:
                    foreach (var variable in keys)
                    {
                        ShowVariable(variable);
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }


            // foreach (var variable in keys)
            // {
            //     ShowVariable(variable);
            // }

            EditorGUILayout.EndScrollView();

            Profiler.EndSample();
        }

        private static void ShowGroup(string labelName, IEnumerable<BaseVariable> variables)
        {
            EditorGUILayout.LabelField(labelName);
            EditorGUILayout.Separator();
            EditorGUI.indentLevel++;

            foreach (var variable in variables)
            {
                ShowVariable(variable);
            }

            EditorGUI.indentLevel--;
            EditorGUILayout.Separator();
        }

        private static void ShowVariable(BaseVariable variable)
        {
            if (variable == null)
                return;

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
    }
}