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
