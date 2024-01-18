using System;
using System.Collections.Generic;
using System.Linq;

namespace Exercises
{
    public static class Grouping
    {
        //Coding Exercise 1
        /*
        Implement the GetTheMostFrequentCharacter method, which given a string will 
        return the character that occurs most frequently in this string.

        If the string is null or empty, the result shall be null.        
        If more than one character occurs most frequently, the one that occurs first 
        in the word should be returned.         
        It doesn't matter if a character is upper case or lower case. 
        The result shall always be lower case.
        
        For example:        
            *the result shall be 's' for the word "grass".        
            *the result shall be 't' for the word "Toast'. 
                Please note that we treat both 'T' and 't' as the same character, 
                and we return the lower case letter.        
            *the result shall be 'b' for the word "Bumblebee". There are 3 letters 'b'
                and 3 letters 'e' in this word, but we return 'b' because its first 
                occurrence is before the first occurrence of the letter 'e'        
            *the result shall be null for the word "" (an empty string)
         */
        public static char? GetTheMostFrequentCharacter(string text)
        {
            if (text == null || text == string.Empty)
            {
                return null;
            }

            var lowerText = text.ToLower();

            // group by letter, then order by the count descending, then get the first letter
            return lowerText.GroupBy(
                letter => letter).OrderByDescending(group => group.Count()).First().Key;
        }

        //Coding Exercise 2
        /*
        Implement the FindTheHeaviestPetType method, which given a collection of Pets 
        will return the PetType for which the average weight of a Pet is the largest.

        For example, for the following input:
            *Pet name: Hannibal, Pet type: Fish, Pet weight: 1.1 kg
            *Pet name: Anthony, Pet type: Cat, Pet weight: 2 kg
            *Pet name: Ed, Pet type: Cat, Pet weight: 0.7 kg
            *Pet name: Taiga, Pet type: Dog, Pet weight: 35 kg
            *Pet name: Rex, Pet type: Dog, Pet weight: 40 kg
        
        ...the result shall be PetType.Dog, because the average weight of the dogs 
        from this collection is 37.5 kg, which is more than for cats (1.35 kg) and 
        fish (1.1kg).
        
        For an empty input collection, the result shall be null.
         */
        public static PetType? FindTheHeaviestPetType(IEnumerable<Pet> pets)
        {
            if (pets == null || pets.Count() < 1)
            {
                return null;
            }

            // group pets by type and weight, create dictionary to get average weight by pet type
            // order by heaviest pet weight
            // get heaviest and return the pet type
            return pets.GroupBy(
                pets => pets.PetType,
                pets => pets.Weight)
                .ToDictionary(
                group => group.Key,
                group => group.Average())
                .OrderByDescending(group => group.Value)
                .First().Key;
        }

        //Refactoring challenge
        public static IEnumerable<string> CountPets_Refactored(string petsData)
        {
            // return empty string if petsData is null or empty
            if (string.IsNullOrEmpty(petsData))
            {
                return new string[0];
            }

            // split petsData by comma
            // create dictionary where key is pet and value is the count
            // return list of strings that prints pet type and count
            return petsData.Split(',').GroupBy(pet => pet).Select(group => $"{group.Key}:{group.Count()}");
        }

        //do not modify this method
        public static IEnumerable<string> CountPets(string petsData)
        {
            if (string.IsNullOrEmpty(petsData))
            {
                return new string[0];
            }
            var pets = petsData.Split(',');
            var petsCountsDictionary = new Dictionary<string, int>();
            foreach (var pet in pets)
            {
                if (!petsCountsDictionary.ContainsKey(pet))
                {
                    petsCountsDictionary[pet] = 0;
                }
                petsCountsDictionary[pet]++;
            }
            var result = new List<string>();
            foreach (var petCount in petsCountsDictionary)
            {
                result.Add($"{petCount.Key}:{petCount.Value}");
            }
            return result;
        }

        public class Pet
        {
            public string Name { get; }
            public PetType PetType { get; }
            public float Weight { get; }

            public Pet(string name, PetType petType, float weight)
            {
                Name = name;
                PetType = petType;
                Weight = weight;
            }

            public override string ToString()
            {
                return $"Name: {Name}, Type: {PetType}, Weight: {Weight}";
            }
        }

        public enum PetType
        {
            Cat,
            Dog,
            Fish
        }
    }
}
