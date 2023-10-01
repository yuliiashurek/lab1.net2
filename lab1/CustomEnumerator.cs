﻿using System.Collections;
using System.Collections.Generic;

namespace lab1;

public class CustomEnumerator<T> : IEnumerator<T>
{
    
    public T Current => _current;
    object IEnumerator.Current => _current!;
    
    private readonly IList<T> _list;
    private int _cursor;
    private T _current;

    public CustomEnumerator(IList<T> list)
    {
        _list = list;
        _cursor = -1;
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

    public void Reset()
    {
        _cursor = -1;
        _current = default!;
    }

    public void Dispose()
    {
    }
    
    private bool HasNext()
    {
        return _cursor < _list.Count - 1;
    }
}