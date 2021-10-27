namespace desExt.Editor.Model
{
    public class VariableData
    {
        public string VariablePath;
        public bool FoldOut;

        public VariableData(string variablePath, bool foldOut = false)
        {
            VariablePath = variablePath;
            FoldOut = foldOut;
        }
    }
}