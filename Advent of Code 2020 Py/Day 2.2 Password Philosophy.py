# Defining Global Variables
minPosition = 0
maxPosition = 0
letter = 0
password = 0
numValidPword = 0

# Define Python Functions at Top to be called later on due to Top-Down Execution of Script
def splitterFunc(input, delimiter):
    retValue = input.split(delimiter)
    return retValue

def splitEachLine(rawLine):
    global minPosition                                          # Explicit 'global' keyword needed to target Global Variables
    global maxPosition
    global letter
    global password
    noColonLine = rawLine.replace(":", "")                      # Remove the colon
    noSpaceLine = splitterFunc(noColonLine, " ")                # Split blankspace character; split() does this by default
    noDash = splitterFunc(noSpaceLine[0], "-")                  # Split on '9-12'
    minPosition = int(noDash[0])
    maxPosition = int(noDash[1]) 
    letter = noSpaceLine[1]
    password = noSpaceLine[2]

def determineValidPassword(password):
    global numValidPword                                        # No need 'global' keyword to read Global Variables
    lowerExist = (password[minPosition - 1] == letter)
    upperExist = (password[maxPosition - 1] == letter)
    if (lowerExist and not upperExist) or (upperExist and not lowerExist):
        numValidPword+=1

for rawLine in listInputPuzzle:
    splitEachLine(rawLine)
    determineValidPassword(password)