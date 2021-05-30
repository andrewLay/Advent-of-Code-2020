/*
DAY 10.0 EXAMPLE
===============
For an electrical outlet initially rated at 0 jolts, suppose that in your bag, you have adapters with the following joltage ratings:
16
10
15
5
1
11
7
19
6
12
4

With these adapters, your device's built-in joltage adapter would be rated for 19 + 3 = 22 jolts, 3 higher than the highest-rated adapter.
Adapters can only connect to a source 1-3 jolts lower than its rating.
So, there are 7 differences of 1 jolt and 5 differences of 3 jolts using every adapter in series.
 
DAY 10.1
=======
(Q): What is the number of 1-jolt differences multiplied by the number of 3-jolt differences?
(A): The number of 1-jolt differences multiplied by 3-jolt differences is 35.

DAY 10.2
=======
(0), 1, 4, 5, 6, 7, 10, 11, 12, 15, 16, 19, (22)
(0), 1, 4, 5, 6, 7, 10, 12, 15, 16, 19, (22)
(0), 1, 4, 5, 7, 10, 11, 12, 15, 16, 19, (22)
(0), 1, 4, 5, 7, 10, 12, 15, 16, 19, (22)
(0), 1, 4, 6, 7, 10, 11, 12, 15, 16, 19, (22)
(0), 1, 4, 6, 7, 10, 12, 15, 16, 19, (22)
(0), 1, 4, 7, 10, 11, 12, 15, 16, 19, (22)
(0), 1, 4, 7, 10, 12, 15, 16, 19, (22)

(Q): What is the total number of distinct ways you can arrange the adapters to connect the charging outlet to your device?
(A): The total number of arrangements that connect the charging outlet to your device is 8.
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace Advent_of_Code_2020
{
    class Day_10
    {
        public static int ProductOfOneAndThreeJoltDifferences(List<string> listInputPuzzle)
        {
            int returnProduct = -1;

            Dictionary<int, int> joltDifferences = new Dictionary<int, int> { [1] = 0, [2] = 0, [3] = 0 };
            List<int> sortedAdapters = SortedAdapters(listInputPuzzle);

            // Check previous adapter ratings relative to the current adapter for 1-2-3 joltage differences 
            for (int i = 1; i < sortedAdapters.Count - 1; i+=2)
            {
                if (sortedAdapters[i] - 1 == sortedAdapters[i - 1])
                    joltDifferences[1]++;
                else if (sortedAdapters[i] - 2 == sortedAdapters[i - 1])
                    joltDifferences[2]++;
                else if (sortedAdapters[i] - 3 == sortedAdapters[i - 1])
                    joltDifferences[3]++;
                // Check following adapter ratings relative to the current adapter for 1-2-3 joltage differences
                if (sortedAdapters[i] + 1 == sortedAdapters[i + 1])
                    joltDifferences[1]++;
                else if (sortedAdapters[i] + 2 == sortedAdapters[i + 1])
                    joltDifferences[2]++;
                else if (sortedAdapters[i] + 3 == sortedAdapters[i + 1])
                    joltDifferences[3]++;
            }

            returnProduct = joltDifferences[1] * joltDifferences[3];
            
            return returnProduct;
        }

        public static int TotalNumberOfWaysConnectingOutletToCharger(List<string> listInputPuzzle)
        {
            int returnProduct = -1;

            List<int> sortedAdapters = SortedAdapters(listInputPuzzle);
            List<int> firstDifferences = new List<int>();

            // Find the first differences between sequentially increasing adapter ratings and store as List<int>
            for (int i = 1; i < sortedAdapters.Count; i++)
            {
                int diff = sortedAdapters[i] - sortedAdapters[i - 1];
                firstDifferences.Add(diff);
            }

            // Need to isolate subsequence segments of first differences that are all only of value 1,2 (segments bounded by value 3)
            // Then count the total number of ways to sum within a segment up to value 3, multiplied by the number of ways in separate segments
            // (0) - 1 - 4 - 5 - 6 - 7 - 10 - 11 - 12 - 15 - 16 - 19 - (22) : adapters
            //     1   3  [1   1   1]  3   [1    1]   3    1    3    3      : first differences
            //           >> 4 ways <<    >> 2 ways <<                       : 8 total ways
            foreach (int diff in firstDifferences)
            {
                if (diff < 3)       // Value of 1,2
                {

                }
            }

            return returnProduct;
        }

        private static List<int> SortedAdapters(List<string> listInputPuzzle)
        {
            List<int> sortedAdapters = new List<int>();
            foreach (string adapter in listInputPuzzle)
            {
                int newAdapterRating = Int32.Parse(adapter);
                int count = sortedAdapters.Count;
                int temp = -1;
                sortedAdapters.Add(newAdapterRating);
                while (count > 0 && sortedAdapters[count - 1] > newAdapterRating)
                {
                    temp = sortedAdapters[count - 1];
                    sortedAdapters[count - 1] = newAdapterRating;
                    sortedAdapters[count] = temp;
                    count--;
                }
            }
            // Account for the first and last joltage jumps to the device's rating (+3 jolts above the highest rated adapter)
            sortedAdapters.Insert(0, 0);
            sortedAdapters.Add(sortedAdapters[sortedAdapters.Count - 1] + 3);

            return sortedAdapters;
        }
    }
}
