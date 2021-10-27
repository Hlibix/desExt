using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace desExt.Editor.Utils
{
    public static class EnumUtils
    {
        public static void GenerateEnum(string enumName, IEnumerable<string> enumValues,
            string relativeScriptFileLocation,
            string namespaceName = "")
        {
            if (!enumValues.Any())
                throw new Exception("No Enum Values passed, aborting");

            string filePathAndName = "Assets/" + relativeScriptFileLocation + "/" + enumName + ".cs";

            using (StreamWriter streamWriter = new StreamWriter(filePathAndName))
            {
                try
                {
                    if (namespaceName != "")
                    {
                        streamWriter.WriteLine("namespace " + namespaceName + "\n{");
                    }

                    streamWriter.WriteLine("public enum " + enumName);
                    streamWriter.WriteLine("{");

                    for (int i = 0; i < enumValues.Count(); i++)
                    {
                        streamWriter.WriteLine("\t" + enumValues.ElementAt(i) + ",");
                    }

                    streamWriter.WriteLine("}");

                    if (namespaceName != "")
                    {
                        streamWriter.WriteLine("}");
                    }

                    AssetDatabase.Refresh();
                    AssetDatabase.ImportAsset(filePathAndName);
                }
                catch (Exception e)
                {
                    Debug.LogWarning(e);
                }
            }
        }

        public static void GenerateScript(string contains, string relativeScriptFileLocation)
        {
            string filePathAndName = "D:\\nord\\unity_projects\\desExt\\Assets\\" + relativeScriptFileLocation + ".cs";

            using (StreamWriter streamWriter = new StreamWriter(filePathAndName, false))
            {
                try
                {
                    streamWriter.WriteLine(contains);

                    AssetDatabase.Refresh();
                    AssetDatabase.ImportAsset(filePathAndName);
                }
                catch (Exception e)
                {
                    Debug.LogWarning(e);
                }
            }
        }
    }
}