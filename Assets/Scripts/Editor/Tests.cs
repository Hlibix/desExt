using System;
using System.Linq;
using System.Threading;
using desExt.Editor.Utils;
using UnityEditor;
using UnityEngine;

public class TestsWindow : EditorWindow
{
    [MenuItem("Tools/Tests")]
    public static void Init()
    {
        GetWindow(typeof(TestsWindow)).Show();
    }

    static string lineTypes =
        "Vector2, Vector3, Vector4, Matrix4x4, Color, Rect, LayerMask, GameObject, Texture2D, AnimationClip, int, string, float, bool";

    static string[] types = lineTypes.Split(new[] {", "}, StringSplitOptions.RemoveEmptyEntries);

    private void OnGUI()
    {
        if (GUILayout.Button("Test1"))
        {
            foreach (var type in types)
            {
                // EditorApplication.ExecuteMenuItem()
            }
        }

        if (GUILayout.Button("Test2"))
        {
            Debug.Log(types.Aggregate((t, y) => t + "\n" + y));

            foreach (var type in types)
            {
                var name = Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(type);
                EnumUtils.GenerateScript(
                    $"using desExt.Variables;using UnityEngine;namespace desExt.Runtime.Variables{{[CreateAssetMenu(menuName = DesExtNames.VariablesMenuName + \"{name} Variable\")]public class {name}Variable : BaseTypeVariable<{type}>{{    }}}}",
                    $"desExt/Runtime/Variables/{name}Variable");
            }
        }

        if (GUILayout.Button("Test3"))
        {
            Debug.Log(types.Aggregate((t, y) => t + "\n" + y));

            foreach (var type in types)
            {
                var name = Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(type);
                EnumUtils.GenerateScript(
                    $"using System;using desExt.Runtime.Variables;using UnityEngine;namespace desExt.Runtime.References{{    [Serializable]    public class {name}Reference : SimpleReference<{type}, {name}Variable>    {{    }}}}",
                    $"desExt/Runtime/References/{name}Reference");
            }
        }
    }
}