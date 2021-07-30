# DAY 12.1 EXAMPLE
# ================
# The ship starts by facing east.
# Only the L and R actions change the direction the ship is facing.
# 
# Action N means to move north by the given value.
# Action S means to move south by the given value.
# Action E means to move east by the given value.
# Action W means to move west by the given value.
# Action L means to turn left the given number of degrees.
# Action R means to turn right the given number of degrees.
# Action F means to move forward by the given value in the direction the ship is currently facing.
# 
# F10
# N3
# F7
# R90
# F11
#
# F10 would move the ship 10 units east to east 10, north 0.
# N3 would move the ship 3 units north to east 10, north 3.
# F7 would move the ship another 7 units east to east 17, north 3.
# R90 would cause the ship to turn right by 90 degrees and face south; remaining at east 17, north 3.
# F11 would move the ship 11 units south to east 17, south 8.
#
# The Manhattan distance is the sum of the absolute values of its east/west/north/south positions.
# 
# DAY 12.1
# ========
# (Q): What is the Manhattan distance between the ship's ending location and its starting position?
# (A): The Manhattan distance is 17 + 8 = 25.

# Defining Global Variables
eastPos = 0
northPos = 0
currDir = 0
dictDir = {0:'E', 90:'N', 180:'W', 270:'S'}

def moveShip(command, value):
    global eastPos
    global northPos
    if command == 'N':
        northPos += value
    elif command == 'S':
        northPos -= value
    elif command == 'E':
        eastPos += value
    elif command == 'W':
        eastPos -= value


# ~ MAIN ~
# --------
instructions = listInputPuzzle

for instruction in instructions:
    command = instruction[0]
    value = int(instruction[1:])
    
    if command == 'L':
        currDir = (currDir + value) % 360
    elif command == 'R':
        currDir = (currDir - value) % 360
    elif command == 'F':
        moveShip(dictDir[currDir], value)
    else:
        moveShip(command, value)                # command: 'N', 'S', 'E', 'W'

manhattanCoord = abs(eastPos) + abs(northPos)
