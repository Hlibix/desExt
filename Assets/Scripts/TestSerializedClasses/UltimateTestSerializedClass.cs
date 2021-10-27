using System;
using System.Collections.Generic;

namespace TestSerializedClasses
{
    [Serializable]
    public class UltimateTestSerializedClass
    {
        public PlaceholderSubClass Class;
        public PlaceholderSubClass[] Classes;
        public List<PlaceholderSubClass> Classes2;
    }

    [Serializable]
    public class PlaceholderSubClass
    {
        public int T1;
        public int[] T2;
    }
}