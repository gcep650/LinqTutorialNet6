using System;
using System.Linq;
using System.Collections.Generic;

namespace Exercises
{
    public static class OfType
    {
        //Coding Exercise 1
        //Implement the GetTheFirstInteger method, which given a collection of objects,
        //will return the first integer from this collection.
        //If no integer is present, it shall return null.
        public static int? GetTheFirstInteger(IEnumerable<object> objects)
        {
            return objects.OfType<int>().Cast<int?>().FirstOrDefault();
        }

        //Coding Exercise 2
        //Implement the AreAllStringsUpperCase method which will check if all strings
        //present in the objects collection are upper case.
        //If there is no string at all in this collection, the result should be true.
        public static bool AreAllStringsUpperCase(IEnumerable<object> objects)
        {
            // get list of only strings
            var strings = objects.OfType<string>();
            // if strings contains objects, check if all strings contain all uppercase char
            // if strings is empty collection, return true
            return strings != null && strings.Any() ? strings.All(str => str.All(c => char.IsUpper(c))) : true;
        }

        //Refactoring challenge
        public static DateTime? GetTheLatestDate_Refactored(IEnumerable<object> objects)
        {
            return objects.OfType<DateTime?>().OrderByDescending(date => date).FirstOrDefault();
        }

        //do not modify this method
        public static DateTime? GetTheLatestDate(IEnumerable<object> objects)
        {
            DateTime? lastDate = null;
            foreach (var obj in objects)
            {
                var dateTime = obj as DateTime?;
                if (dateTime != null)
                {
                    if (lastDate == null || lastDate < dateTime)
                    {
                        lastDate = dateTime;
                    }
                }
            }
            return lastDate;
        }
    }
}
