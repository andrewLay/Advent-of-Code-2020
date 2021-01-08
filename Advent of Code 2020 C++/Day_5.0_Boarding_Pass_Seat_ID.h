#pragma once											// '#pragma once' used as preprocessor directive for file to be used only once in a single compilation

#ifdef BOARDINGPASS_SEATID_EXPORTS						// Explicit MACRO defn optimizes the import of functions by client apps by informing Linker and Compiler
#define BOARDINGPASS_SEATID_API __declspec(dllexport)
#else
#define BOARDINGPASS_SEATID_API __declspec(dllimport)
#endif

														// .h declares all functions whose implementations are found in .cpp

extern "C" BOARDINGPASS_SEATID_API int FindHighestSeatID(char** arrayInputPuzzle, int numArrayInputs);
extern "C" BOARDINGPASS_SEATID_API int FindMySeatID(char** arrayInputPuzzle, int numArrayInputs);

enum class Function { FindHighest, FindMine };
int PopulateSeatIDs(char** arrayInputPuzzle, int numArrayInputs, Function function);
int* SortArrayElements(int arrayInput[], int numArrayInputs);
int Helper_FindNextSmallestIndex(int seatIDs[], int nextSmallestIndex, int numArrayInputs);
int SearchForMySeat(int seatIDs[], int numArrayInputs);