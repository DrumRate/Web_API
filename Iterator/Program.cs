using System;

namespace Iterator
{
    class Program
    {
        static void Main(string[] args)
        {
            var a = new Iterator();
            var b = a.GetEnumerator();
            while (b.MoveNext())
            {
                Console.WriteLine(b.Current);
            }
            
        }
    }
}
