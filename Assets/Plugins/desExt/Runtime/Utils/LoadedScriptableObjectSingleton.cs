using System.Linq;
using UnityEngine;

namespace desExt.Runtime.Utils
{
    public class LoadedScriptableObjectSingleton<T> : ScriptableObject where T : LoadedScriptableObjectSingleton<T>
    {
        private static T _instance;

        protected static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    var instances = Resources.LoadAll<T>(ConfigLocations.ConfigFolderResourcePath);

                    if (instances.Length > 1)
                    {
                        Logging.LogError($"Found more than 1 instance of {typeof(T).ToString().Color(Color.blue)}!");
                    }

                    _instance = instances.FirstOrDefault();

                    if (_instance == null)
                    {
#if UNITY_EDITOR
                        _instance = (T) CreateInstance(typeof(T));

                        _instance.name = typeof(T).Name;

                        var absoluteFileDirectory = System.IO.Path.Combine(Application.dataPath,
                            ConfigLocations.ConfigFolderDataPath);

                        if (!System.IO.Directory.Exists(absoluteFileDirectory))
                        {
                            System.IO.Directory.CreateDirectory(absoluteFileDirectory);
                        }

                        UnityEditor.AssetDatabase.CreateAsset(_instance,
                            ConfigLocations.GetAssetDatabasePath(_instance.name));
#endif
                    }
                }


                return _instance;
            }
        }
    }
}