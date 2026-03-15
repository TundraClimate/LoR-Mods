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

    public static Faction FaceTo(this Faction faction)
    {
        return faction switch
        {
            Faction.Enemy => Faction.Player,
            _ => Faction.Enemy,
        };
    }
}

public static class Mem
{
    public static void Swap<T>(ref T a, ref T b)
    {
        (a, b) = (b, a);
    }

    public static T? Take<T>(ref T? self)
        where T : class
    {
        return Mem.Replace(ref self, default(T));
    }

    public static object? Take(ref object? self)
    {
        return Mem.Replace(ref self, null);
    }

    public static T? Replace<T>(ref T? self, T? newValue)
        where T : class
    {
        return Interlocked.Exchange(ref self, newValue);
    }

    public static int Replace(ref int self, int newValue)
    {
        return Interlocked.Exchange(ref self, newValue);
    }

    public static float Replace(ref float self, float newValue)
    {
        return Interlocked.Exchange(ref self, newValue);
    }

    public static long Replace(ref long self, long newValue)
    {
        return Interlocked.Exchange(ref self, newValue);
    }

    public static double Replace(ref double self, double newValue)
    {
        return Interlocked.Exchange(ref self, newValue);
    }

    public static IntPtr Replace(ref IntPtr self, IntPtr newValue)
    {
        return Interlocked.Exchange(ref self, newValue);
    }

    public static object? Replace(ref object? self, object? newValue)
    {
        return Interlocked.Exchange(ref self, newValue);
    }

    public static T? ReplaceIf<T>(ref T? self, T? newValue, T? expected)
        where T : class
    {
        return Interlocked.CompareExchange(ref self, newValue, expected);
    }

    public static int ReplaceIf(ref int self, int newValue, int expected)
    {
        return Interlocked.CompareExchange(ref self, newValue, expected);
    }

    public static float ReplaceIf(ref float self, float newValue, float expected)
    {
        return Interlocked.CompareExchange(ref self, newValue, expected);
    }

    public static long ReplaceIf(ref long self, long newValue, long expected)
    {
        return Interlocked.CompareExchange(ref self, newValue, expected);
    }

    public static double ReplaceIf(ref double self, double newValue, double expected)
    {
        return Interlocked.CompareExchange(ref self, newValue, expected);
    }

    public static IntPtr ReplaceIf(ref IntPtr self, IntPtr newValue, IntPtr expected)
    {
        return Interlocked.CompareExchange(ref self, newValue, expected);
    }

    public static object? ReplaceIf(ref object? self, object? newValue, object? expected)
    {
        return Interlocked.CompareExchange(ref self, newValue, expected);
    }
}
