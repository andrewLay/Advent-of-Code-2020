# DAY 8.2 EXAMPLE
# ===============
# Instructions are visited in this order:
# 
# nop +0  | 1
# acc +1  | 2
# jmp +4  | 3
# acc +3  |
# jmp -3  |
# acc -99 |
# acc +1  | 4
# nop -4  | 5       (used to be jmp -4, which would loop infinitely)
# acc +6  | 6
#
# nop - instruc does nothing; instruc immediately below executed next.
# acc - incr or decr a global var by the value of param passed with acc.
# jmp - jumps to a new instruc line relative to itself.
# 
# Exactly one instruction is corrupted.
# Either a jmp is supposed to be a nop, or a nop is supposed to be a jmp.
# The program should terminate after executing the last instruction in the file.
#
# DAY 8.2
# =======
# (Q): What is the value of the accumulator after the program terminates?
# (A): Therefore, the value in the accumulator is 8.

import copy

# Defining Global Variables
linePointer = 0
linePointerList = []
glbAccValue = 0
instrLine = None
instrCmd = None
instrParam = None
instrTypeDict = {"nop" : "jmp", "jmp" : "nop"}
isGameCodeFullyExecuted = False

def resetGlobalVariables():
    global linePointer
    global linePointerList
    global glbAccValue
    global instrLine
    global instrCmd
    global instrParam
    linePointer = 0
    linePointerList = []
    glbAccValue = 0
    instrLine = None
    instrCmd = None
    instrParam = None

def extractInstructionParts(modifiedPuzzle):
    global instrLine
    global instrCmd
    global instrParam
    instrLine = modifiedPuzzle[linePointer].split(" ")
    instrCmd = instrLine[0]
    instrParam = instrLine[1]

def executeGameCode(modifiedPuzzle):                            # Main function used to check if Infinite Looping occurs for this iter of Modified Puzzle
    global linePointer
    global linePointerList
    global glbAccValue
    global instrCmd
    global instrParam
    while (linePointer != len(modifiedPuzzle)):
        extractInstructionParts(modifiedPuzzle)
        linePointerList.append(linePointer)
        if (instrCmd == "nop"): 
            linePointer += 1
        elif (instrCmd == "acc"):
            if (instrParam[0] == "+"):
                glbAccValue += int(instrParam[1:])
            elif (instrParam[0] == "-"):
                glbAccValue -= int(instrParam[1:])
            linePointer += 1
        elif (instrCmd == "jmp"):
            if (instrParam[0] == "+"):
                linePointer += int(instrParam[1:])
            elif (instrParam[0] == "-"):
                linePointer -= int(instrParam[1:])
        if (linePointer in linePointerList):                    # Looping occurs when a previous instruc line value is revisited by the linePointer
            return False                                        # Return TRUE only when listInputPuzzle's final instruc executes and program terminates
    return True

modifiedPuzzle = copy.deepcopy(listInputPuzzle)                 # Module copy.deepcopy() duplicates List structure with Values whereas copy.copy() is shallow
for instrType in instrTypeDict.keys():
    for index, rawLine in enumerate(listInputPuzzle):           # Use enumerate() to have access to both the (string) and (index of string) in List[string]
        if (rawLine[0:3] == instrType):                         # rawline[0:3] --> "nop" or "jmp"
            resetGlobalVariables()
            replaceInstrType = instrTypeDict[instrType]         # Dict{"nop" : "jmp", "jmp" : "nop"} used as Look-Up for switching the corrupted instruction
            modifiedPuzzle[index] = listInputPuzzle[index].replace(instrType, replaceInstrType)
            isGameCodeFullyExecuted = executeGameCode(modifiedPuzzle)
            if (isGameCodeFullyExecuted):
                break
            modifiedPuzzle = copy.deepcopy(listInputPuzzle)
    if (isGameCodeFullyExecuted):                               # Use flag 'isGameCodeFullyExecuted' to also break out of Outer Loop
        break