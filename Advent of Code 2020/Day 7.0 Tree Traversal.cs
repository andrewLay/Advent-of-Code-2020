/*
DAY 7.0 EXAMPLE
===============
light red bags contain 1 bright white bag, 2 muted yellow bags.
dark orange bags contain 3 bright white bags, 4 muted yellow bags.
bright white bags contain 1 shiny gold bag.
muted yellow bags contain 2 shiny gold bags, 9 faded blue bags.
shiny gold bags contain 1 dark olive bag, 2 vibrant plum bags.
dark olive bags contain 3 faded blue bags, 4 dotted black bags.
vibrant plum bags contain 5 faded blue bags, 6 dotted black bags.
faded blue bags contain no other bags.
dotted black bags contain no other bags.
 
DAY 7.1
=======
(Q): You have a shiny gold bag. If you wanted to carry it in at least one other bag, how many different bag colors would be valid for the outermost bag?
(A): The number of bag colors that can eventually contain at least one shiny gold bag is 4.

DAY 7.2
=======
(Q): You have a shiny gold bag. How many individual bags are required inside your single shiny gold bag?
(A): 1 (dark olive) + 1*7 (3 faded blue & 4 dotted black) + 2 (vibrant plum) + 2*11 (5 faded blue & 6 dotted black) = 32 bags!
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Advent_of_Code_2020
{
    class Day_7
    {
        static Dictionary<string, Dictionary<string, int>> masterDict = new Dictionary<string, Dictionary<string, int>>();

        public static int SolveNumberBagColorsContainAtLeastOneShinyGoldBag(List<string> listInputPuzzle)
        {
            int returnNumBags = 0;

            foreach (string rawLine in listInputPuzzle)                                             // Load Master definition of all Bag Colours as 'masterDict' into memory
            {
                string keyDefn = rawLine.Split(" contain ")[0];
                Dictionary<string, int> currentChildBags = ConstructDictOfChildBagsFromInput(rawLine);
                masterDict.Add(keyDefn, currentChildBags);
            }
            foreach (string masterBagName in masterDict.Keys)                                       // Check each masterDict definition (each line = Bag Colour), for potentially containing shiny gold bags
            {
                bool containShinyBag = Helper_TraverseTree(masterDict[masterBagName], false);       // [muted magenta bags,3][clear cyan bags,3]
                if (containShinyBag)
                {
                    returnNumBags += 1;
                }
            }
            masterDict.Clear();

            return returnNumBags;
        }

        public static int SolveNumberBagsRequiredInShinyGoldBag(List<string> listInputPuzzle)
        {
            int returnNumBags = 0;

            foreach (string rawLine in listInputPuzzle)                                             // Load Master definition of all Bag Colours as 'masterDict' into memory
            {
                string keyDefn = rawLine.Split(" contain ")[0];
                Dictionary<string, int> currentChildBags = ConstructDictOfChildBagsFromInput(rawLine);
                masterDict.Add(keyDefn, currentChildBags);
            }
            returnNumBags += Helper_TraverseTree_ReturnINT(masterDict["shiny gold bags"], 0);
            masterDict.Clear();

            return returnNumBags;
        }

        private static Dictionary<string, int> ConstructDictOfChildBagsFromInput(string rawLine)
        {
            Dictionary<string, int> currentChildBags = new Dictionary<string, int>();
            string noPeriodRawLine = rawLine.Trim('.');                                             // shiny lime bags contain 3 muted magenta bags, 3 clear cyan bags
            string[] splitRawLine = noPeriodRawLine.Split(" contain ");                             // [shiny lime bags][3 muted magenta bags, 3 clear cyan bags]
            string[] childRawLine = splitRawLine[1].Split(", ");                                    // [3 muted magenta bags][3 clear cyan bags]

            for (int i = 0; i < childRawLine.Length; i++)
            {
                string strNumChildBag = Regex.Match(childRawLine[i], @"\d+").Value;
                if (childRawLine[i] == "no other bags")
                    strNumChildBag = "0";
                string strChildBag = childRawLine[i].Replace(strNumChildBag, String.Empty).Trim();
                currentChildBags.Add(strChildBag, Int32.Parse(strNumChildBag));                     // [muted magenta bags,3][clear cyan bags,3]
            }
            return currentChildBags;
        }
        
        private static bool Helper_TraverseTree(Dictionary<string, int> currBag, bool containShinyBag)
        {
            // RECURSIVE FUNCTION ::
            // get called with parameter Dict<string, int>
            // foreach childBagName in parameter Dict<string, int> { call Helper_TraverseTree(Dictionary<string,int>, bool) }
            // check if Dict[current Instance] == "no other bags" --> return False
            // check if Dict[current Instance] == "shiny gold bags"" --> return True
            // otherwise, current Instance is not end-tip of branch, so instantiate its children

            foreach (string childBagName in currBag.Keys)
            {
                if (childBagName == "no other bags")
                {
                    return false;
                }
                else if (childBagName.Contains("shiny gold bag"))
                {
                    containShinyBag = true;
                }
                else
                {
                    if (masterDict.ContainsKey(childBagName))
                        containShinyBag |= Helper_TraverseTree(masterDict[childBagName], containShinyBag);
                    else
                        containShinyBag |= Helper_TraverseTree(masterDict[childBagName + "s"], containShinyBag);
                }
            }
            return containShinyBag;
        }

        private static int Helper_TraverseTree_ReturnINT(Dictionary<string, int> currBag, int returnNumBags)
        {
            // RECURSIVE FUNCTION ::
            // get called with parameter Dict<string, int>
            // foreach childBagName in parameter Dict<string, int> { call Helper_TraverseTree(Dictionary<string,int>, int) }
            // count the current Instance's number of bags before iterating children
            // check if Dict[current Instance] == "no other bags" --> return count
            // check if Dict[current Instance] == "shiny gold bags"" --> return count++
            // otherwise, current Instance is not end-tip of branch, so instantiate its children

            foreach (string childBagName in currBag.Keys)
            {
                if (childBagName == "no other bags")
                {
                    return 0;
                }
                if (masterDict.ContainsKey(childBagName))
                    returnNumBags += currBag[childBagName] + currBag[childBagName] * Helper_TraverseTree_ReturnINT(masterDict[childBagName], 0);
                else
                    returnNumBags += currBag[childBagName] + currBag[childBagName] * Helper_TraverseTree_ReturnINT(masterDict[childBagName + "s"], 0);
            }
            return returnNumBags;
        }
    }
}