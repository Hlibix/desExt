using System;
using desExt.Runtime.Utils;
using desExt.Runtime.Variables;
using UnityEngine;

namespace desExt.Runtime.References
{
    [Serializable]
    public abstract class SimpleReference<TValue, TSerializedClass> : BaseReference
        where TSerializedClass : BaseTypeVariable<TValue>
    {
#pragma warning disable 0649
        [SerializeField]
        private TSerializedClass variableReference;

        [SerializeField]
        private TValue constantValue;

        [SerializeField]
        private bool useConstantValue;
#pragma warning restore 0649

        public TValue Value
        {
            get
            {
                if (useConstantValue)
                {
                    return constantValue;
                }

                if (!useConstantValue && variableReference == null)
                {
                    Logging.LogWarning(
                        $"Reference value of {GetType().Name.Color(Color.blue)} is null! Forcing constant value...");
                    return constantValue;
                }

                return variableReference.Value;
            }
        }

        public void SetConstantValue(TValue value)
        {
            constantValue = value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public static implicit operator TValue(SimpleReference<TValue, TSerializedClass> reference)
        {
            return reference.Value;
        }
    }


    [Serializable]
    public class BaseReference
    {
    }
}