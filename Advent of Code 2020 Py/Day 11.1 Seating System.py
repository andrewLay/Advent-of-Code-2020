# DAY 11.1 EXAMPLE
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
# If a seat is empty (L) and there are no occupied seats adjacent to it, the seat becomes occupied.
# If a seat is occupied (#) and four or more seats adjacent to it are also occupied, the seat becomes empty.
# Otherwise, the seat's state does not change.
#
# Simulate your seating area by applying the seating rules repeatedly until no seats change state.
# 
# #.#L.L#.##
# #LLL#LL.L#
# L.#.L..#..
# #L##.##.L#
# #.#L.LL.LL
# #.#L#L#.##
# ..L.L.....
# #L#L##L#L#
# #.LLLLLL.L
# #.#L#L#.##
#
# DAY 11.1
# ========
# (Q): How many seats end up occupied?
# (A): Once people stop moving around, you count 37 occupied seats.

# Defining Global Variables
finalNumOccSeats = 0
prevNumOccSeats = 0

def checkOccupiedSpace(char):
    if (char == '#'):
        return 1
    else:
        return 0

def countAdjacentSpaces(listOfLines, lineIndex, charIndex):
    countNumOccAdjSeats = 0
    countNumOccAdjSeats += checkOccupiedSpace(listOfLines[lineIndex - 1][charIndex - 1])
    countNumOccAdjSeats += checkOccupiedSpace(listOfLines[lineIndex - 1][charIndex])
    countNumOccAdjSeats += checkOccupiedSpace(listOfLines[lineIndex - 1][charIndex + 1])
    countNumOccAdjSeats += checkOccupiedSpace(listOfLines[lineIndex][charIndex - 1])
    countNumOccAdjSeats += checkOccupiedSpace(listOfLines[lineIndex][charIndex + 1])
    countNumOccAdjSeats += checkOccupiedSpace(listOfLines[lineIndex + 1][charIndex - 1])
    countNumOccAdjSeats += checkOccupiedSpace(listOfLines[lineIndex + 1][charIndex])
    countNumOccAdjSeats += checkOccupiedSpace(listOfLines[lineIndex + 1][charIndex + 1])
    return countNumOccAdjSeats

def applySeatingRules(listOfLines):
    global finalNumOccSeats
    global prevNumOccSeats
    prevNumOccSeats = finalNumOccSeats
    nextListOfLines = listOfLines.copy()
    
    for lineIndex, line in enumerate(listOfLines):    
        for charIndex, char in enumerate(line):
            # Check adjacent seats for occupation
            if (char == 'L' or char == '#'):
                countNumOccAdjSeats = countAdjacentSpaces(listOfLines, lineIndex, charIndex)
                if (char == 'L' and countNumOccAdjSeats == 0):
                    line = line[:charIndex] + '#' + line[charIndex + 1:]
                    nextListOfLines[lineIndex] = line
                    finalNumOccSeats += 1
                if (char == '#' and countNumOccAdjSeats >= 4):
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