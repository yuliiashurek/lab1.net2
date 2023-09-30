using System;
using System.Collections;
using System.Collections.Generic;

namespace lab1;

public class MyNewList<T> : IList<T>
{
    private int _size = 4;
    public int Count  => _size;
    public bool IsReadOnly => false;
    private T[] _items = new T[4];
    private int _capacity;
    private const int DefaultCapacity = 4;
    


    public MyNewList(int capacity)
    {
        if (capacity < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(capacity));
        }
        //==, !, ? можна перевантажити
        //is не можна перевантажити і тому буде завжди використовуватися стандартний компаратор
        //is лише з константами
        _size = 0;
        _capacity = capacity;
        _items = capacity is 0 ? Array.Empty<T>() : new T[capacity];
    }
    
    //avoids boxing
    public IEnumerator<T> GetEnumerator()
    {
        return new MyEnumerator(this);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Add(T item)
    {
        //with length will be slower, worse performance
        if (_size >= _capacity)
        {
            Resize();
        }
        _items[_size] = item;
        _size++;
    }
    
    private void Resize()
    {
        var newCapacity = _capacity * 2;
        var tempArray = new T [newCapacity];
        //substitution of references
        Array.Copy(_items, tempArray, _size);
        _items = tempArray;
        _capacity = newCapacity;
    }

    public void Clear()
    {
        _items = new T[DefaultCapacity];
        _capacity = _size = 0;
    }

    public bool Contains(T item)
    {
        for (int i = 0; i < _size; i++)
        {
            var element = _items[i];
            //== need comparator/comparable
            if (element?.Equals(item) == true)
            {
                return true;
            }
        }

        return false;
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        if (array.Length - arrayIndex < _items.Length)
        {
            throw new ArgumentException("Dest array is too small");
        }
        Array.Copy(_items, 0, array, arrayIndex, _items.Length);
    }

    public bool Remove(T item)
    {
        var index = Array.IndexOf(_items, item);
        var isRemoved = index != -1;
        RemoveAt(index);
        return isRemoved;
    }


    public int IndexOf(T item)
    {
        return Array.IndexOf(_items, item);
    }

    public void Insert(int index, T item)
    {
        if (_size == index)
        {
            _items[index] = item;
        }

        if (_size == _capacity)
        {
            Resize();
        }
        
        if (_size < index)
        {
            throw new InvalidOperationException("Invalid index");
        }
        
        //index =0
        _size++;
        Array.Copy(_items, index, _items, index+1, _items.Length - index);
        _items[index] = item;
    }

    public void RemoveAt(int index)
    {
        if (index < 0 || index > _size)
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }
        _size--;
        Array.Copy(_items, index+1, _items, index, _size - index);
    }

    public T this[int index]
    {
        get => _items[index];
        set
        {
            if (index >= _size)
            {
                throw new ArgumentNullException("Invalid index");
            }

            _items[index] = value;
        }
    }

    private class MyEnumerator : IEnumerator<T>
    {
        private readonly IList<T> _list;
        private int _cursor;
        private T _current;

        public T Current => _current;
        object IEnumerator.Current => _current;

        public MyEnumerator(IList<T> list)
        {
            _list = list;
            _cursor = -1; // Initialize cursor to -1 to indicate that it hasn't started iterating yet
            _current = default;
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
            _current = default;
        }

        public void Dispose()
        {
            // Dispose implementation here
        }
    }

}