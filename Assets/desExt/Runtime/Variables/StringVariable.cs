using UnityEngine;

namespace desExt.Variables
{
    [CreateAssetMenu(menuName = DesExtNames.VariablesMenuName + "String Variable")]

    public class StringVariable: BaseVariable
    {
        public string StringValue;
    }
}