namespace ClassLibrary1
{
    public class Program
    {
        public static void Main()
        {
            MyNewList<int> list = new MyNewList<int>(5) { 1, 2, 3, 4, 5 };
            foreach (var el in list)
            {
                Console.WriteLine(el);
            }
        }
    }
}