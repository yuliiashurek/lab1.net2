using System;
using System.Collections.Generic;

namespace CustomListClassLibrary;

using System.Collections;

public class CustomList<T> : IList<T>
{
    public int Count  => _size;
    public bool IsReadOnly => false;

    #region private fields

    private int _size;
    private T[] _items;
    internal int _capacity;
    private const int DefaultCapacity = 4;

    #endregion

    #region implementation of IList<T>
    public CustomList(int capacity = 0)
    {
        if (capacity < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(capacity), "Capacity cannot be a negative value.");
        }
        _size = 0;
        _capacity = capacity;
        _items = capacity is 0 ? Array.Empty<T>() : new T[capacity];
    }
    
    public IEnumerator<T> GetEnumerator()
    {
        return new CustomEnumerator<T>(this);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Add(T item)
    {
        if (_size >= _capacity)
        {
            Resize();
        }
        _items[_size] = item;
        _size++;
        OnItemAdded(item, _size - 1);
    }

    public void Clear()
    {
        _items = new T[DefaultCapacity];
        _capacity = _size = 0;
        OnListCleared();
    }

    public bool Contains(T item)
    {
        for (int i = 0; i < _size; i++)
        {
            var element = _items[i];
            if (element?.Equals(item) == true)
            {
                return true;
            }
        }

        return false;
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        if (array is null)
        {
            throw new ArgumentNullException(nameof(array), "Array cannot be null.");
        }
        
        if (arrayIndex < 0 || arrayIndex >= array.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(arrayIndex), "Invalid array index");
        }

        if (array.Length - arrayIndex < _size)
        {
            throw new ArgumentException("Number of elements to copy cannot be placed into the destination array.");
        }

        Array.ConstrainedCopy(_items, 0, array, arrayIndex, _size);
    }
    
    public bool Remove(T item)
    {
        var index = Array.IndexOf(_items, item);
        if (index == -1)
        {
            throw new InvalidOperationException("The item does not exist in the list.");
        }
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
        if (index < 0 || index > _size)
        {
            throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range. It must be within the current list size.");
        }
        if (_size == _capacity)
        {
            Resize();
        }
        Array.Copy(_items, index, _items, index + 1, _size - index);
        _items[index] = item;
        _size++;
        OnItemAdded(item, index);
    }

    public void RemoveAt(int index)
    {
        CheckIndex(index);
        Array.Copy(_items, index + 1, _items, index, _size - index - 1);
        _size--;
        OnItemRemoved(this[index], index);
    }
    
    public T this[int index]
    {
        get => _items[index];
        set
        {
            CheckIndex(index);
            _items[index] = value;
        }
    }
    
    #endregion

    #region work with events

    public EventHandler<CustomListItemEventArgs<T>> ItemAdded;

    public EventHandler<CustomListItemEventArgs<T>> ItemRemoved;

    public EventHandler<CustomListBaseEventArgs> ListCleared;

    public EventHandler<CustomListEventArgs> ListResized;

    private void OnItemAdded(T item, int index)
    {
        if (ItemAdded != null)
        {
            ItemAdded(this, new CustomListItemEventArgs<T>(item, index, ModificationTypes.ItemAdded));
        }
    }

    private void OnItemRemoved(T item, int index)
    {
        if (ItemRemoved != null)
        {
            ItemRemoved(this, new CustomListItemEventArgs<T>(item, index, ModificationTypes.ItemRemoved));
        }
    }

    private void OnListCleared()
    {
        if (ListCleared != null)
        {
            ListCleared(this, new CustomListBaseEventArgs(ModificationTypes.ListCleared));
        }
    }

    private void OnListResized(int oldCapacity)
    {
        if (ListResized != null)
        {
            ListResized(this, new CustomListEventArgs(oldCapacity, _capacity));
        }
    }
    #endregion

    #region private methods
    private void Resize()
    {
        var oldCapacity = _capacity;
        var newCapacity = _capacity <= 0 ? DefaultCapacity : _capacity * 2;
        var tempArray = new T [newCapacity];
        Array.Copy(_items, tempArray, _size);
        _items = tempArray;
        _capacity = newCapacity;
        OnListResized(oldCapacity);
    }
    private void CheckIndex(int index)
    {
        if (index < 0 || index >= _size)
        {
            throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range. It must be within the current list size.");
        }
    }
    
    #endregion
}