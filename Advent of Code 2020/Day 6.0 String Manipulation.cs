using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Advent_of_Code_2020
{
    class Day_6
    {
        const string regexPattern = @"^[a-z]+$";
        enum AnyoneEveryone { Anyone, Everyone };

        public static int SolveNumberDistinctAnswersByAnyone(List<string> listInputPuzzle)
        {
            int numQuestionsAnswered = 0;
            List<string> groups = new List<string>();
            SplitBlankLinesAsGroup(listInputPuzzle, groups, AnyoneEveryone.Anyone);                 // 'groups' parameter is passed by reference and changed in function call (mutable)
            foreach (string group in groups)
            {
                IEnumerable<char> match = group.Distinct();                                         // Distinct() on List<char> returns all unique char corresponding to Anyone in Group's unique answer
                numQuestionsAnswered += match.Count();
            }
            return numQuestionsAnswered;
        }

        public static int SolveNumberDistinctAnswersByEveryone(List<string> listInputPuzzle)
        {
            int numQuestionsAnswered = 0;
            List<string> groups = new List<string>();
            SplitBlankLinesAsGroup(listInputPuzzle, groups, AnyoneEveryone.Everyone);               // 'groups' parameter is passed by reference and changed in function call (mutable)
            
            foreach (string group in groups)                                                        // groups = ["abc" , "a b c" , "ab ac" , "a a a a" , "b"]
            {
                string[] persons = group.Split(" ");                                                // group = ["abc"] ..... group = ["a" , "b" , "c"] ..... group = ["ab" , "ac"] ..... group = ["a" , "a" , "a" , "a"] ..... group = ["b"]
                Dictionary<char, int> firstPersonCharTemplate = new Dictionary<char, int>();
                foreach (char keyCharFirstPerson in persons[0])
                {
                    firstPersonCharTemplate.Add(keyCharFirstPerson, 0);                             // Dict<char, int> = { KEY:VALUE } = { a:0 , b:0 , c:0 } ..... { a:0 } ..... { a:0 , b:0 } ..... { a:0 } ..... { b:0 }
                }
                List<char> dictKeys = new List<char>(firstPersonCharTemplate.Keys);                 // Need to capture Dict<>.KeyCollection because reading and writing to same Dict within same loop is not allowed
                foreach (string person in persons)
                {
                    foreach (char keyCharFirstPerson in dictKeys)
                    {
                        foreach (char charPerson in person)
                        {
                            if (charPerson == keyCharFirstPerson)
                            {
                                firstPersonCharTemplate[keyCharFirstPerson] += 1;
                            }
                        }
                    }
                }
                foreach (char keyCharFirstPerson in dictKeys)
                {
                    if (firstPersonCharTemplate[keyCharFirstPerson] == persons.Length)
                    {
                        numQuestionsAnswered += 1;
                    }
                }
            }
            return numQuestionsAnswered;
        }

        private static void SplitBlankLinesAsGroup(List<string> listInputPuzzle, List<string> groups, AnyoneEveryone who)
        {
            string oneGroup = String.Empty;
            foreach (string rawLine in listInputPuzzle)
            {
                if (rawLine != "")
                {
                    if (Regex.IsMatch(rawLine, regexPattern))
                    {
                        if (who == AnyoneEveryone.Anyone)
                            oneGroup += (rawLine);
                        else if (who == AnyoneEveryone.Everyone)
                            oneGroup += (" " + rawLine);
                        continue;
                    }
                }
                else
                {
                    oneGroup = oneGroup.Trim();
                    groups.Add(oneGroup);
                    oneGroup = String.Empty;
                }
            }
            oneGroup = oneGroup.Trim();
            groups.Add(oneGroup);                                                                   // Must force one last add to group since "" input line triggers the add, normally
        }
    }
}
