/*
DAY 9.0 EXAMPLE
===============
35
20
15
25
47
40
62
55
65
95
102
117
150
182
127
219
299
277
309
576

For every number in the list, consider the previous 5 numbers (has a preamble of length 5).

DAY 9.1
=======
(Q): What is the first number in the list (after the preamble) which is not the sum of two of the 5 numbers before it?
(A): After the 5-number preamble, almost every number is the sum of two of the previous 5 numbers; the only number that does not follow this rule is 127.

DAY 9.2
=======
(Q): What is the encryption weakness (sum of the smallest and largest number in the contiguous range whose sum is 127) in your encrypted list of numbers?
(A): The numbers 15,25,47,40 produce the invalid number from Step 1 (127), therefore, the smallest + largest in this contiguous range is 62.
 */

#include "pch.h"
#include "Day_9.0_Encoding_Error.h"
#include <deque>

extern "C" {
	__declspec(dllexport) double FindFirstNumbNotSumOfPrevNumbs(double* arrayInputPuzzle, int numArrayInputs, int preambleSize) 
	{
		double firstNumbNotSumOfPrevNumbs = -1;

		std::deque<double> preamble;
		bool isDifferenceFoundInPreamble = true;
		for (int i = 0; i < numArrayInputs; i++) 
		{
			if (preamble.size() < preambleSize)						// Step 1. Populate the List<int> as preamble to the size specified by the user
			{
				preamble.push_back(arrayInputPuzzle[i]);
				continue;
			}
			else													// Step 2. Else, Preamble is already populated to the size specified by the user
			{
				double currNumForChecking = arrayInputPuzzle[i];
				isDifferenceFoundInPreamble = Helper_IsDifferenceFoundInPreamble(currNumForChecking, preamble);
				if (isDifferenceFoundInPreamble)
				{
					preamble.pop_front();
					preamble.push_back(arrayInputPuzzle[i]);
				}
				else
				{
					firstNumbNotSumOfPrevNumbs = currNumForChecking;
					break;
				}
			}
		}

		return firstNumbNotSumOfPrevNumbs;
	}

	__declspec(dllexport) double FindSumOfSmallestAndLargestNumbsInContiguousRange(double* arrayInputPuzzle, int numArrayInput, double contiguousSumVal)
	{
		double sumOfSmallestLargest = -1;

		bool isSummationFound = false;
		for (int i = 0; i < numArrayInput; i++)						// Step 1. Iterate every value in Input Puzzle as potential start of the Contiguous Range
		{
			double currContiguousRngSumVal = arrayInputPuzzle[i];
			double smallestNumb = arrayInputPuzzle[i];
			double largestNumb = arrayInputPuzzle[i];

			for (int ii = (i + 1); ii < numArrayInput; ii++)		// Step 2. Iterate ahead of each potential start of the Contiguous Range for that Range's summation 
			{
				currContiguousRngSumVal += arrayInputPuzzle[ii];

				if (currContiguousRngSumVal < contiguousSumVal)		// Ultimate State Machine
				{
					if (arrayInputPuzzle[ii] < smallestNumb)
						smallestNumb = arrayInputPuzzle[ii];
					else if (arrayInputPuzzle[ii] > largestNumb)
						largestNumb = arrayInputPuzzle[ii];
					continue;
				}
				else if (currContiguousRngSumVal == contiguousSumVal)
				{
					if (arrayInputPuzzle[ii] < smallestNumb)
						smallestNumb = arrayInputPuzzle[ii];
					else if (arrayInputPuzzle[ii] > largestNumb)
						largestNumb = arrayInputPuzzle[ii];
					isSummationFound = true;
					break;
				}
				else
					break;
			}
			if (isSummationFound)
			{
				sumOfSmallestLargest = smallestNumb + largestNumb;
				break;
			}
		}

		return sumOfSmallestLargest;
	}
}

bool Helper_IsDifferenceFoundInPreamble(double currNumForChecking, std::deque<double> preamble)
{
	std::deque<double> modifiedPreamble = preamble;
	bool isDifferenceFoundInPreamble = false;
	for (int i = 0; i < preamble.size(); i++)						// Across Set {35,20,15,25,47}: numSearchedFromPreamble = 40 - (each value in set)
	{
		double numSearchedFromPreamble = currNumForChecking - preamble[i];
		modifiedPreamble.pop_front();
		for (int ii = 0; ii < modifiedPreamble.size(); ii++)		// Look for numSearchedFromPreamble = 5 in {20,15,25,47}
		{
			if (modifiedPreamble[ii] == numSearchedFromPreamble)
			{
				isDifferenceFoundInPreamble = true;
				break;
			}
		}
		if (isDifferenceFoundInPreamble)
			break;
	}
	return isDifferenceFoundInPreamble;								// Return false only when entire search thru preamble cannot find 2 numbs that sum to numSearchedFromPreamble
}