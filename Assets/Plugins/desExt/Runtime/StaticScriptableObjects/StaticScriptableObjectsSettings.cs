using System;
using System.Collections.Generic;
using System.Linq;
using desExt.Runtime.Presets;
using desExt.Runtime.Utils;
using UnityEngine;

namespace desExt.Runtime.StaticScriptableObjects
{
    public class StaticScriptableObjectsSettings : LoadedScriptableObjectSingleton<StaticScriptableObjectsSettings>
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("desExt/Static Scriptable Objects Manager")]
        public static void MenuItem()
        {
            EditorUtils.SetInspectorObject(Instance);
        }
#endif

#pragma warning disable 0649
        [SerializeField]
        private List<StaticScriptableObjectConfig> configs;
#pragma warning restore 0649

        private static Dictionary<Type, BaseStaticScriptableObject> _currentSetup;

        private static List<Type> StaticScriptableImplementationsTypes
        {
            get
            {
                var types = new List<Type>();

                foreach (var assembly in System.AppDomain.CurrentDomain.GetAssemblies())
                {
                    types.AddRange(assembly.GetTypes().Where(t => t.IsSubclassOf(typeof(BaseStaticScriptableObject)) &&
                                                                  t != typeof(StaticScriptableObject<>)));
                }

                return types;
            }
        }

        private static Dictionary<Type, BaseStaticScriptableObject> CurrentSetup
        {
            get
            {
                if (_currentSetup == null)
                {
                    RefreshCurrentSetup();
                }

                return _currentSetup;
            }
        }

        private static void RefreshCurrentSetup()
        {
            _currentSetup = new Dictionary<Type, BaseStaticScriptableObject>();
            foreach (var scriptableImplementationType in StaticScriptableImplementationsTypes)
            {
                var staticScriptableObjectConfig = Instance
                    .configs
                    .Find(i => i.SerializedType == scriptableImplementationType &&
                               i.Preset == PresetManager.ActivePreset);

                var concreteImplementation = staticScriptableObjectConfig.ScriptableObject;

                if (concreteImplementation != null)
                {
                    _currentSetup.Add(scriptableImplementationType, concreteImplementation);
                }
                else
                {
                    Logging.LogError(
                        $"Implementation for {scriptableImplementationType.Name.Color(Color.blue)} is not set in Static Scriptable Objects Manager!");
                }
            }
        }

        public static T GetStaticScriptableObjectInstance<T>() where T : BaseStaticScriptableObject
        {
            return (T) CurrentSetup[typeof(T)];
        }

        private List<StaticScriptableObjectConfig> GetConfigCopy()
        {
            return new List<StaticScriptableObjectConfig>(configs);
        }

#if UNITY_EDITOR
        [UnityEditor.Callbacks.DidReloadScripts]
        private static void OnScriptsReloaded()
        {
            Instance.OnValidate();
        }
#endif
        private void OnValidate()
        {
            // Add placeholders for non-implemented scriptable objects
            foreach (var type in StaticScriptableImplementationsTypes)
            {
                if (configs.Find(c => c.SerializedType == type) == null)
                {
                    foreach (var preset in Utils.Utils.GetEnumValues<Preset>())
                    {
                        configs.Add(new StaticScriptableObjectConfig(type, preset));
                    }
                }
            }

            // Fix non-existing types
            foreach (var config in GetConfigCopy())
            {
                if (config.SerializedType == null)
                {
                    // Logging.LogError($"No script of type {config.Type} found!");

                    // Remove config entry if object is empty
                    if (config.ScriptableObject == null)
                    {
                        configs.Remove(config);
                    }
                    // If the object is there then change the serialized type
                    else
                    {
                        config.SerializedType = config.GetScriptableObjectType();
                    }
                }
            }

            // Fill missing preset implementations
            foreach (var config in GetConfigCopy())
            {
                foreach (var preset in Utils.Utils.GetEnumValues<Preset>())
                {
                    var foundConfigsPerPreset = configs.FindAll(t =>
                        t.SerializedType == config.SerializedType &&
                        t.Preset == preset);

                    // Remove preset duplicates
                    if (foundConfigsPerPreset.Count > 1)
                    {
                        Logging.LogError(
                            $"Found {foundConfigsPerPreset.Count.ToString().Color(Color.blue)} {preset.ToString().Color(Color.blue)} implementations of {config.SerializedType.ToString().Color(Color.blue)} static scriptable object!");

                        configs.Remove(foundConfigsPerPreset.Find(c =>
                            c.ScriptableObject == null));
                    }

                    if (foundConfigsPerPreset.FirstOrDefault() == null)
                    {
                        Logging.LogError(
                            $"Cannot find the {preset.ToString().Color(Color.blue)} preset implementation for static scriptable object {config.SerializedType.ToString().Color(Color.blue)}!");

                        Instance.configs.Add(new StaticScriptableObjectConfig(config.SerializedType,
                            preset));
                    }
                }
            }
        }
    }
}