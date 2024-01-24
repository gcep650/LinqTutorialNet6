using System;
using System.Collections.Generic;
using System.Linq;

namespace Exercises
{
    public static class Select
    {
        //Coding Exercise 1
        /*
         Imagine you work on an application that retrieves data from scanned paper 
        documents. Let's say you only want to retrieve numbers from some particular 
        documents, unfortunately, they contain other data, like dates, strings, etc.

        Implement the GetNumbers method, which given a collection of objects 
        of different types, will return a collection of integers. 
        Please note that if an object in the input collection is a string, 
        it should be parsed to int if possible.
        
        For example, for input collection 
            *{"1", 3, "abc", new DateTime(2020,1,2), true, "10"} the result shall be 
                {1,3,10}.
        
        The result collection shall be ordered from least to greatest.
         */
        public static IEnumerable<int> GetNumbers(IEnumerable<object> objects)
        {
            // get list of all ints in the collection
            var ints = objects.OfType<int>();
            // get list of all strings in the collection
            var strings = objects.OfType<string>();

            // parse each string into int or return null
            // remove all null ints (strings that were not numbers)
            // cast as int without nulls
            var parsedStrings = strings.Select(str =>
            {
                int parsed;
                if (int.TryParse(str, out parsed))
                {
                    return parsed;
                }
                return (int?)null;
            }).Where(n => n != null).Cast<int>();

            // concat int and parsed strings and order by ascending
            return ints.Concat(parsedStrings).OrderBy(n => n);
            
        }

        //Coding Exercise 2
        /*
        Imagine you work on an application that retrieves data from scanned paper 
        documents. Those documents contain personal data which you want to convert to 
        C# objects.

        Implement the PeopleFromString method, which given a single string containing 
        personal data of multiple people, will return a collection of people. 
        If a person's data is not valid and can't be properly parsed, 
        this person should not be included in the result collection.
        
        For example, for an input string:        
            "John Smith, 1983/08/21;Jane Doe, 1993/12/21;Francis Brown, invalid date here"
        
        ...the result shall be a collection of two people: 
            John Smith and Jane Doe. 
        We shall not include Francis Brown, because his date of birth is not valid 
        and can't be parsed to a valid DateTime.
        
        As you can see in the example the separator between each person's data is ";", 
        and the full name and the date of birth are separated with ",".
         */
        public static IEnumerable<Person> PeopleFromString(string input)
        {
            // split input string by ; to get list of person entries
            // split each entry by comma to get name and date of birth
            // parse date of birth, if fails, return null instead of new Person object

            return input.Split(';').Select(person =>
            {
                var data = person.Split(',');
                var name = data[0].Split(' ');
                DateTime dob;
                if (!DateTime.TryParse(data[1], out dob))
                {
                    return null;
                }

                var current = new Person();
                current.FirstName = name[0];
                current.LastName = name[1];
                current.DateOfBirth = dob;

                return current;
            }).Where(p => p != null);
        }

        //Refactoring challenge
        public static TimeSpan TotalDurationOfSongs_Refactored(string allSongsDuration)
        {
            if (string.IsNullOrEmpty(allSongsDuration))
            {
                return new TimeSpan();
            }

            return allSongsDuration.Split(',').Select(
                duration => TimeSpan.ParseExact(
                    duration, @"m\:ss", null))
                .Aggregate(new TimeSpan(), (total, current) => total += current);
        }

        //do not modify this method
        public static TimeSpan TotalDurationOfSongs(
            string allSongsDuration)
        {
            if (string.IsNullOrEmpty(allSongsDuration))
            {
                return new TimeSpan();
            }

            var durationStrings = allSongsDuration.Split(',');
            var totalDuration = 0d;
            foreach (var durationString in durationStrings)
            {
                var timeSpan = TimeSpan.ParseExact(
                    durationString, @"m\:ss", null);
                totalDuration += timeSpan.TotalSeconds;
            }

            return TimeSpan.FromSeconds(totalDuration);
        }

        public class Person : IEquatable<Person>
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public DateTime DateOfBirth { get; set; }

            public override string ToString()
            {
                return $"{FirstName} {LastName} born on {DateOfBirth.ToString("d")}";
            }

            public bool Equals(Person other)
            {
                return FirstName == other.FirstName &&
                       LastName == other.LastName &&
                       DateOfBirth == other.DateOfBirth;
            }
        }
    }
}
