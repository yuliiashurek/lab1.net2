using System;

namespace lab1
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            MyNewList<int> list = new MyNewList<int>(5) { 1, 2, 3, 4, 5 };
            foreach (var el in list)
            {
                Console.WriteLine(el);
            }
        }
    }
}