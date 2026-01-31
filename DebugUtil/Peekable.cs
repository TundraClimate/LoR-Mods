using System.Collections;

public class Peekable<T>
{
    public Peekable(IEnumerable enumrable)
    {
        _inner = enumrable.GetEnumerator();
    }

    public T Peek(out bool result)
    {
        if (!this._hasPeeked)
        {
            if (!this._inner.MoveNext())
            {
                result = false;

                return default(T);
            }

            this._peeked = (T)this._inner.Current;
            this._hasPeeked = true;
        }

        result = true;

        return this._peeked;
    }

    public T Peek()
    {
        bool _;

        return Peek(out _);
    }

    public T MoveNext(out bool result)
    {
        if (this._hasPeeked)
        {
            result = true;
            this._hasPeeked = false;

            return this._peeked;
        }

        if (this._inner.MoveNext())
        {
            result = true;

            return (T)this._inner.Current;
        }

        result = false;

        return default(T);
    }

    public T MoveNext()
    {
        bool _;

        return MoveNext(out _);
    }

    private IEnumerator _inner;

    private bool _hasPeeked;

    private T _peeked;
}
