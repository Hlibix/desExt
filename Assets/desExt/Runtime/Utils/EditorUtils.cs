using UnityEditor;
using UnityEngine;

namespace desExt.Runtime.Utils
{
#if UNITY_EDITOR
    public static class EditorUtils
    {
        public static void SetInspectorObject(Object @object)
        {
            Selection.activeObject = @object;
            EditorApplication.ExecuteMenuItem("Window/General/Inspector");
        }
    }

#endif
}