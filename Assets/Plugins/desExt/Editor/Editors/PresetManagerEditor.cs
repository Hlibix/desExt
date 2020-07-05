using desExt.Runtime.Presets;
using UnityEditor;

namespace desExt.Editor.Editors
{
    [CustomEditor(typeof(PresetManager))]
    public class PresetManagerEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty(PresetManager.SerializedActivePresetName));
        }
    }
}