using System;
using UnityEngine;

public static class Logger
{
    public static void Info(string stmt, params object[] inject)
    {
        Debug.Log(Fmt.Format(stmt, inject));
    }

    public static void Warn(string stmt, params object[] inject)
    {
        Debug.LogWarning(Fmt.Format(stmt, inject));
    }

    public static void Error(string stmt, params object[] inject)
    {
        Debug.LogError(Fmt.Format(stmt, inject));
    }
}
