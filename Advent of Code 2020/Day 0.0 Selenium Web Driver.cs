/*
ADVENT OF CODE 2020 CONSOLE APPLICATION
=======================================
Everyday from December 1st - 25th, coding challenges are released at https://adventofcode.com/2020.
Each day's challenge is composed of two problems to be solved sequentially. 
Day X.2 is slightly harder than Day X.1, but related. 
This console application issues calls to the solutions of the daily challenges.
The solutions are written as C# files, C++ DLLs, and Python scripts.
This application automates logins to the website using GitHub credentials, and web scrapes the puzzle input (HTML) using Selenium Webdriver.
This application communicates with C++ DLLs using interop services and with Python scripts using IronPython hosting.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;

namespace Advent_of_Code_2020
{
    class Program
    {
        const string dirChromeDriver = @"C:\Users\Andrew Lay\source\repos\Advent of Code 2020\Advent of Code 2020\bin\Debug\netcoreapp3.0\";
        const string dirPythonScript_Day_2_1 = @"C:\Users\Andrew Lay\source\repos\Advent of Code 2020\Advent of Code 2020 Py\Day 2.1 IronPython Script.py";
        const string dirPythonScript_Day_2_2 = @"C:\Users\Andrew Lay\source\repos\Advent of Code 2020\Advent of Code 2020 Py\Day 2.2 Password Philosophy.py";
        const string dirPythonScript_Day_4_1 = @"C:\Users\Andrew Lay\source\repos\Advent of Code 2020\Advent of Code 2020 Py\Day 4.1 Batch File for Passport Valid.py";
        const string dirPythonScript_Day_4_2 = @"C:\Users\Andrew Lay\source\repos\Advent of Code 2020\Advent of Code 2020 Py\Day 4.2 Strict Passport Rules.py";
        const string dirPythonScript_Day_8_1 = @"C:\Users\Andrew Lay\source\repos\Advent of Code 2020\Advent of Code 2020 Py\Day 8.1 Instruction Line Jumping.py";
        const string dirPythonScript_Day_8_2 = @"C:\Users\Andrew Lay\source\repos\Advent of Code 2020\Advent of Code 2020 Py\Day 8.2 Instruction Line Jumping.py";
        const string CppFunctionsDLL = @"..\..\..\..\x64\Debug\Advent of Code 2020 C++.dll";
        const string url_Input_Day_X = "https://adventofcode.com/2020/day/X/input";

        [DllImport(CppFunctionsDLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int FindHighestSeatID(string[] arrayInputPuzzle, int numArrayInput);
        [DllImport(CppFunctionsDLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int FindMySeatID(string[] arrayInputPuzzle, int numArrayInput);

        [DllImport(CppFunctionsDLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern double FindFirstNumbNotSumOfPrevNumbs(double[] arrayInputPuzzle, int numArrayInput, int preambleSize);
        [DllImport(CppFunctionsDLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern double FindSumOfSmallestAndLargestNumbsInContiguousRange(double[] arrayInputPuzzle, int numArrayInput, double contiguousSumVal);

        static void Main(string[] args)
        {
            IWebDriver driver = new ChromeDriver(dirChromeDriver);
            AutomateGitHubCredentials(driver);

            bool isTerminate = false;
            string day = null;
            List<string> listInputPuzzle;
            string[] arrayInputPuzzle;
            int intReturnValue = 0;
            var scriptReturn = -1;
            while (!isTerminate)
            {
                switch (day)
                {
                    case "1":
                        driver.Navigate().GoToUrl(url_Input_Day_X.Replace("X", day));
                        listInputPuzzle = Enumerable.ToList(ReadInputPuzzle(driver));
                        Day_1.SolvePuzzle2Numbs(listInputPuzzle);
                        Day_1.SolvePuzzle3Numbs(listInputPuzzle);
                        day = null;
                        break;
                    case "2":
                        driver.Navigate().GoToUrl(url_Input_Day_X.Replace("X", day));
                        listInputPuzzle = Enumerable.ToList(ReadInputPuzzle(driver));
                        scriptReturn = CreateIronPythonSession(listInputPuzzle, dirPythonScript_Day_2_1, "numValidPword");
                        Console.WriteLine("Day 2.1 -- The Number of Valid Passwords is: " + scriptReturn);
                        scriptReturn = CreateIronPythonSession(listInputPuzzle, dirPythonScript_Day_2_2, "numValidPword");
                        Console.WriteLine("Day 2.2 -- The Number of Valid Passwords is: " + scriptReturn);
                        day = null;
                        break;
                    case "3":
                        driver.Navigate().GoToUrl(url_Input_Day_X.Replace("X", day));
                        listInputPuzzle = Enumerable.ToList(ReadInputPuzzle(driver));
                        int xMov, yMov;
                        Console.Write("----> Enter the Number of X-movements to the Right: ");
                        bool isValidxPos = Int32.TryParse(Console.ReadLine(), out xMov);
                        Console.Write("----> Enter the Number of Y-movements to the Down: ");
                        bool isValidyPos = Int32.TryParse(Console.ReadLine(), out yMov);
                        if (isValidxPos && isValidyPos)
                        {
                            uint numTrees = Day_3.SolveNumTrajectoryTrees(listInputPuzzle, xMov, yMov);
                            double productOfTrees = Day_3.SolveProductTreesEncountered(listInputPuzzle);
                            Console.WriteLine("Day 3.1 -- The Number of Trees Encountered is: " + numTrees);
                            Console.WriteLine("Day 3.2 -- The Product of Trees Encountered per Slope is: " + productOfTrees);
                        }
                        day = null;
                        break;
                    case "4":
                        driver.Navigate().GoToUrl(url_Input_Day_X.Replace("X", day));
                        listInputPuzzle = Enumerable.ToList(ReadInputPuzzle(driver));
                        scriptReturn = CreateIronPythonSession(listInputPuzzle, dirPythonScript_Day_4_1, "numValidPassport");
                        Console.WriteLine("Day 4.1 -- The Number of Valid Passports is: " + scriptReturn);
                        scriptReturn = CreateIronPythonSession(listInputPuzzle, dirPythonScript_Day_4_2, "numValidPassport");
                        Console.WriteLine("Day 4.2 -- The Number of Valid Passports is: " + scriptReturn);
                        day = null;
                        break;
                    case "5":
                        driver.Navigate().GoToUrl(url_Input_Day_X.Replace("X", day));
                        arrayInputPuzzle = ReadInputPuzzle(driver);
                        intReturnValue = FindHighestSeatID(arrayInputPuzzle, arrayInputPuzzle.Length);
                        Console.WriteLine("Day 5.1 -- The Highest Seat ID of a Boarding Pass is: " + intReturnValue);
                        intReturnValue = FindMySeatID(arrayInputPuzzle, arrayInputPuzzle.Length);
                        Console.WriteLine("Day 5.2 -- The Seat ID of my missing Boarding Pass is: " + intReturnValue);
                        day = null;
                        break;
                    case "6":
                        driver.Navigate().GoToUrl(url_Input_Day_X.Replace("X", day));
                        listInputPuzzle = Enumerable.ToList(ReadInputPuzzle(driver));
                        intReturnValue = Day_6.SolveNumberDistinctAnswersByAnyone(listInputPuzzle);
                        Console.WriteLine("Day 6.1 -- The Sum of the Number of Distinct Questions Answered by Anyone is: " + intReturnValue);
                        intReturnValue = Day_6.SolveNumberDistinctAnswersByEveryone(listInputPuzzle);
                        Console.WriteLine("Day 6.2 -- The Sum of the Number of Distinct Questions Answered by Everyone is: " + intReturnValue);
                        day = null;
                        break;
                    case "7":
                        driver.Navigate().GoToUrl(url_Input_Day_X.Replace("X", day));
                        listInputPuzzle = Enumerable.ToList(ReadInputPuzzle(driver));
                        intReturnValue = Day_7.SolveNumberBagColorsContainAtLeastOneShinyGoldBag(listInputPuzzle);
                        Console.WriteLine("Day 7.1 -- The Number of Bag Colours that eventually contain at least One Shiny Gold Bag is: " + intReturnValue);
                        intReturnValue = Day_7.SolveNumberBagsRequiredInShinyGoldBag(listInputPuzzle);
                        Console.WriteLine("Day 7.2 -- The Number of Bags Required inside One Single Shiny Gold Bag is: " + intReturnValue);
                        day = null;
                        break;
                    case "8":
                        driver.Navigate().GoToUrl(url_Input_Day_X.Replace("X", day));
                        listInputPuzzle = Enumerable.ToList(ReadInputPuzzle(driver));
                        scriptReturn = CreateIronPythonSession(listInputPuzzle, dirPythonScript_Day_8_1, "glbAccValue");
                        Console.WriteLine("Day 8.1 -- The value in the Accumulator variable is: " + scriptReturn);
                        scriptReturn = CreateIronPythonSession(listInputPuzzle, dirPythonScript_Day_8_2, "glbAccValue");
                        Console.WriteLine("Day 8.2 -- The value in the Accumulator variable is: " + scriptReturn);
                        day = null;
                        break;
                    case "9":
                        driver.Navigate().GoToUrl(url_Input_Day_X.Replace("X", day));
                        arrayInputPuzzle = ReadInputPuzzle(driver);
                        int preambleSize;
                        Console.Write("----> Enter the Size of the Preamble: ");
                        bool isValidPreambleSize = Int32.TryParse(Console.ReadLine(), out preambleSize);
                        if (isValidPreambleSize)
                        {
                            double dblReturnValue = FindFirstNumbNotSumOfPrevNumbs(Array.ConvertAll(arrayInputPuzzle, double.Parse), arrayInputPuzzle.Length, preambleSize);
                            Console.WriteLine("Day 9.1 -- The First Number which is not the sum of two numbers in the preamble is: " + dblReturnValue);
                            dblReturnValue = FindSumOfSmallestAndLargestNumbsInContiguousRange(Array.ConvertAll(arrayInputPuzzle, double.Parse), arrayInputPuzzle.Length, dblReturnValue);
                            Console.WriteLine("Day 9.2 -- The sum of the Smallest and Largest numbers from the contiguous range is: " + dblReturnValue);
                        }
                        day = null;
                        break;
                    case "10":
                        driver.Navigate().GoToUrl(url_Input_Day_X.Replace("X", day));
                        listInputPuzzle = Enumerable.ToList(ReadInputPuzzle(driver));
                        intReturnValue = Day_10.ProductOfOneAndThreeJoltDifferences(listInputPuzzle);
                        Console.WriteLine("Day 10.1 -- The Product between the Number of 1-Jolt Differences and 3-Jolt Differences is: " + intReturnValue);
                        long returnNumWays = Day_10.TotalNumberOfWaysConnectingOutletToCharger(listInputPuzzle);
                        Console.WriteLine("Day 10.2 -- The Total Number of Ways to Connect the Outlet to Device is: " + returnNumWays);
                        day = null;
                        break;
                    case "end":
                        isTerminate = true;
                        day = null;
                        break;
                default:
                        Console.Write("Enter 'end' to terminate. Enter the Day for which the Activity Challenges will be solved by the Program: ");
                        day = Console.ReadLine();
                        break;
                }
            }
            if (isTerminate)
            {
                driver.Close();
                driver.Quit();
                Environment.Exit(0);
            }
        }

        static void AutomateGitHubCredentials(IWebDriver driver)
        {
            driver.Url = "https://adventofcode.com/2020/auth/login";
            IWebElement element = driver.FindElement(By.LinkText("[Log In]"));
            element.Click();
            element = driver.FindElement(By.LinkText("[GitHub]"));
            element.Click();
            Console.Write("Enter GitHub username: ");
            driver.FindElement(By.Id("login_field")).SendKeys(Console.ReadLine());
            Console.Write("Enter GitHub password: ");
            driver.FindElement(By.Id("password")).SendKeys(Console.ReadLine());
            element = driver.FindElement(By.Name("commit"));
            element.Click();
        }

        static string[] ReadInputPuzzle(IWebDriver driver)
        {
            By mySelector = By.XPath("/html/body/pre");
            System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> webElements = driver.FindElements(mySelector);
            string[] arrayInputPuzzle = new string[] { };
            foreach (IWebElement element in webElements)
            {
                arrayInputPuzzle = element.Text.Split(new[] { "\r\n", "\r", "\n", Environment.NewLine }, StringSplitOptions.None);
            }
            return arrayInputPuzzle;
        }

        static int CreateIronPythonSession(List<string> listInputPuzzle, string sourcePyScript, string scriptOutputVar)
        {
            var pyEngine = Python.CreateEngine();
            var pyScope = pyEngine.CreateScope();
            pyEngine.SetSearchPaths(new[] { "C:\\Program Files (x86)\\Microsoft Visual Studio\\Shared\\Python37_64\\Lib" });
            pyScope.SetVariable("listInputPuzzle", listInputPuzzle);
            ScriptSource scriptSource = pyEngine.CreateScriptSourceFromFile(sourcePyScript);
            scriptSource.Execute(pyScope);
            var scriptReturn = pyScope.GetVariable(scriptOutputVar);
            return scriptReturn;
        }
    }
}
