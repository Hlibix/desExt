namespace desExt.Runtime
{
    public static class ConfigLocations
    {
        public const string ConfigFolderDatabasePath = "Assets/" + ConfigFolderDataPath;
        public const string ConfigFolderDataPath = "Resources/" + ConfigFolderResourcePath;
        public const string ConfigFolderResourcePath = "desExt/Config/";

        public static string GetAssetResourcePath(string assetName)
        {
            return ConfigFolderResourcePath + assetName;
        }

        public static string GetAssetDatabasePath(string assetName)
        {
            return ConfigFolderDatabasePath + assetName + ".asset";
        }
    }
}