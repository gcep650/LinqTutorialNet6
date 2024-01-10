using System;
using System.Collections.Generic;
using System.Linq;

namespace Exercises
{
    public static class Aggregate
    {
        //Coding Exercise 1
        /*
        Imagine you are working on an activity tracker app. On the main screen, 
        we want to show the user the total activity time for the current day.

        Using the Aggregate method, implement the TotalActivityDuration method, 
        which given a collection of integers representing activities durations 
        in seconds will return a TimeSpan object representing the total time of activity.

        For example, for durations {10, 50, 121} the result shall be a TimeSpan 
        object with a total duration of 3 minutes and 1 second.
         */
        public static TimeSpan TotalActivityDuration(
            IEnumerable<int> activityTimesInSeconds)
        {
            // aggregate over activityTimesInSeconds
            // parameters are initial empty timespan and current time
            // each iteration adds current time to the total time timespan
            // returns the total time
            var totalTime = activityTimesInSeconds.Aggregate(new TimeSpan(), (totalSoFar, time) =>
            totalSoFar.Add(TimeSpan.FromSeconds(time))
            );

            return totalTime;
        }

        //Coding Exercise 2
        /*
         Using LINQ's Aggregate method implement the PrintAlphabet method which given 
        a count (assume it's from 1 to 26) will return a string with this count 
        of letters starting with 'a'.

        For example:
            *For count 5 it will return "a,b,c,d,e"
            *For count 3 it will return "a,b,c"
            *For count 1 it will return "a"
        
        For count less than 1 or more than 26 it will throw ArgumentException
         */
        public static string PrintAlphabet(int count)
        {
            // throw argumentexception if count is not between 1-26 inclusive
            if (count < 1 || count > 26)
            {
                throw new ArgumentException("Count must be between 1 to 26 inclusive.");
            }

            // create alphabet string array
            string[] alphabet = new string[] {"a","b","c","d","e","f","g",
                "h","i","j","k","l","m","n","o","p",
                "q","r","s","t","u","v","w","x","y","z" };

            // use take to only aggregate over specified count
            // use aggregate to combine letters with separator into final string
            var output = alphabet.Take(count)
                .Aggregate((total, letter) => total + "," + letter);

            return output;
        }

        //Refactoring challenge
        public static IEnumerable<int> Fibonacci_Refactored(int n)
        {
            // throw exception if count is less than one
            if (n < 1)
            {
                throw new ArgumentException("n must be greater than 1.");
            }
            else if (n == 1)
            {
                // return 0 if count is only 1
                return new[] { 0 };
            }

            // create initial list, represents count
            var list = Enumerable.Range(1, n - 2);

            // aggregate over list (acts as index i from for loop)
            var result = list.Aggregate(new List<int> { 0, 1 }, (sequence, current) =>
            sequence.Append(sequence[current - 1] + sequence[current]).ToList()
            );

            return result;
        }

        //do not modify this method
        public static IEnumerable<int> Fibonacci(int n)
        {
            if (n < 1)
            {
                throw new ArgumentException(
                    $"Can't generate Fibonacci sequence " +
                    $"for {n} elements. N must be a " +
                    $"positive number");
            }

            if (n == 1)
            {
                return new[] { 0 };
            }
            var result = new List<int> { 0, 1 };
            for (int i = 1; i < n - 1; ++i)
            {
                result.Add(result[i - 1] + result[i]);
            }
            return result;
        }
    }
}
