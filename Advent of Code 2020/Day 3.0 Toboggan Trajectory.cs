using System;
using System.Collections.Generic;
using System.Text;

namespace Advent_of_Code_2020
{
    class Day_3
    {
        static readonly int[,] slopes = { {1,1}, {3,1}, {5,1}, {7,1}, {1,2} };

        public static uint SolveNumTrajectoryTrees(List<string> listInputPuzzle, int xMov, int yMov)
        {
            return CalculateNumberOfTrees(listInputPuzzle, xMov, yMov);
        }

        public static double SolveProductTreesEncountered(List<string> listInputPuzzle)
        {
            double productOfTrees = 1;
            int[] numTrees = new int[] { };
            for (int i = 0; i < slopes.GetLength(0); i++)
            {
                productOfTrees = productOfTrees * CalculateNumberOfTrees(listInputPuzzle, slopes[i, 0], slopes[i, 1]);
            }

            return productOfTrees;
        }

        private static uint CalculateNumberOfTrees(List<string> listInputPuzzle, int xMov, int yMov)
        {
            uint numTrees = 0;
            int xPos = 0, yPos = 0;
            int numSkipFlag = 1;                                    //Used as Flag to skip every other iteration
            foreach (string rawLine in listInputPuzzle)
            {
                if (numSkipFlag > 1)
                {
                    numSkipFlag -= 1;
                    continue;                                       //Continue skips code below to next iteration; break terminates loop
                }
                numSkipFlag = yMov;

                if (rawLine[xPos] == '#')                           //'' for char comparisons; "" for string
                {
                    numTrees += 1;
                }
                xPos += xMov;
                yPos += yMov;
                if (yPos > listInputPuzzle.Capacity - 1)            //Check if upcoming Descent is out of bounds
                    return numTrees;
                if (xPos > rawLine.Length - 1)
                {
                    xPos -= rawLine.Length;
                }
            }
            return numTrees;
        }
    }
}
