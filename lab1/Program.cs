using System;

namespace lab1
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                ListDemo.ListShowAll();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}