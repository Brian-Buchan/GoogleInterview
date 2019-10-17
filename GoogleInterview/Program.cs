using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SandS;

namespace GoogleInterview
{
    class Program
    {
        static void Main(string[] args)
        {
            #region UserStringInput
            int stringAsize = 0;
            int stringBsize = 0;
            // Question one jump incase of error entering list length
            Q1:
            Console.WriteLine("How many words in your first list?");
            try
            {
                stringAsize = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception caught! Please enter a digit \nPress any key to continue");
                Console.ReadLine();
                Console.Clear();
                goto Q1;
            }
            string[] Astring = new string[stringAsize];
            addWord(Astring);
            Console.Clear();

            // Question 2 jump incase of error entering list length
            Q2:
            Console.WriteLine("How many words in your second list?");
            try
            {
                stringBsize = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception caught! Please enter a digit \nPress any key to continue");
                Console.ReadLine();
                Console.Clear();
                goto Q2;
            }
            string[] Bstring = new string[stringBsize];
            addWord(Bstring);
            Console.Clear();

            printArray(Astring);
            printArray(Bstring);

            #endregion


            // TODO: Allow for user to input strings themself
            //string[] Astring = { "Lorem", "ipsum", "dolor", "sit", "amet", "consectetur", "adipiscing", "elit", "Morbi", "ultricies", "leo", "nec", "aliquam", "fermentum", "Sed", "commodo", "faucibus", "lectus", "facilisis", "feugiat", "Sed", "id", "condimentum", "lectus", "eget" };
            //string[] Bstring = { "In", "hac", "habitasse", "platea", "dictumst", "Mauris", "scelerisque", "arcu", "in", "ligula", "tristique", "imperdiet", "Duis", "id", "dui", "a", "diam" };
            //// Arrays are type safe so we cannot set a string[] = int[]
            // Lists can overcome this but may add more overhead than creating 2 extra arrays
            int[] Aint = new int[Astring.Length];
            int[] Bint = new int[Bstring.Length];

            // Convert A string[] to A int[]
            for (int i = 0; i < Astring.Length; i++)
            {
                Aint[i] = ConvertStringToInt(Astring[i]);
            }
            // Convert B string[] to B int[]
            for (int i = 0; i < Bstring.Length; i++)
            {
                Bint[i] = ConvertStringToInt(Bstring[i]);
            }

            // TODO: Implement our own quick sort
            //       This will allow us to change the indexes of the string[]
            //       alongside the int[] so we can say which string in the B[]
            //       is larger than which strings in the A[]
            SandS.Algorithm.Library.SortNamespace.SortingAlgorithm.QuickSort(Aint);
            SandS.Algorithm.Library.SortNamespace.SortingAlgorithm.QuickSort(Bint);

            // The index of the largest A that is smaller than the B's checked so far
            // This is also the count of As that will get set to the B[] when the B[]
            //  is found to be the same size or smaller than that of the A[]
            int AintIndex = 0;

            // Search each B in the array and set it's value to AintIndex which is
            //  returned by the recursive function: GetCountOfSmaller_As()
            for (int BintIndex = 0; BintIndex < Bint.Length; BintIndex++)
            {
                // Reference AintIndex so we can update it's value inside the function and pass
                //  back the correct number throughout any number of recursion of the function
                Bint[BintIndex] = GetCountOfSmaller_As(Aint, Bint, ref AintIndex, BintIndex);
            }

            for (int i = 0; i < Bint.Length; i++)
            {
                Console.WriteLine("There are " + Bint[i] + " strings in A smaller than this B");
            }
            Console.ReadKey();
        }

        static void addWord(string[] array)
        {
            for (int x = 0; x < array.Length; x++)
            {
                Console.WriteLine("Please enter a word for your list.");
                array[x] = Console.ReadLine();
            }
        }

        static int GetCountOfSmaller_As(int[] Aint, int[] Bint, ref int AintIndex, int BintIndex)
        {
            // Check first to avoid out of bounds error
            if (AintIndex == Aint.Length)
            {
                // End of Aint
                // return the count of As
                return AintIndex;
            }
            else if (Aint[AintIndex] < Bint[BintIndex])
            {
                // This A is smaller than this B
                // Add one to AintIndex (count of As smaller than this B)
                // And check next A
                AintIndex++;
                return GetCountOfSmaller_As(Aint, Bint, ref AintIndex, BintIndex);
            }
            else
            {
                // This A is as big or larger than this B
                // return the count of As
                return AintIndex;
            }
        }

        static int ConvertStringToInt(string input)
        {
            // Set to all lowercase to get correct ASCII number from all characters
            input.ToLower();
            // ASCII (Char to Int) a = 97, b = 98 ..... y = 121, z = 122
            int lowestASCII_Index = input[0];
            // Assumes string is not null or blank, ASCII char for these values are
            //  different than those for a-z
            int output = 1;
            // Start at i = 1 because we have already set count and index for position 0
            for (int i = 1; i < input.Length; i++)
            {
                if (input[i] < lowestASCII_Index)
                {
                    // Found new smaller character
                    lowestASCII_Index = input[i];
                    // Count = 1
                    output = 1;
                }
                else if (input[i] == lowestASCII_Index)
                {
                    // Found same character as lowest
                    // Increase count
                    output++;
                }
                else
                {
                    // Found larger character
                    // IGNORE
                }
            }
            return output;
        }

        static void printArray(string[] array)
        {
            Console.WriteLine("This list contains the words:");
            for (int x = 0; x < array.Length; x++)
            {
                Console.Write(array[x] + ", ");
            }
            Console.WriteLine("\n");

        }
    }
}
