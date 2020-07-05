using desExt.Runtime.Utils;
using UnityEditor;
using UnityEngine;

namespace desExt.Runtime.Presets
{
    public class PresetManager : LoadedScriptableObjectSingleton<PresetManager>
    {
#if UNITY_EDITOR
        [MenuItem("desExt/Preset Manager")]
        public static void MenuItem()
        {
            EditorUtils.SetInspectorObject(Instance);
        }
#endif

        public const string SerializedActivePresetName = nameof(activePreset);

        [SerializeField]
        private Preset activePreset = Preset.Debug;

        public static Preset ActivePreset => Instance.activePreset;
    }
}