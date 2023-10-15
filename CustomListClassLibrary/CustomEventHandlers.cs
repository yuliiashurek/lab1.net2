using System;

namespace CustomListClassLibrary;

public static class CustomEventHandlers
{
    public static void PrintListItemEventHandler<T>(object sender, CustomListItemEventArgs<T> e)
    {
        Console.WriteLine($"\t\t\t\t\t\t\t\t\tEvent: {e.ModificationTypes} Item: {e.Item} Index: {e.Index} Date: {e.DateTime}");
    }

    public static void PrintListEventHandler(object sender, CustomListBaseEventArgs e)
    {
        Console.WriteLine($"\t\t\t\t\t\t\t\t\tEvent: {e.ModificationTypes} {e.DateTime}");
    }
        
    public static void PrintListResizedEventHandler(object sender, CustomListEventArgs e)
    {
        Console.WriteLine($"\t\t\t\t\t\t\t\t\tEvent: {e.ModificationTypes} Old capacity: {e.OldCapacity} New capacity: {e.NewCapacity} {e.DateTime}");
    }
}