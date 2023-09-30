using System;

namespace lab1
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                MyNewList<int> list = new MyNewList<int>() { 1, 2, 3, 4, 5 };
              
                var enumerator = list.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    int item = enumerator.Current;
                    Console.WriteLine(item);
                }

// Now, you want to iterate through the collection again from the beginning
                enumerator.Reset();

// Iterate through the collection again
                while (enumerator.MoveNext())
                {
                    int item = enumerator.Current;
                    Console.WriteLine(item);
                }
                // foreach (var el in list)
                // {
                //     Console.WriteLine(el);
                // }

                //list[-1] = 333;

                // int[] destinationArray = new int[list.Count+2]; // Creating a destination array with enough space
                //
                // list.CopyTo(destinationArray, 1); // Copy elements from myCollection to destinationArray starting at index 2

                // foreach (var el in destinationArray)
                // {
                //     Console.WriteLine(el);
                // }

                foreach (var el in list)
                {
                    Console.WriteLine(el);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}