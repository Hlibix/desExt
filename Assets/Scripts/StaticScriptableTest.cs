using desExt.Runtime.References;
using desExt.Runtime.Variables;
using UnityEngine;
using UnityEngine.UI;

public class StaticScriptableTest : MonoBehaviour
{
    public Text t1;
    public Text t2;

    // [SerializeField]
    // private SimpleReference<float, float> _type;

    [SerializeField]
    private SerializeReference test;


    private void Awake()
    {
        t1.text = "Should be 5";
        t2.text = TestRename.Instance.testString;
    }
}