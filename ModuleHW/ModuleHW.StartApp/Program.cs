using System;

namespace ModuleHW.StartApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var starter = new Starter();
            starter.Run().GetAwaiter().GetResult();

            Console.WriteLine("\nAll tasks completed!");
            Console.ReadKey();
        }
    }
}
