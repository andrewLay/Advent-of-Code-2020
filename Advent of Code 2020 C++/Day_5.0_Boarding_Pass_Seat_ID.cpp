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