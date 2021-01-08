import re                                                       # Library used to split on multiple characters 

# Defining Global Variables
validFields = ["byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid"]
multipleDelimit = " " + "|" + "\n"                              # re.split() uses '|' to clause each delimitter
numValidPassport = 0  
onePassport = ""

def splitterFunc(input, multipleDelimit):
    retValue = re.split(multipleDelimit, input)
    return retValue

def splitEachPassport(input):
    input = input.strip()                                       # strip() removes leading and lagging blankspaces
    passportEntity = splitterFunc(input, multipleDelimit)
    return passportEntity

def determineValidPassport(passportEntity):
    global numValidPassport
    numValidFields = 0
    if (len(passportEntity) == 8 or len(passportEntity) == 7):  # 'cid' field is unnecessary for passport qualification
        for field in passportEntity:
            fieldName = field.split(":")[0]
            for validField in validFields:
                if (fieldName == validField):
                    numValidFields += 1
            if (numValidFields == 7):
                numValidPassport += 1
                numValidFields = 0

for rawLine in listInputPuzzle:
    if (rawLine != ""):
        onePassport += (" " + rawLine)                          # Form a password entity from multi-lines
        continue
    passportEntity = splitEachPassport(onePassport)
    determineValidPassport(passportEntity)
    onePassport = ""

passportEntity = splitEachPassport(onePassport)                 # Account for last line in batch List<string>
determineValidPassport(passportEntity)
onePassport = ""
