using UnityEngine;

namespace desExt.Runtime.Variables
{
    public class BaseVariable : ScriptableObject
    {
        [SerializeField]
        private VariableCategory VariableCategory;
    }

    public class BaseTypeVariable<T> : BaseVariable
    {
        public T Value;
    }
}