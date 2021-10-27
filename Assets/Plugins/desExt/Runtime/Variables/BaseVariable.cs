using UnityEngine;

namespace desExt.Runtime.Variables
{
    public class BaseVariable : ScriptableObject
    {
        public const string SerializedVariableCategoryName = nameof(variableCategory);

        [SerializeField]
        private VariableCategory variableCategory;

        public VariableCategory VariableCategory => variableCategory;
    }

    public class BaseTypeVariable<T> : BaseVariable
    {
        public T Value;
    }
}