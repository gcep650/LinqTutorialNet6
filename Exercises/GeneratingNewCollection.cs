using System;
using System.Collections.Generic;
using System.Linq;

namespace Exercises
{
    public static class GeneratingNewCollection
    {
        //Coding Exercise 1
        /*
         Implement the NewYearsEvesSince method, which given an initialYear and 
        yearsCount parameters, will build a dictionary with the key being the year, 
        and the value being the day of the week (as string) of the New Year's Eve. 
        Please assume the New Year's Eve is on the 31st of December 
        (according to the Gregorian calendar).

        For example, for initialYear 1900 and yearsCount 5, 
        the result shall be a dictionary with the following keys and values:        
            *[1900] = "Monday" ,        
            *[1901] = "Tuesday",         
            *[1902] = "Wednesday",        
            *[1903] = "Thursday",         
            *[1904] = "Saturday"
        
        As you can see this dictionary contains 5 elements because the yearsCount
        parameter was set to 5.
         */
        public static Dictionary<int, string> NewYearsEvesSince(
            int initialYear, int yearsCount)
        {
            // create range starting with initialYear and ending with initialYear + yearsCount
            var years = Enumerable.Range(initialYear, yearsCount);

            // create dictionary where year is key and value is day of week
            return years.ToDictionary(year => year, year => new DateTime(year, 12, 31).DayOfWeek.ToString());
        }

        //Coding Exercise 2
        /*
        Implement the BuildTree method, which given a number of levels, 
        will return a string representation of a tree. For example, for 5 levels 
        the result should be a string like this:
        "*
        **
        ***
        ****
        *****"

        Remember that to get the new line character, 
        you can use the Environment.NewLine property. 
        It will return "\n" when running on Unix systems, 
        and "\r\n" when running on Windows (the new line symbol
        is different for those two systems). 
        This way, no matter where your code is run, it will work correctly.

         */
        public static string BuildTree(int levels)
        {
            return Enumerable.Range(1, levels)
                .Aggregate(string.Empty, (tree, level) =>
                {
                    if (level > 1)
                    {
                        tree += Environment.NewLine;
                    }
                    string s = new string(Enumerable.Repeat('*', level).ToArray());
                    return tree + s;
                });
        }

        //Refactoring challenge
        public static IEnumerable<string> DoubleLetters_Refactored(int countOfLetters)
        {
            // count can only be 26 or less
            const int countOfLettersInEngAlphabet = 26;
            var finalCount = Math.Min(countOfLetters, countOfLettersInEngAlphabet);

            // create list of letters using enumerable range
            var letters = Enumerable.Range('A', finalCount).Select(num => (char)num);

            // change to list of strings in which each string is a possible combination of two letters
            var result = letters.Select(letter => letters.Select(letter2 => $"{letter}{letter2}")).SelectMany(list => list);

            return result;
        }

        //do not modify this method
        public static IEnumerable<string> DoubleLetters(int countOfLetters)
        {
            const int CountOfLettersInEnglishAlphabet = 26;
            var finalCountOfLetters = Math.Min(
                countOfLetters,
                CountOfLettersInEnglishAlphabet);

            var allLetters = new List<char>();
            var letter = 'A';
            for (int i = 0; i < finalCountOfLetters; ++i)
            {
                allLetters.Add(letter);
                letter++;
            }

            var result = new List<string>();
            foreach (var firstLetter in allLetters)
            {
                foreach (var secondLetter in allLetters)
                {
                    result.Add($"{firstLetter}{secondLetter}");
                }
            }
            return result;
        }
    }
}
