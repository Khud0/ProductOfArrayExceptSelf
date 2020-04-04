using System;

namespace ProductOfArrayExceptSelf
{
    class Program
    {
        private static int[] testArray = {1, 2, 3, 4};

        private static void Main(string[] args)
        {
            ProductFinder.RunAllMethods(testArray);
            Console.ReadKey();
        }
    }
}
