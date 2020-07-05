using System;
using desExt.Runtime.Presets;
using UnityEngine;

namespace desExt.Runtime.StaticScriptableObjects
{
    [Serializable]
    public class StaticScriptableObjectConfig
    {
        public static readonly string SerializedTypeName = nameof(type);
        public static readonly string SerializedPresetName = nameof(preset);
        public static readonly string SerializedScriptableObjectName = nameof(scriptableObject);

        [SerializeField]
        private string type;

        [SerializeField]
        private Preset preset;

#pragma warning disable 0649
        [SerializeField]
        private BaseStaticScriptableObject scriptableObject;
#pragma warning restore 0649

        public BaseStaticScriptableObject ScriptableObject => scriptableObject;
        public Preset Preset => preset;

        public Type SerializedType
        {
            get => Type.GetType(type);
            set => type = value.AssemblyQualifiedName;
        }

        public Type GetScriptableObjectType()
        {
            return scriptableObject.GetType();
        }

        public StaticScriptableObjectConfig(Type type)
        {
            this.type = type.AssemblyQualifiedName;
        }

        public StaticScriptableObjectConfig(Type type, Preset preset)
        {
            this.type = type.AssemblyQualifiedName;
            this.preset = preset;
        }
    }
}