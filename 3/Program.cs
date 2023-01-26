using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace AdventOfCode3
{
    internal class Program
    {
        static int compartmentSize;
        static char[] compartment1;
        static char[] compartment2;
        static char[] duplicateItems;

        static void Main(string[] args)
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Assets\rucksacks.txt");
            string[] lines = System.IO.File.ReadAllLines(path);
            int resultForDuplicates = SolveDuplicateItems(lines);
            Console.WriteLine("result duplicates {0}", resultForDuplicates);
            int badgesResult = SolveBadges(lines);
            Console.WriteLine("result badges {0}", badgesResult);
        }

        private static int SolveBadges(string[] lines)
        {
            int lineIndex = 2;
            char[] rucksack1;
            char[] rucksack2;
            char[] rucksack3;
            char[] badges;
            int result = 0;

            for (int i = 0; i < lines.Length; i += 3)
            {
                rucksack1 = lines[lineIndex - 2].ToCharArray();
                rucksack2 = lines[lineIndex - 1].ToCharArray();
                rucksack3 = lines[lineIndex - 0].ToCharArray();
                badges = rucksack1.Intersect(rucksack2).ToArray();
                badges = badges.Intersect(rucksack3).ToArray();
                lineIndex += 3;
                result += GetValueForDuplicateItems(badges);
            }
            return result;
        }

        private static int SolveDuplicateItems(string[] lines)
        {
            int valueOfItems = 0;
            foreach (string line in lines)
            {
                compartmentSize = line.Length / 2;
                compartment1 = line.Substring(0, compartmentSize).ToCharArray();
                compartment2 = line.Substring(compartmentSize).ToCharArray();
                duplicateItems = compartment1.Intersect(compartment2).ToArray();
                valueOfItems += GetValueForDuplicateItems(duplicateItems);
            }
            return valueOfItems;
        }

        private static int GetValueForDuplicateItems(char[] duplicateItems)
        {
            int value = 0;
            foreach(int charValue in duplicateItems)
            {
                if(charValue >= 97) value += charValue - 96;
                if (charValue <= 90) value += charValue - 38;
            }
            return value;
        }
    }
}
