using System;
using System.Collections.Generic;
using Khud0.Utility;

namespace ProductOfArrayExceptSelf
{
    class ProductFinder
    {
        private static List<Func<int[], int[]>> allMethods = new List<Func<int[], int[]>>()
        {
            new Func<int[], int[]>(ProductExceptSelfMultiply),
            new Func<int[], int[]>(ProductExceptSelfDivide),
            new Func<int[], int[]>(ProductExceptSelfSides),
            new Func<int[], int[]>(ProductExceptSelfSidesShort)   
        };

        public static void RunAllMethods(int[] inputArray)
        {
            int arrayLength = inputArray.Length;

            foreach (Func<int[], int[]> method in allMethods)
            {
                Stopwatcher.Start();
                Console.WriteLine("-----");
                int[] result = method.Invoke(inputArray);
                Stopwatcher.Stop();

                Console.WriteLine($"{method.Method} generated the following result:");
                for (int i=0; i<arrayLength; i++)
                {
                    Console.Write(result[i].ToString() + " ");
                }
                Console.WriteLine("\n");
            }
        }   


        #region Methods

        /// <summary>
        /// Every single array cell value is calculated by multiplying everything but value at the current index.
        /// </summary>
        public static int[] ProductExceptSelfMultiply(int[] inputArray)
        {
            int arrayLength = inputArray.Length;
            if (arrayLength == 0) { Console.WriteLine($"{nameof(ProductExceptSelfMultiply)} requires at least 1 argument!"); return inputArray; }

            int[] outArray = new int[arrayLength];
            for (int i=0; i<arrayLength; i++)
            {
                outArray[i] = Calculator.ProductOf(i, inputArray);
            }

            return outArray;
        }

        /// <summary>
        /// Multiplication is only done once and then you divide the overall product by value at the current index.
        /// </summary>
        public static int[] ProductExceptSelfDivide(int[] inputArray)
        {
            int arrayLength = inputArray.Length;
            if (arrayLength == 0) { Console.WriteLine($"{nameof(ProductExceptSelfMultiply)} requires at least 1 argument!"); return inputArray; }

            int totalProduct = Calculator.ProductOf(inputArray);
            int[] outArray = new int[arrayLength];
            for (int i=0; i<arrayLength; i++)
            {
                outArray[i] = totalProduct / inputArray[i];
            }
            
            return outArray;
        }

        /// <summary>
        /// rightArray[i] contains product of every number to the right of i, leftArray - to the left.
        /// left[i] * right[i] = product of all numbers except i.
        /// The idea was looked up here: https://www.youtube.com/watch?v=khTiTSZ5QZY
        /// </summary>
        public static int[] ProductExceptSelfSides(int[] inputArray)
        {
            int arrayLength = inputArray.Length;
            if (arrayLength == 0) { Console.WriteLine($"{nameof(ProductExceptSelfMultiply)} requires at least 1 argument!"); return inputArray; }

            int[] leftArray, rightArray, outArray;
            leftArray = new int[arrayLength];
            rightArray = new int[arrayLength];
            outArray = new int[arrayLength];
            leftArray[0] = 1; // There's nothing to the left of the first value in the array (array[0])

            // Multiply every value to the left of the current index to get the product of all the values to the left
            // For value at every new index, you multiply "real" number before by the product of all "previous" values
            for (int i=1; i<arrayLength; i++)
            {
                leftArray[i] = inputArray[i-1] * leftArray[i-1];
            }

            // Previous version for right array
            /*rightArray[arrayLength-1] = 1; // There's nothing to the right of the last value in the array (array[arrayLength-1]
            for (int i=arrayLength-2; i>=0; i--)
            {
                rightArray[i] = inputArray[i+1] * rightArray[i+1];
            }*/

            int currentRight = 1;
            for (int i=arrayLength-1; i>=0; i--)
            {
                rightArray[i] = currentRight; // Use the previous value, the same as inputArray[i+1] from the previous version shown above
                currentRight *= inputArray[i]; // Current value is just stored, so in the next step it will become "the previous one"
            }
            
            for (int i=0; i<arrayLength; i++)
            {
                outArray[i] = leftArray[i] * rightArray[i];
            }
            
            return outArray;
        }

        /// <summary>
        /// ProductExceptSelfSides, but in constant space complexity.
        /// Left array is counted in outArray, so we don't even need to store right array anywhere.
        /// </summary>
        public static int[] ProductExceptSelfSidesShort(int[] inputArray)
        {
            int arrayLength = inputArray.Length;
            if (arrayLength == 0) { Console.WriteLine($"{nameof(ProductExceptSelfMultiply)} requires at least 1 argument!"); return inputArray; }

            int[] rightArray, outArray;
            rightArray = new int[arrayLength];
            outArray = new int[arrayLength];
            outArray[0] = 1; // There's nothing to the left of the first value in the array (array[0])

            // Multiply every value to the left of the current index to get the product of all the values to the left
            // For value at every new index, you multiply "real" number before by the product of all "previous" values
            for (int i=1; i<arrayLength; i++)
            {
                outArray[i] = inputArray[i-1] * outArray[i-1];
            }

            int currentRightProduct = 1;
            for (int i=arrayLength-1; i>=0; i--)
            {
                outArray[i] = outArray[i] * currentRightProduct;
                currentRightProduct = currentRightProduct * inputArray[i];
                
            }
            
            return outArray;
        }

        #endregion
    }
}
