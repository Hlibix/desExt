using System.Collections.Generic;
using desExt.Runtime.Variables;
using TestSerializedClasses;
using UnityEngine;

namespace TestVariables
{
    [CreateAssetMenu]
    public class UltimateTestVariable : BaseVariable
    {
        public AnimationCurve AnimationCurve;
        public int Int;
        public string String;
        public bool[] Bools;
        public List<float> Floats;

        public FloatVariable[] FloatVariables;
        // public List<StringVariable> StringVariables;

        public UltimateTestSerializedClass TestSerializedClass;
        public List<UltimateTestSerializedClass> TestSerializedClassList;
        public UltimateTestSerializedClass[] TestSerializedClassArray;
    }
}