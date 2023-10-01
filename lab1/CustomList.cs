﻿using System;
using System.Collections.Generic;

namespace lab1;

using System.Collections;

public class CustomList<T> : IList<T>
{
    public int Count  => _size;
    public bool IsReadOnly => false;
    
    private int _size;
    private T[] _items;
    private int _capacity;
    private const int DefaultCapacity = 4;
    
    public CustomList(int capacity = 0)
    {
        if (capacity < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(capacity), "Capacity cannot be a negative value.");
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
        return new CustomEnumerator<T>(this);
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
        OnItemAdded(item, _size - 1);
    }
    
    private void Resize()
    {
        var oldCapacity = _capacity;
        var newCapacity = _capacity <= 0 ? DefaultCapacity : _capacity * 2;
        var tempArray = new T [newCapacity];
        //substitution of references
        Array.Copy(_items, tempArray, _size);
        _items = tempArray;
        _capacity = newCapacity;
        OnListResized(oldCapacity);
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
            //== needs comparator/comparable, equals doesn't
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

        // Shift elements to make room for the new item
        Array.Copy(_items, index, _items, index + 1, _size - index);

        // Insert the new item at the specified index
        _items[index] = item;
        _size++;
        OnItemAdded(item, index);
    }

    private void CheckIndex(int index)
    {
        if (index < 0 || index >= _size)
        {
            throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range. It must be within the current list size.");
        }
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
    
    
    public EventHandler<CustomListItemEventArgs<T>>? ItemAdded;

    public EventHandler<CustomListItemEventArgs<T>>? ItemRemoved;

    public EventHandler<CustomListBaseEventArgs>? ListCleared;

    public EventHandler<CustomListEventArgs>? ListResized;

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

}