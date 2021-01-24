# DAY 8.1 EXAMPLE
# ===============
# Instructions are visited in this order:
# 
# nop +0  | 1
# acc +1  | 2, 8(!)
# jmp +4  | 3
# acc +3  | 6
# jmp -3  | 7
# acc -99 |
# acc +1  | 4
# jmp -4  | 5
# acc +6  |
#
# nop - instruc does nothing; instruc immediately below executed next.
# acc - incr or decr a global var by the value of param passed with acc.
# jmp - jumps to a new instruc line relative to itself.
#
# DAY 8.1
# =======
# (Q): Immediately before any instruction is executed a second time, what value is in the accumulator?
# (A): Therefore, the value in the accumulator is 5.

# Defining Global Variables
linePointer = 0
linePointerList = []
glbAccValue = 0
instrLine = None
instrCmd = None
instrParam = None

def extractInstructionParts():
    global instrLine
    global instrCmd
    global instrParam
    instrLine = listInputPuzzle[linePointer].split(" ")
    instrCmd = instrLine[0]
    instrParam = instrLine[1]

while (linePointer != len(listInputPuzzle)):
    extractInstructionParts()
    linePointerList.append(linePointer)
    if (instrCmd == "nop"):                                 # Use of a State Machine implemented as a series of if-statements 
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
        break