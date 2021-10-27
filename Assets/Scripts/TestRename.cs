using desExt.Runtime.StaticScriptableObjects;
using UnityEngine;

[CreateAssetMenu]
public class TestRename : StaticScriptableObject<TestRename>
{
    public string testString = "Static Test";
}

// [CreateAssetMenu]
// public class TestStaticSo2 : StaticScriptableObject<TestStaticSo2>
// {
//     public float testFloat = 5f;
// }
//
// [CreateAssetMenu]
// public class TestStaticSo3 : StaticScriptableObject<TestStaticSo3>
// // public class TestStaticSo5 : StaticScriptableObject<TestStaticSo5>
// {
//     public float testFloat = 5f;
// }