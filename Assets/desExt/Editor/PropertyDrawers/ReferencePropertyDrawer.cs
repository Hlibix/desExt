using desExt.Editor.Utils;
using desExt.Runtime.References;
using UnityEditor;
using UnityEngine;

namespace desExt.Editor.PropertyDrawers
{
    [CustomPropertyDrawer(typeof(SimpleReference<,>), true)]
    public class ReferencePropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            property.serializedObject.Update();
            EditorGUI.BeginProperty(position, label, property);

            var useConstantValue = property.FindPropertyRelative("useConstantValue");

            useConstantValue.boolValue =
                EditorGUI.Toggle(position.MoveRight(position.width * 0.3f).SetWidth(15f), useConstantValue.boolValue);

            if (useConstantValue.boolValue)
            {
                EditorGUI.PropertyField(position, property.FindPropertyRelative("constantValue"),
                    property.displayName.ToGuiContent());
            }
            else
            {
                EditorGUI.PropertyField(position, property.FindPropertyRelative("variableReference"),
                    property.displayName.ToGuiContent());
            }

            EditorGUI.EndProperty();
            property.serializedObject.ApplyModifiedProperties();
        }
    }
}