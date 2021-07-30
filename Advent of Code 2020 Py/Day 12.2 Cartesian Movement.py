# DAY 12.2 EXAMPLE
# ================
# The instructions now refer to a waypoint relative to the ship's position.
# The waypoint starts 10 units east and 1 unit north relative to the ship.
# The ship starts at (0, 0).
# 
# Action N means to move the waypoint north by the given value.
# Action S means to move the waypoint south by the given value.
# Action E means to move the waypoint east by the given value.
# Action W means to move the waypoint west by the given value.
# Action L means to rotate the waypoint around the ship left in degrees by the given value.
# Action R means to rotate the waypoint around the ship right in degrees by the given value.
# Action F means to move forward to the waypoint a number of times equal to the given value.
# 
# F10
# N3
# F7
# R90
# F11
#
# F10 would move the ship to the waypoint 10 times (100 units east, 10 units north).
# --- The ship is at east 100, north 10.
# N3 would move the waypoint 3 units north to east 10, north 4.
# F7 would move the ship to the waypoint 7 times (70 units east, 28 units north).
# --- The ship is at east 170, north 38. The waypoint stays at east 10, north 4.
# R90 rotates the waypoint around the ship 90 degrees right to 4 units east, 10 units south.
# --- The ship is still at east 170, north 38.
# F11 would move the ship to the waypoint 11 times (44 units east, 110 units south).
# --- The ship is at east 214, south 72.
# --- The waypoint stays 4 units east, 10 units south of the ship.
#
# The Manhattan distance is the sum of the absolute values of its east/west/north/south positions.
# 
# DAY 12.2
# ========
# (Q): What is the Manhattan distance between the ship's ending location and its starting position?
# (A): The Manhattan distance is 214 + 72 = 286.

# Defining Global Variables
shipEastPos = 0
shipNorthPos = 0
wayPtEastPos = 10
wayPtNorthPos = 1
currDir = 0         # Cartesian quadrant of the waypoint relative to the ship where
dictDir = {0:[1,1], 90:[-1,1], 180:[-1,-1], 270:[1,-1]}
                    # [Quad:  x,  y]
                    # [0   :+ve,+ve]
                    # [90  :-ve,+ve]
                    # [180 :-ve,-ve]
                    # [270 :+ve,-ve]

def rotateWayPoint(currDir, value, old_WayPtEastPos, old_WayPtNorthPos):
    global wayPtEastPos
    global wayPtNorthPos
    
    if value == 90 or value == 270:
        wayPtEastPos = old_WayPtNorthPos
        wayPtNorthPos = old_WayPtEastPos
        
    # The (x,y) magnitudes do not change; only their signs for 180d rotations found in dictDir
    wayPtEastPos = abs(wayPtEastPos) * dictDir[currDir][0]
    wayPtNorthPos = abs(wayPtNorthPos) * dictDir[currDir][1]

def moveWayPoint(command, value):
    global wayPtEastPos
    global wayPtNorthPos
    
    if command == 'N':
        wayPtNorthPos += value
    elif command == 'S':
        wayPtNorthPos -= value
    elif command == 'E':
        wayPtEastPos += value
    elif command == 'W':
        wayPtEastPos -= value

def updateCurrDir(new_WayPtEastPos, new_WayPtNorthPos):
    global currDir

    if new_WayPtEastPos > 0 and new_WayPtNorthPos >= 0:
        currDir = 0
    elif new_WayPtEastPos <= 0 and new_WayPtNorthPos > 0:
        currDir = 90
    elif new_WayPtEastPos < 0 and new_WayPtNorthPos <= 0:
        currDir = 180
    elif new_WayPtEastPos >= 0 and new_WayPtNorthPos < 0:
        currDir = 270


# ~ MAIN ~
# --------
instructions = listInputPuzzle

for instruction in instructions:
    command = instruction[0]
    value = int(instruction[1:])
    
    if command == 'L':
        currDir = (currDir + value) % 360
        rotateWayPoint(currDir, value, wayPtEastPos, wayPtNorthPos)
    elif command == 'R':
        currDir = (currDir - value) % 360
        rotateWayPoint(currDir, value, wayPtEastPos, wayPtNorthPos)
    elif command == 'F':
        shipEastPos = shipEastPos + (wayPtEastPos * value)      # Ship command
        shipNorthPos = shipNorthPos + (wayPtNorthPos * value)
    else:
        moveWayPoint(command, value)                            # Waypoint command: 'N', 'S', 'E', 'W'
        updateCurrDir(wayPtEastPos, wayPtNorthPos)              # To account for waypoint drifting into new quadrant

manhattanCoord = abs(shipEastPos) + abs(shipNorthPos)
