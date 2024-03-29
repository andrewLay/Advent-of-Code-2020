# DAY 4.2 EXAMPLE
# ===============
# byr (Birth Year)      - four digits; at least 1920 and at most 2002.
# iyr (Issue Year)      - four digits; at least 2010 and at most 2020.
# eyr (Expiration Year) - four digits; at least 2020 and at most 2030.
# hgt (Height)          - a number followed by either cm or in:
#                         If cm, the number must be at least 150 and at most 193.
#                         If in, the number must be at least 59 and at most 76.
# hcl (Hair Color)      - a # followed by exactly six characters 0-9 or a-f.
# ecl (Eye Color)       - exactly one of: amb blu brn gry grn hzl oth.
# pid (Passport ID)     - a nine-digit number, including leading zeroes.
# cid (Country ID)      - ignored, missing or not.
# 
# [invalid]
# eyr:1972 cid:100
# hcl:#18171d ecl:amb hgt:170 pid:186cm iyr:2018 byr:1926
# 
# [invalid]
# iyr:2019
# hcl:#602927 eyr:1967 hgt:170cm
# ecl:grn pid:012533040 byr:1946
# 
# [valid]
# hcl:#888785
# hgt:164cm byr:2001 iyr:2015 cid:88
# pid:545766238 ecl:hzl
# eyr:2022
# 
# [valid]
# iyr:2010 hgt:158cm hcl:#b6652a ecl:blu byr:1944 eyr:2021 pid:093154719
# iyr:2011 ecl:brn hgt:59in
# 
# DAY 4.2
# =======
# (Q): Count the number of valid passports - those that have all required fields and valid values. Treat cid as optional. In your batch file, how many passports are valid?
# (A): Therefore, the improved system would report 2 valid passports.

import re                                                       # Library used to split on multiple characters 

# Defining Global Variables
validFields = ["byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid"]
multipleDelimit = " " + "|" + "\n"                              # re.split() uses '|' to clause each delimitter
numValidPassport = 0  
onePassport = ""

def splitEachPassport(input):
    input = input.strip()                                       # strip() removes leading and lagging blankspaces
    passportEntity = re.split(multipleDelimit, input)
    return passportEntity

def fnIsByrValid(input):
    intInput = int(input)
    if (intInput >= 1920 and intInput <= 2002):
        return True
    return False

def fnIsIyrValid(input):
    intInput = int(input)
    if (intInput >= 2010 and intInput <= 2020):
        return True
    return False

def fnIsEyrValid(input):
    intInput = int(input)
    if (intInput >= 2020 and intInput <= 2030):
        return True
    return False

def fnIsHgtValid(input):
    units = input[-2:]
    if (units in ["cm", "in"]):
        intInput = int(input[:(len(input) - 2)])                    # slicer[<0-based> : <num of chars>]
        if (units == "cm"):
            if (intInput >= 150 and intInput <= 193):
                return True
        elif (units == "in"):
            if (intInput >= 59 and intInput <= 76):
                return True
    return False

def fnIsHclValid(input):
    if (input[0] == "#" and len(input) == 7):
        pattern = re.compile("[0-9a-f]+")
        if pattern.match(input[1:]) is not None:            # Matching string against character pattern
            return True
    return False

def fnIsEclValid(input):
    validHcl = ["amb", "blu", "brn", "gry", "grn", "hzl", "oth"]
    if (input in validHcl):
        return True
    return False

def fnIsPidValid(input):
    if (len(input) == 9):
        pattern = re.compile("[0-9]+")
        if pattern.match(input) is not None:
            return True
    return False      

def fnIsFieldValueValid(passportEntity):
    passportDictionary = {}                                     # Dictionary used to store key:value pairs
    for field in passportEntity:
        fieldName = field.split(":")[0]
        fieldValue = field.split(":")[1]
        passportDictionary.update({fieldName : fieldValue})     # Dict.update( {new KEY : new VALUE} )
    isByrValid = fnIsByrValid(passportDictionary[validFields[0]])
    isIyrValid = fnIsIyrValid(passportDictionary[validFields[1]])
    isEyrValid = fnIsEyrValid(passportDictionary[validFields[2]])
    isHgtValid = fnIsHgtValid(passportDictionary[validFields[3]])
    isHclValid = fnIsHclValid(passportDictionary[validFields[4]])
    isEclValid = fnIsEclValid(passportDictionary[validFields[5]])
    isPidValid = fnIsPidValid(passportDictionary[validFields[6]])
    if (isByrValid and isIyrValid and isEyrValid and isHgtValid and isHclValid and isEclValid and isPidValid):
        return True
    return False

def fnIsFieldNameValid(passportEntity):
    numValidFields = 0
    for field in passportEntity:
        fieldName = field.split(":")[0]
        for validField in validFields:
            if (fieldName == validField):
                numValidFields += 1
        if (numValidFields == 7):
            return True
    return False

def determineValidPassport(passportEntity):
    global numValidPassport
    if (len(passportEntity) == 8 or len(passportEntity) == 7):  # 'cid' field is unnecessary for passport qualification
        isFieldNameValid = fnIsFieldNameValid(passportEntity)
        if (isFieldNameValid):
            isFieldValueValid = fnIsFieldValueValid(passportEntity)
        if (isFieldNameValid and isFieldValueValid):
            numValidPassport += 1


# ~ MAIN ~
# --------
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