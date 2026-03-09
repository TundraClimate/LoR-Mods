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

    public static void Store<T>(T data)
    {
        DataStorage<T>.Data = data;
    }

    public static T Load<T>()
    {
        return DataStorage<T>.Data;
    }

    public static bool TryLoad<T>(out T? data)
    {
        data = DataStorage<T>._data;

        return data is not null;
    }
}

public enum MessageLevel
{
    Info,
    Warn,
    Error,
}

internal static class DataStorage<T>
{
    internal static T? _data;

    public static T Data
    {
        get => _data ?? throw new InvalidOperationException($"Data of {nameof(T)} must was initialized");
        set => _data = value;
    }
}

public static class Extension
{
    public static int Min(this int n1, int n2)
    {
        return Math.Min(n1, n2);
    }

    public static int Max(this int n1, int n2)
    {
        return Math.Max(n1, n2);
    }

    public static string? StripPrefix(this string original, string strip)
    {
        if (original.StartsWith(strip))
        {
            return original.Substring(strip.Length);
        }

        return null;
    }

    public static string? StripSuffix(this string original, string strip)
    {
        if (original.EndsWith(strip))
        {
            return original.Substring(0, (original.Length - strip.Length).Max(0));
        }

        return null;
    }

    public static IEnumerable<V> Map<T, V>(this IEnumerable<T> enumerable, Func<T, V> pred)
    {
        return enumerable.Select(pred);
    }

    public static IEnumerable<T> Filter<T>(this IEnumerable<T> enumerable, Func<T, bool> pred)
    {
        return enumerable.Where(pred);
    }

    public static IEnumerable<V> FilterMap<T, V>(this IEnumerable<T> enumerable, Func<T, V> pred)
    {
        return enumerable.Select(val => pred(val)).Where(val => val is not null);
    }

    public static IEnumerable<T> Flatten<T>(this IEnumerable<IEnumerable<T>> enumerable)
    {
        return enumerable.SelectMany(val => val);
    }

    public static IEnumerable<(int, T)> Enumerate<T>(this IEnumerable<T> enumerable)
    {
        return enumerable.Select((val, idx) => (idx, val));
    }

    public static List<T> Collect<T>(this IEnumerable<T> enumerable)
    {
        return enumerable.ToList();
    }

    public static R Fold<T, R>(this IEnumerable<T> enumerable, R root, Func<R, T, R> acc)
    {
        var res = root;

        foreach (var elem in enumerable)
        {
            res = acc(res, elem);
        }

        return res;
    }

    public static T? Reduce<T>(this IEnumerable<T> enumerable, Func<T, T, T> acc)
    {
        T? res = default(T);

        foreach (var elem in enumerable)
        {
            if (res is null)
            {
                res = elem;

                continue;
            }

            res = acc(res, elem);
        }

        return res;
    }
}
