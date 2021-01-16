using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Advent_of_Code_2020
{
    class Day_7
    {
        static Dictionary<string, Dictionary<string, int>> masterDict = new Dictionary<string, Dictionary<string, int>>();

        public static int SolveNumberBagColorsContainAtLeastOneBag(List<string> listInputPuzzle)
        {
            int returnNumBags = 0;

            // Load Master definition of all Bag Types as 'masterDict' into memory.
            foreach (string rawLine in listInputPuzzle)
            {
                string keyDefn = rawLine.Split(" contain ")[0];
                Dictionary<string, int> currentChildBags = ConstructDictOfChildBagsFromInput(rawLine);
                masterDict.Add(keyDefn, currentChildBags);
            }
            foreach (string masterBagName in masterDict.Keys)
            {
                //var minionDict = new Dictionary<string, Dictionary<string, int>>(masterDict);
                returnNumBags += Helper_TraverseTree(masterDict[masterBagName], returnNumBags);     // [muted magenta bags,3][clear cyan bags,3]
            }
            masterDict.Clear();

            return returnNumBags;
        }

        private static Dictionary<string, int> ConstructDictOfChildBagsFromInput(string rawLine)
        {
            Dictionary<string, int> currentChildBags = new Dictionary<string, int>();
            string noPeriodRawLine = rawLine.Trim('.');                                             // shiny lime bags contain 3 muted magenta bags, 3 clear cyan bags.
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
        
        private static int Helper_TraverseTree(Dictionary<string, int> currBag, int returnNumBags)
        {
            // RECURSIVE FUNCTION ::
            // get called with parameter Dict<string, int>
            // foreach childBagName in parameter Dict<string, int> { call Helper_TraverseTree(Dictionary<string, int) }
            // check if Dict[current Instance] == "no other bags" --> return counter
            // check if Dict[current Instance] == "shiny gold bags"" --> return counter++

            foreach (string childBagName in currBag.Keys)
            {
                if (childBagName == "no other bags")
                {
                    return returnNumBags;
                }
                else if (childBagName.Contains("shiny gold bag"))
                {
                    returnNumBags++;
                }
                else
                {
                    if (masterDict.ContainsKey(childBagName))
                        returnNumBags = Helper_TraverseTree(masterDict[childBagName], returnNumBags);
                    else
                        returnNumBags = Helper_TraverseTree(masterDict[childBagName + "s"], returnNumBags);
                }
            }
            return returnNumBags;
        }
    }
}