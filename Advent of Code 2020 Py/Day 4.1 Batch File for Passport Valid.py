# DAY 4.1 EXAMPLE
# ===============
# byr (Birth Year)
# iyr (Issue Year)
# eyr (Expiration Year)
# hgt (Height)
# hcl (Hair Color)
# ecl (Eye Color)
# pid (Passport ID)
# cid (Country ID)
# 
# ecl:gry pid:860033327 eyr:2020 hcl:#fffffd
# byr:1937 iyr:2017 cid:147 hgt:183cm
# 
# iyr:2013 ecl:amb cid:350 eyr:2023 pid:028048884
# hcl:#cfa07d byr:1929
# 
# hcl:#ae17e1 iyr:2013
# eyr:2024
# ecl:brn pid:760753108 byr:1931
# hgt:179cm
# 
# hcl:#cfa07d eyr:2025 pid:166559648
# iyr:2011 ecl:brn hgt:59in
# 
# The first passport is valid - all eight fields are present. The second passport is invalid - it is missing hgt (the Height field).
# 
# The third passport is interesting; the only missing field is cid. Treat this "passport" as valid.
# 
# The fourth passport is missing two fields, cid and byr. Missing cid is fine, but missing any other field is not, so this passport is invalid.
# 
# DAY 4.1
# =======
# (Q): Count the number of valid passports - those that have all required fields. Treat cid as optional. In your batch file, how many passports are valid?
# (A): Therefore, the improved system would report 2 valid passports.

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
