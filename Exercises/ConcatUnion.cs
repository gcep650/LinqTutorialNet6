using System;
using System.Linq;
using System.Collections.Generic;

namespace Exercises
{
    public static class ConcatUnion
    {
        //Coding Exercise 1
        /*
        Imagine you are working on a news website. On the main panel of this website, 
        we want to show the most important news, as well as the most recent ones. 

        Implement the SelectRecentAndImportant method which given a collection 
        of news will return the three most recent news, 
        as well as all news with priority set to high.

        For example, for the following collection of news 
        we will return the following news:
            *2021/10/6 and priority high - because it has the high priority (and it's amongst 3 most recent news)
            *2021/10/5 and priority low - because it's amongst 3 most recent news
            *2021/10/4 and priority medium - because it's amongst 3 most recent news
            *2021/10/3 and priority medium - WILL NOT BE INCLUDED IN RESULT
            *2021/10/2 and priority high - because it has the high priority
            *2021/10/1 and priority low - WILL NOT BE INCLUDED IN RESULT
         */
        public static IEnumerable<News> SelectRecentAndImportant(
            IEnumerable<News> newsCollection)
        {
            var recent = newsCollection.OrderByDescending(news => news.PublishingDate).Take(3);
            var highPriority = newsCollection.Where(news => news.Priority == Priority.High);

            return recent.Union(highPriority);
        }

        //Coding Exercise 2
        /*
         Implement the CleanWord method, which given a string that can consist 
        of letters and non-letter characters, will return a new string, 
        where all letters proceed the non-letter characters. 
        The non-letter characters should be unique in the result.

        For example:
            *for input "f_o!_!x" the result will be "fox_!". 
                Please note that only the first "!" is present in the result 
                according to this rule "The non-letter characters should be unique 
                in the result."
            *for input "d_3uc(k))" the result will be "duck_3()". 
                Please note that only the first ")" is present in the result 
                according to this rule "The non-letter characters should be unique 
                in the result."
         */
        public static string CleanWord(string word)
        {
            var letters = word.Where(letter => (letter >= 65 && letter <= 90) ||
            (letter >= 97 && letter <= 122));
            var nonletters = word.Where(letter => !(letter >= 65 && letter <= 90) &&
            !(letter >= 97 && letter <= 122)).Distinct();

            return new string(letters.Concat(nonletters).ToArray());
        }

        //Refactoring challenge
        public static IEnumerable<int> GetPerfectSquares_Refactored(
            IEnumerable<int> numbers1, IEnumerable<int> numbers2)
        {
            // 
            var set1 = numbers1.Where(num => Math.Sqrt(num) % 1 == 0).Distinct();
            var set2 = numbers2.Where(num => Math.Sqrt(num) % 1 == 0).Distinct();
            var result = set1.Union(set2).ToList();
            result.Sort();
            return result;
        }

        //do not modify this method
        public static IEnumerable<int> GetPerfectSquares(IEnumerable<int> numbers1, IEnumerable<int> numbers2)
        {
            var result = new List<int>();
            foreach (var number in numbers1)
            {
                if (Math.Sqrt(number) % 1 == 0 && !result.Contains(number))
                {
                    result.Add(number);
                }
            }
            foreach (var number in numbers2)
            {
                if (Math.Sqrt(number) % 1 == 0 && !result.Contains(number))
                {
                    result.Add(number);
                }
            }
            result.Sort();
            return result;
        }

        public struct News
        {
            public DateTime PublishingDate { get; set; }
            public Priority Priority { get; set; }

            public override string ToString()
            {
                return $"Date: {PublishingDate.ToString("d")}, Priority: {Priority}";
            }
        }

        public enum Priority
        {
            Low,
            Medium,
            High
        }
    }
}
