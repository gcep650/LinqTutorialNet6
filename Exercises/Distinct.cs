using System;
using System.Collections.Generic;
using System.Linq;

namespace Exercises
{
    public static class Distinct
    {
        //Coding Exercise 1
        /*
         Implement the AreAllUnique method, which will check if all elements of 
        a collection are unique.

        For example:
            *for numbers 1,2,3,4 the result shall be true because no number is duplicated
            *for strings AAA, BBB, BBB, CCC the result shall be false 
                because BBB is duplicated
         */
        public static bool AreAllUnique<T>(IEnumerable<T> collection)
        {
            // check if distinct collection has same count as unmodified collection
            return collection.Count() == collection.Distinct().Count();
        }

        //Coding Exercise 2
        /*
         Implement the GetCollectionWithMostDuplicates method, which given 
        a collection of collections will return the collection with 
        the most duplicates in it. 
        If a couple of collections have the same count of duplicates, 
        the shortest should be returned. 
        If the collections parameter is empty, the result shall be null.

        Let's consider the following collections:        
            *{1,2,3,4} - it has 0 duplicates        
            *{1,2,3,3,4,4,4} - it has 3 duplicates: 
                one 3 is a duplicate, and two 4s are duplicates       
            *{1,2,3,3,4,4,4,5,6,7} - it has 3 duplicates: one 3 is a duplicate, 
                and two 4s are duplicates
        
        The result shall be the second collection, because it has the most duplicates, 
        and it is the shortest of two collections with 3 duplicates
         */
        public static IEnumerable<T> GetCollectionWithMostDuplicates<T>(
            IEnumerable<IEnumerable<T>> collections)
        {
            // return null if collections is empty
            if (collections == null || collections.Count() < 1)
            {
                return null;
            }
            // return shortest collection with the most duplicates

            // get starting values (first collection)
            var retCollection = collections.First();
            // calculate duplicates
            int retDupCount = retCollection.Count() - retCollection.Distinct().Count();

            foreach (var collection in collections)
            {
                // calculate duplicates for current collection
                int currentDupCount = collection.Count() - collection.Distinct().Count();

                // current collection becomes return value if duplicate count is greater
                // or current duplicate count is the same as retDupCount and the collection is smaller
                if (currentDupCount > retDupCount ||
                    currentDupCount == retDupCount && collection.Count() < retCollection.Count())
                {
                    retCollection = collection;
                    retDupCount = currentDupCount;
                }
            }

            return retCollection;
        }

        //Refactoring challenge
        public static IEnumerable<string> GetWordsShorterThan5Letters_Refactored(
            IEnumerable<string> words)
        {
            return words.Distinct().Where(word => word.Length < 5);
        }

        //do not modify this method
        public static IEnumerable<string> GetWordsShorterThan5Letters(
            IEnumerable<string> words)
        {
            var result = new List<string>();
            foreach (var word in words)
            {
                if (word.Length < 5 && !result.Contains(word))
                {
                    result.Add(word);
                }
            }
            return result;
        }
    }
}
