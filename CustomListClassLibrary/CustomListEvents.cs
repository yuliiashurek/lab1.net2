using System;

namespace CustomListClassLibrary;

public enum ModificationTypes
{
    ItemAdded,
    ItemRemoved,
    ListCleared, 
    ListResized
}

public class CustomListBaseEventArgs : EventArgs
{
    public ModificationTypes ModificationTypes { get; }
    public DateTime DateTime { get; }
    
    public CustomListBaseEventArgs(ModificationTypes modificationTypes)
    {
        ModificationTypes = modificationTypes;
        DateTime = DateTime.Now;
    }
}

public class CustomListItemEventArgs<T> : CustomListBaseEventArgs
{
    public T Item { get; }
    public int Index { get; }

    public CustomListItemEventArgs(T item, int index, ModificationTypes modificationTypes) : base(modificationTypes)
    {
        Item = item;
        Index = index;
    }
}

public class CustomListEventArgs : CustomListBaseEventArgs
{
    public int OldCapacity { get; }
    public int NewCapacity { get; }

    public CustomListEventArgs(int oldCapacity, int newCapacity) : base(ModificationTypes.ListResized)
    {
        OldCapacity = oldCapacity;
        NewCapacity = newCapacity;
    }
}