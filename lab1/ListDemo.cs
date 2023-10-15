using System;
using System.Linq;
using CustomListClassLibrary;

namespace lab1;

public static class ListDemo
{
    public static void ListShowAll()
    {
        CustomList<int> list = new CustomList<int>();

        list.ItemAdded += CustomEventHandlers.PrintListItemEventHandler!;
        list.ItemRemoved += CustomEventHandlers.PrintListItemEventHandler!;
        list.ListCleared += CustomEventHandlers.PrintListEventHandler!;
        list.ListResized += CustomEventHandlers.PrintListResizedEventHandler!;
                
        list.Add( 2);
        list.Add(3);
        list.Insert(2, 5);
        list.Insert(2, 4);
        list.Insert(0, 1);

        Console.Write("After adding and inserting elements: ");
        Printing(list);
                
        Console.WriteLine($"Contains element 3: {list.Contains(3)}");

        Console.WriteLine($"Index of element 5: {list.IndexOf(5)}");
                
        int[] arrayToCopyTo = new int[list.Count];
        list.CopyTo(arrayToCopyTo, 0);
        Console.Write("Array copied from list: ");
        arrayToCopyTo.ToList().ForEach(item => Console.Write($"{item} "));
        Console.WriteLine();
                
        list[1] = 10;
        Console.Write("After list[1] = 10: ");
        Printing(list);

        list.Remove(3);
        list.RemoveAt(2);
        Console.Write("After removing el=3 and at index=2: ");
        Printing(list);
                
        list.Clear();
        Console.WriteLine($"After clear. Count: {list.Count}");
    }
    
    private static void Printing<T>(CustomList<T> list)
    {
        string elements = string.Join(", ", list);
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine(elements);
        Console.ForegroundColor = ConsoleColor.Gray;
    }
}