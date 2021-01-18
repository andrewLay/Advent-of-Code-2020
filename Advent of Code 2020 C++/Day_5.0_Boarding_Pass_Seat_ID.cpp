/*
DAY 5.0 EXAMPLE
===============
Boarding Pass: FBFBBFFRLR

Start by considering the whole range, rows 0 through 127.
F means to take the lower half, keeping rows 0 through 63.
B means to take the upper half, keeping rows 32 through 63.
F means to take the lower half, keeping rows 32 through 47.
B means to take the upper half, keeping rows 40 through 47.
B keeps rows 44 through 47.
F keeps rows 44 through 45.
The final F keeps the lower of the two, row 44.

Start by considering the whole range, columns 0 through 7.
R means to take the upper half, keeping columns 4 through 7.
L means to take the lower half, keeping columns 4 through 5.
The final R keeps the upper of the two, column 5.

Every seat also has a unique seat ID: multiply the row by 8, then add the column. In this example, the seat has ID (44 * 8 + 5) = 357.

Here are some other boarding passes:
BFFFBBFRRR: row 70, column 7, seat ID 567.
FFFBBBFRRR: row 14, column 7, seat ID 119.
BBFFBBFRLL: row 102, column 4, seat ID 820.

DAY 5.1
=======
(Q): What is the highest seat ID on a boarding pass?
(A): The highest seat ID on a boarding pass is 820.

DAY 5.2
=======
(Q): Your seat wasn't at the very front or back, though; the seats with IDs +1 and -1 from yours will be in your list. What is the ID of your seat?
(A): Given all Seat IDs are computed and stored in memory, use Selection Sort to order IDs in ascending order and iterate thru for missing Seat ID.
 */

#include "pch.h"
#include "Day_5.0_Boarding_Pass_Seat_ID.h"
#include <cmath>

extern "C" {
	__declspec(dllexport) int FindHighestSeatID(char** arrayInputPuzzle, int numArrayInputs)
	{
		return PopulateSeatIDs(arrayInputPuzzle, numArrayInputs, Function::FindHighest);
	}

	__declspec(dllexport) int FindMySeatID(char** arrayInputPuzzle, int numArrayInputs)
	{

		return PopulateSeatIDs(arrayInputPuzzle, numArrayInputs, Function::FindMine);
	}
}

int PopulateSeatIDs(char** arrayInputPuzzle, int numArrayInputs, Function function)
{
	int highestSeatID = 0;
	int* seatIDs = new int[numArrayInputs];			// How to declare an array of variable size in Heap (runtime memory)
	float currRowUpper = 127;						// 'int' as opposed to 'int*' is auto memory-managed in Stack (memory for pointers/functions)
	float currRowLower = 0;
	float currColUpper = 7;
	float currColLower = 0;
	for (int i = 0; i < numArrayInputs; i++)
	{
		char* rawLine = arrayInputPuzzle[i];
		for (int j = 0; j < strlen(rawLine); j++)
		{
			switch (rawLine[j])
			{
			case 'F':
				currRowUpper = floor((currRowLower + currRowUpper) / 2);
				break;
			case 'B':
				currRowLower = ceil((currRowLower + currRowUpper) / 2);
				break;
			case 'L':
				currColUpper = floor((currColLower + currColUpper) / 2);
				break;
			case 'R':
				currColLower = ceil((currColLower + currColUpper) / 2);
				break;
			}
		}
		if (currRowUpper == currRowLower && currColUpper == currColLower)
		{
			seatIDs[i] = currRowUpper * 8 + currColUpper;
			if (function == Function::FindHighest)
			{
				if (seatIDs[i] > highestSeatID)
				{
					highestSeatID = seatIDs[i];
				}
			}
			currRowUpper = 127;
			currRowLower = 0;
			currColUpper = 7;
			currColLower = 0;
		}
		else
		{
			delete rawLine;
			rawLine = NULL;
			delete[] seatIDs;						// Delete[] array pointers to free memory in Heap (only use Delete or Delete[] for 'new' Type)
			seatIDs = NULL;
			return -1;
		}
	}
	if (function == Function::FindHighest)
	{
		delete[] seatIDs;
		seatIDs = NULL;
		return highestSeatID;
	}
	else if (function == Function::FindMine)
	{
		seatIDs = SortArrayElements(seatIDs, numArrayInputs);
		highestSeatID = SearchForMySeat(seatIDs, numArrayInputs);
		delete[] seatIDs;
		seatIDs = NULL;
		return highestSeatID;
	}
}

int* SortArrayElements(int seatIDs[], int numArrayInputs)	// Sorting via Selection Sort
{
	int pos, temp;
	for (int i = 0; i < numArrayInputs; i++)
	{
		pos = Helper_FindNextSmallestIndex(seatIDs, i, numArrayInputs);
		temp = seatIDs[i];
		seatIDs[i] = seatIDs[pos];
		seatIDs[pos] = temp;
	}
	return seatIDs;
}

int Helper_FindNextSmallestIndex(int seatIDs[], int nextSmallestIndex, int numArrayInputs)
{
	for (int j = nextSmallestIndex + 1; j < numArrayInputs; j++)
	{
		if (seatIDs[j] < seatIDs[nextSmallestIndex])
		{
			nextSmallestIndex = j;
		}
	}
	return nextSmallestIndex;
}

int SearchForMySeat(int seatIDs[], int numArrayInputs)
{
	for (int i = 0; i < (numArrayInputs - 2); i++)
	{
		int currIndexVal = seatIDs[i];
		int nextIndexVal = seatIDs[i + 1];
		int futrIndexVal = seatIDs[i + 2];
		if ((currIndexVal + 1 != nextIndexVal) && (futrIndexVal - 2 != nextIndexVal))
		{
			return currIndexVal + 1;
		}
	}
	return -1;
}