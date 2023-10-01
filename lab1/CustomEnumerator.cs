using System.Collections;
using System.Collections.Generic;

namespace lab1;

public class CustomEnumerator<T> : IEnumerator<T>
{
    private readonly IList<T> _list;
    private int _cursor;
    private T _current;

    public T Current => _current;
    object IEnumerator.Current => _current!;

    public CustomEnumerator(IList<T> list)
    {
        _list = list;
        _cursor = -1; // Initialize cursor to -1 to indicate that it hasn't started iterating yet
        _current = default!;
    }

    public bool MoveNext()
    {
        if (!HasNext())
        {
            return false;
        }

        _cursor++;
        _current = _list[_cursor];
        return true;
    }

    private bool HasNext()
    {
        return _cursor < _list.Count - 1;
    }

    public void Reset()
    {
        _cursor = -1; // Reset cursor to -1 to start from the beginning
        _current = default!;
    }

    public void Dispose()
    {
    }
}