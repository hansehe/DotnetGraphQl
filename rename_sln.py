import os
import sys
import fnmatch
from SwarmManagement import SwarmTools

# Usage: python .\rename_sln.py -old OldSolName -new <new_sln_name>

DEFAULT_SRC_FOLDER = ''


def EndsWithPattern(filename, filePatterns):
    for filePattern in filePatterns:
        if filename.endswith(filePattern):
            return True
    return False


def ContainsPattern(filename, filePatterns):
    for filePattern in filePatterns:
        if filePattern in filename:
            return True
    return False


def RenameFileOrDirectory(path, filename, find, replace):
    if not(find in filename):
        return
    newFilename = filename.replace(find, replace)
    oldFilenamePath = os.path.join(path, filename)
    newFilenamePath = os.path.join(path, newFilename)
    os.rename(oldFilenamePath, newFilenamePath)


def ReplaceInFile(filepath, find, replace):
    with open(filepath) as f:
        s = f.read()
    s = s.replace(find, replace)
    with open(filepath, 'w') as f:
        f.write(s)


def findReplace(directory, find, replace, filePattern, filePatternsToIgnore = ['.pdb', '.dll'], directoriesToIgnore = ['.git']):
    for path, dirs, files in os.walk(os.path.abspath(directory)):
        if ContainsPattern(path, directoriesToIgnore):
            continue
        for filename in fnmatch.filter(files, filePattern):
            if EndsWithPattern(filename, filePatternsToIgnore):
                continue
            filepath = os.path.join(path, filename)
            try:
                ReplaceInFile(filepath, find, replace)
            except Exception as e:
                pass
                # print("Failed: {0}. Exception: {1}".format(filepath, str(e)))
            RenameFileOrDirectory(path, filename, find, replace)
    for path, dirs, files in os.walk(os.path.abspath(directory), topdown=False):
        for directory in dirs:
            RenameFileOrDirectory(path, directory, find, replace)


def GetOldSlnNameFromArguments(arguments):
    sln = SwarmTools.GetArgumentValues(arguments, '-old')
    if len(sln) == 0:
        raise Exception(
            "Please provide the old solution name with the '-old <sln_name>' argument.")
    return sln[0]


def GetNewSlnNameFromArguments(arguments):
    sln = SwarmTools.GetArgumentValues(arguments, '-new')
    if len(sln) == 0:
        raise Exception(
            "Please provide the new solution name with the '-new <sln_name>' argument.")
    return sln[0]


if __name__ == "__main__":
    arguments = sys.argv
    oldSlnName = GetOldSlnNameFromArguments(arguments)
    newSlnName = GetNewSlnNameFromArguments(arguments)
    directory = os.path.join(os.getcwd(), DEFAULT_SRC_FOLDER)
    print(directory)
    print(newSlnName)
    print(oldSlnName)
    findReplace(directory, oldSlnName, newSlnName, '*')
