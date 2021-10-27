using UnityEngine;

public static partial class Utilities
{
    /// <summary>
    /// Logs given object to the console and then returns it. 
    /// </summary>
    /// <param name="logType">Specifies which Log message to use.</param>
    public static T Log<T>(this T objectToLog, LogType logType = LogType.Log)
    {
        var message = objectToLog.ToString();
        switch (logType)
        {
            case LogType.Log:
                Debug.Log(message);
                break;
            case LogType.Warning:
                Debug.LogWarning(message);
                break;
            case LogType.Error:
                Debug.LogError(message);
                break;
        }

        return objectToLog;
    }

    public enum LogType
    {
        Log,
        Warning,
        Error
    }
}