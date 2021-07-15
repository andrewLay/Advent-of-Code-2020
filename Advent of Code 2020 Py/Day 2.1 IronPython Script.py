# DAY 2.1 EXAMPLE
# ===============
# 1-3 a: abcde
# 1-3 b: cdefg
# 2-9 c: ccccccccc
# 
# DAY 2.1
# =======
# (Q): If (1-3 a) means that the password must contain (a) at least 1 time and at most 3 times, how many passwords are valid?
# (A): Therefore, 2 passwords are valid.

# Defining Global Variables
minCount = 0
maxCount = 0
letter = 0
password = 0
numValidPword = 0

# Define Python Functions at Top to be called later on due to Top-Down Execution of Script
def splitterFunc(input, delimiter):
    retValue = input.split(delimiter)
    return retValue

def splitEachLine(rawLine):
    global minCount                                             # Explicit 'global' keyword needed to target Global Variables
    global maxCount
    global letter
    global password
    noColonLine = rawLine.replace(":", "")                      # Remove the colon
    noSpaceLine = splitterFunc(noColonLine, " ")                # Split blankspace character; split() does this by default
    noDash = splitterFunc(noSpaceLine[0], "-")                  # Split on '9-12'
    minCount = int(noDash[0])
    maxCount = int(noDash[1]) 
    letter = noSpaceLine[1]
    password = noSpaceLine[2]

def determineValidPassword(password):
    global numValidPword                                        # No need 'global' keyword to read Global Variables
    counter = 0
    for char in password:
        if char == letter:
            counter+=1
    if (counter >= minCount) and (counter <= maxCount):
        numValidPword+=1


# ~ MAIN ~
# --------
for rawLine in listInputPuzzle:
    splitEachLine(rawLine)
    determineValidPassword(password)