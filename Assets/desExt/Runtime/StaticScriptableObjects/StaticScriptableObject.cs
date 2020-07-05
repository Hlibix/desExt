using desExt.Runtime.Utils;
using UnityEngine;

namespace desExt.Runtime.StaticScriptableObjects
{
    public class BaseStaticScriptableObject : ScriptableObject
    {
    }

    public class StaticScriptableObject<T> : BaseStaticScriptableObject where T : BaseStaticScriptableObject
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = StaticScriptableObjectsSettings.GetStaticScriptableObjectInstance<T>();

                    if (_instance == null)
                    {
                        Logging.LogError($"Cannot find an asset instance of {typeof(T).Name.Color(Color.blue)}");
                    }
                }

                return StaticScriptableObjectsSettings.GetStaticScriptableObjectInstance<T>();
            }
        }
    }
}