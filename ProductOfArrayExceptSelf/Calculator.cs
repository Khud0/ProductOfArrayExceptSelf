using System;

namespace Khud0
{
    namespace Utility
    {
        public class Calculator
        {
            public static int ProductOf(params int[] numbers)
            {
                int amountOfArguments = numbers.Length;
                if (amountOfArguments == 0) { Console.WriteLine($"Provide at least 1 argument! {nameof(ProductOf)} returned a default value."); return default; }

                int result = 1;
                for (int i=1; i<amountOfArguments; i++)
                {
                    result *= numbers[i];
                }

                return result;
            }

            public static int ProductOf(int skipIndex, params int[] numbers)
            {
                int amountOfArguments = numbers.Length;
                if (amountOfArguments == 0) { Console.WriteLine($"Provide at least 1 argument! {nameof(ProductOf)} returned a default value."); return default; }

                int result = 1;
                for (int i=0; i<amountOfArguments; i++)
                {
                    if (i == skipIndex) continue;
                    result *= numbers[i];
                }

                return result;
            }
        }
    }
}
