/*
DAY 1.0 EXAMPLE
===============
1721
979
366
299
675
1456
 
DAY 1.1
=======
(Q): Find the two entries that sum to 2020; what do you get if you multiply them together?
(A): The two entries that sum to 2020 are 1721 and 299. Therefore, the correct answer is 514579.

DAY 1.2
=======
(Q): What is the product of the three entries that sum to 2020?
(A): The three entries that sum to 2020 are 979, 366, and 675. Multiplying them together produces the answer, 241861950.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Advent_of_Code_2020
{
    class Day_1
    {
        public static void SolvePuzzle2Numbs(List<string> listInputPuzzle)
        {
            List<int> differences = new List<int>();
            foreach (string input in listInputPuzzle)
            {
                int intInput = Int32.Parse(input);
                int complement = 2020 - intInput;
                differences.Add(complement);
                foreach (int difference in differences)
                {
                    if (intInput == difference)
                    {
                        Console.WriteLine("Day 1.1 -- The sum of {0} + {1} = 2020 and their product is {2}", intInput, complement, intInput * complement);
                    }
                }
            }
        }

        public static void SolvePuzzle3Numbs(List<string> listInputPuzzle)
        {
            int ia, ib, ic;
            foreach (string a in listInputPuzzle)
            {
                ia = Int32.Parse(a);
                foreach (string b in listInputPuzzle)
                {
                    ib = Int32.Parse(b);
                    foreach (string c in listInputPuzzle)
                    {
                        ic = Int32.Parse(c);
                        if (ia + ib + ic == 2020)
                        {
                            Console.WriteLine("Day 1.2 -- The sum of {0} + {1} + {2} = 2020 and their product is {3}", ia, ib, ic, ia*ib*ic);
                        }
                    }
                }
            }
        }
    }
}
