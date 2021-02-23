#pragma once											// '#pragma once' used as preprocessor directive for file to be used only once in a single compilation

#ifdef ENCODING_ERROR_EXPORTS							// Explicit MACRO defn optimizes the import of functions by client apps by informing Linker and Compiler
#define ENCODING_ERROR_API __declspec(dllexport)
#else
#define ENCODING_ERROR_API __declspec(dllimport)
#endif

														// .h declares all functions whose implementations are found in .cpp
#include <deque>
extern "C" ENCODING_ERROR_API double FindFirstNumbNotSumOfPrevNumbs(double* arrayInputPuzzle, int numArrayInputs, int preambleSize);
extern "C" ENCODING_ERROR_API double FindSumOfSmallestAndLargestNumbsInContiguousRange(double* arrayInputPuzzle, int numArrayInput, double contiguousSumVal);
bool Helper_IsDifferenceFoundInPreamble(double searchFromPreamble, std::deque<double> preamble);