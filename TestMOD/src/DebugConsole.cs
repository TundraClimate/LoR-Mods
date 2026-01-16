using System;
using System.Runtime.InteropServices;
using UnityEngine;

public static class DebugConsole
{
    [DllImport("kernel32.dll")]
    private static extern bool AllocConsole();

    [DllImport("kernel32.dll")]
    private static extern void SetConsoleOutputCP(int cp);

    [DllImport("kernel32.dll")]
    private static extern void SetConsoleCP(int cp);

    public static void Open()
    {
        AllocConsole();
        SetConsoleOutputCP(65001);
        SetConsoleCP(65001);
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Application.logMessageReceived += DebugConsole.OnLog;
        Console.Write(@"
██╗     ██╗██╗   ██╗███████╗    ██╗      ██████╗  ██████╗  ██████╗ ███████╗██████╗ 
██║     ██║██║   ██║██╔════╝    ██║     ██╔═══██╗██╔════╝ ██╔════╝ ██╔════╝██╔══██╗
██║     ██║██║   ██║█████╗      ██║     ██║   ██║██║  ███╗██║  ███╗█████╗  ██████╔╝
██║     ██║╚██╗ ██╔╝██╔══╝      ██║     ██║   ██║██║   ██║██║   ██║██╔══╝  ██╔══██╗
███████╗██║ ╚████╔╝ ███████╗    ███████╗╚██████╔╝╚██████╔╝╚██████╔╝███████╗██║  ██║
╚══════╝╚═╝  ╚═══╝  ╚══════╝    ╚══════╝ ╚═════╝  ╚═════╝  ╚═════╝ ╚══════╝╚═╝  ╚═╝
===================================================================================
");
    }

    private static void OnLog(string condition, string stackTrace, LogType type)
    {
        string prefix;

        switch (type)
        {
            case LogType.Error:
            case LogType.Exception:
                Console.ForegroundColor = ConsoleColor.Red;
                prefix = "[ERROR] ";
                break;
            case LogType.Warning:
                Console.ForegroundColor = ConsoleColor.Yellow;
                prefix = "[WARN] ";
                break;
            default:
                Console.ForegroundColor = ConsoleColor.Green;
                prefix = "[INFO] ";
                break;
        }

        Console.WriteLine(prefix + condition);

        if (type == LogType.Exception)
            Console.WriteLine(stackTrace);
    }
}
