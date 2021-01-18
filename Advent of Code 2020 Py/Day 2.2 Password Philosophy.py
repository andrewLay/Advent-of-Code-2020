# DAY 2.2 EXAMPLE
# ===============
# 1-3 a: abcde is valid: position 1 contains a and position 3 does not.
# 1-3 b: cdefg is invalid: neither position 1 nor position 3 contains b.
# 2-9 c: ccccccccc is invalid: both position 2 and position 9 contain c.
#
# DAY 2.2
# =======
# (Q): Each policy actually describes two positions in the password, where 1 means the first character, 2 means the second character. How many passwords are valid?
# (A): Therefore, only 1 password is valid.

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