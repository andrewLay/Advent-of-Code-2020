# DAY 11.2 EXAMPLE
# ================
# The seat layout fits neatly on a grid.
# Each position is either floor (.), an empty seat (L), or an occupied seat (#).
# 
# L.LL.LL.LL
# LLLLLLL.LL
# L.L.L..L..
# LLLL.LL.LL
# L.LL.LL.LL
# L.LLLLL.LL
# ..L.L.....
# LLLLLLLLLL
# L.LLLLLL.L
# L.LLLLL.LL
#
# If a seat is empty (L) and the first seat in each of the eight (8) directions is also empty (called visible seats),
# the seat becomes occupied.
# If a seat is occupied (#) and five (5) or more visible seats are also occupied, the seat becomes empty.
# Otherwise, the seat's state does not change.
#
# Simulate your seating area by applying the seating rules repeatedly until no seats change state.
# 
# #.L#.L#.L#
# #LLLLLL.LL
# L.L.L..#..
# ##L#.#L.L#
# L.L#.LL.L#
# #.LLLL#.LL
# ..#.L.....
# LLL###LLL#
# #.LLLLL#.L
# #.L#LL#.L#
#
# DAY 11.2
# ========
# (Q): Given the new visibility method, how many seats end up occupied?
# (A): Once people stop moving around, you count 26 occupied seats.

# Defining Global Variables
finalNumOccSeats = 0
prevNumOccSeats = 0
strExecCodePrt1 = "listOfLines"
listOfStrExecCodePrt2 = ["[currLineIndex - 1][currCharIndex - 1]",
                         "[currLineIndex - 1][currCharIndex]",
                         "[currLineIndex - 1][currCharIndex + 1]",
                         "[currLineIndex][currCharIndex - 1]",
                         "[currLineIndex][currCharIndex + 1]",
                         "[currLineIndex + 1][currCharIndex - 1]",
                         "[currLineIndex + 1][currCharIndex]",
                         "[currLineIndex + 1][currCharIndex + 1]"]

def countVisibleSpaces(listOfLines, lineIndex, charIndex):
    numOccVisibleSeats = 0
    for strExecCodePrt2 in listOfStrExecCodePrt2:
        execCode = strExecCodePrt1 + strExecCodePrt2
        currLineIndex = lineIndex
        currCharIndex = charIndex
        while (True):
            # ENUMERATE: [# -> occupied] , [L -> unoccupied] , [0 -> edge of grid] , [. -> don't care but update indices]
            # eval() returns the evaluation of the expression stored as a string that gets treated like an executable object of code
            # exec() always returns None
            char = eval(execCode)
            if (char == '#'):
                numOccVisibleSeats += 1
                break
            elif (char == 'L' or char == '0'):
                break
            else:
                if ("[currLineIndex - 1]" in strExecCodePrt2):
                    currLineIndex -= 1
                if ("[currLineIndex + 1]" in strExecCodePrt2):
                    currLineIndex += 1
                if ("[currCharIndex - 1]" in strExecCodePrt2):
                    currCharIndex -= 1
                if ("[currCharIndex + 1]" in strExecCodePrt2):
                    currCharIndex += 1
                continue    
    return numOccVisibleSeats

def applySeatingRules(listOfLines):
    global finalNumOccSeats
    global prevNumOccSeats
    prevNumOccSeats = finalNumOccSeats
    nextListOfLines = listOfLines.copy()
    
    for lineIndex, line in enumerate(listOfLines):    
        for charIndex, char in enumerate(line):
            # Check visible seats for occupation
            if (char == 'L' or char == '#'):
                numOccVisibleSeats = countVisibleSpaces(listOfLines, lineIndex, charIndex)
                if (char == 'L' and numOccVisibleSeats == 0):
                    line = line[:charIndex] + '#' + line[charIndex + 1:]
                    nextListOfLines[lineIndex] = line
                    finalNumOccSeats += 1
                if (char == '#' and numOccVisibleSeats >= 5):
                    line = line[:charIndex] + 'L' + line[charIndex + 1:]
                    nextListOfLines[lineIndex] = line
                    finalNumOccSeats -= 1
    return nextListOfLines


# ~ MAIN ~
# --------
listOfLines = listInputPuzzle

# Pad two outer rows with '0' entries to simplify logic about accessing index-out-of-range exception 
padRow = ""
for i in range(0, len(listOfLines[0])):
    padRow += "0"
listOfLines = [padRow] + listOfLines + [padRow]

# Pad two outer columns with '0' entries as well
for lineIndex, line in enumerate(listOfLines):
    line = '0' + line + '0'
    listOfLines[lineIndex] = line

nextIterListOfLines = applySeatingRules(listOfLines)
while (finalNumOccSeats != prevNumOccSeats):
    nextIterListOfLines = applySeatingRules(nextIterListOfLines)
