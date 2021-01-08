using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Advent_of_Code_2020
{
    class Day_7
    {
        readonly static string strDesiredBag = "shiny gold bags";

        public static int SolveNumberBagColorsContainAtLeastOneBag(List<string> listInputPuzzle)
        {
            int returnNumBags = 0;

            foreach(string rawLine in listInputPuzzle)
            {
                Bag currBag = ConstructBagFromInput(rawLine);
                returnNumBags += CountNumberBagColorsContainAtLeastOneBag(currBag);
            }

            return returnNumBags;
        }

        private static Bag ConstructBagFromInput(string rawLine)
        {
            string noPeriodRawLine = rawLine.Trim('.');                         // shiny lime bags contain 3 muted magenta bags, 3 clear cyan bags.
            string[] splitRawLine = noPeriodRawLine.Split(" contain ");         // [shiny lime bags][3 muted magenta bags, 3 clear cyan bags]
            string[] childRawLine = splitRawLine[1].Split(", ");                // [3 muted magenta bags][3 clear cyan bags]
            int[] numChildBags = new int[childRawLine.Length];
            string[] strChildBags = new string[childRawLine.Length];

            for (int i = 0; i < childRawLine.Length; i++)
            {
                string strNumChildBag = Regex.Match(childRawLine[i], @"\d+").Value;
                if (childRawLine[i] == "no other bags")
                    strNumChildBag = "0";
                numChildBags[i] = Int32.Parse(strNumChildBag);
                strChildBags[i] = childRawLine[i].Replace(strNumChildBag, String.Empty).Trim();
            }
            Bag currentBag = new Bag() { BagName = splitRawLine[0], NumberChildBags = numChildBags, StringChildBags = strChildBags };
            return currentBag;
        }

        private static int CountNumberBagColorsContainAtLeastOneBag(Bag currBag)
        {
            int numBags = 0;
            if (currBag.BagName == strDesiredBag)
                numBags++;
            for (int i = 0; i < currBag.StringChildBags.Length; i++)
            {
                if (currBag.StringChildBags[i] == strDesiredBag)
                    numBags++;
            }
            return numBags;
        }
    }

    public class Bag
    {
        public string BagName { get; set; }
        public int[] NumberChildBags { get; set; }
        public string[] StringChildBags { get; set; }
    }
}

/*
 * 1. Read each Line
 * 2. Each Line -> Split('_contains_') = Array
 * 3. Array[1] -> Split(',_') = Array_Child
 * 4. Each Array_Child -> Split('_') = [int][bag*]
 * 5. Pass above into constructor of Struct with one Parent and multiple Children
 * 6. Save constructed Nodal Object in some data structure
 */