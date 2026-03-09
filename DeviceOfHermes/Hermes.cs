using UnityEngine;

namespace System;

public static class Hermes
{
    public static void Say(string? message, MessageLevel lvl = MessageLevel.Info)
    {
        switch (lvl)
        {
            case MessageLevel.Info:
                Debug.Log(message);

                break;
            case MessageLevel.Warn:
                Debug.LogWarning(message);

                break;
            case MessageLevel.Error:
                Debug.LogError(message);

                break;
        }
    }
}

public enum MessageLevel
{
    Info,
    Warn,
    Error,
}
