using System;
using System.Collections.Generic;
using desExt.Editor.Utils;
using desExt.Runtime.Presets;
using desExt.Runtime.StaticScriptableObjects;
using UnityEditor;

namespace desExt.Editor.Editors
{
    [CustomEditor(typeof(StaticScriptableObjectsSettings))]
    public class StaticScriptableObjectSettingsEditor : UnityEditor.Editor
    {
        private bool[] _foldouts = new bool[Enum.GetNames(typeof(Preset)).Length];

        private Dictionary<Preset, List<SerializedProperty>> _groupedStaticScriptableObjects;

        private Dictionary<Preset, List<SerializedProperty>> GroupedStaticScriptableObjects
        {
            get
            {
                Update();

                return _groupedStaticScriptableObjects;
            }
        }

        private void OnValidate()
        {
            Update();
        }

        private void OnEnable()
        {
            Update();
        }

        private void Update()
        {
            _groupedStaticScriptableObjects = new Dictionary<Preset, List<SerializedProperty>>();

            foreach (var preset in Runtime.Utils.Utils.GetEnumValues<Preset>())
            {
                _groupedStaticScriptableObjects.Add(preset, new List<SerializedProperty>());
            }

            var configs = serializedObject.FindProperty("configs");
            for (int i = 0; i < configs.arraySize; i++)
            {
                var configElement = configs.GetArrayElementAtIndex(i);
                _groupedStaticScriptableObjects[
                        (Preset) configElement.FindPropertyRelative(StaticScriptableObjectConfig.SerializedPresetName)
                            .enumValueIndex]
                    .Add(configElement);
            }
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            Update();

            var foldoutIndex = 0;

            foreach (var preset in GroupedStaticScriptableObjects.Keys)
            {
                _foldouts[foldoutIndex] = EditorGUILayout.Foldout(_foldouts[foldoutIndex], preset.ToString());
                if (_foldouts[foldoutIndex++])
                {
                    EditorGUI.indentLevel++;

                    if (GroupedStaticScriptableObjects[preset].Count > 0)
                    {
                        foreach (var configElement in GroupedStaticScriptableObjects[preset])
                        {
                            var staticScriptableObjectType =
                                Type.GetType(configElement
                                    .FindPropertyRelative(StaticScriptableObjectConfig.SerializedTypeName)
                                    .stringValue);

                            var staticScriptableObject =
                                configElement.FindPropertyRelative(StaticScriptableObjectConfig
                                    .SerializedScriptableObjectName);

                            EditorGUILayout.ObjectField(staticScriptableObject, staticScriptableObjectType,
                                staticScriptableObjectType.ToString().ToGuiContent());

                            if (staticScriptableObject.objectReferenceValue != null)
                                serializedObject.ApplyModifiedProperties();
                        }
                    }
                    else
                    {
                        EditorGUILayout.HelpBox("No implemented Static Scriptable Objects found!", MessageType.Info);
                    }

                    EditorGUI.indentLevel--;
                }

                EditorGUILayout.Separator();
            }
        }
    }
}