using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace kalorije
{
    internal class Program
    {
        static int calories = 0;
        static int maxCalories = 0;
        static int skratIndex = 1;
        static int maxCaloriesIndex = 1;
        static int[] top3Calories = new int[3];

        static void Main(string[] args)
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Assets\kalorije.txt");
            string[] lines = System.IO.File.ReadAllLines(path);
            
            foreach (string line in lines)
            {
                if(line.Length > 0)
                {
                    calories += int.Parse(line);
                }
                else
                {
                    CheckCaloriesCount(calories);
                    calories = 0;
                    skratIndex++;
                }
            }
            Console.WriteLine("Elf with max calories: {0} has {1} calories", maxCaloriesIndex, maxCalories);
            int sumTop3 = top3Calories[0] + top3Calories[1] + top3Calories[2];
            Console.WriteLine("sum top 3 calories: {0}", sumTop3);
        }

        private static void CheckCaloriesCount(int calories)
        {
            if (calories >= maxCalories)
            {
                maxCalories = calories;
                maxCaloriesIndex = skratIndex;
                top3Calories[0] = top3Calories[1];
                top3Calories[1] = top3Calories[2];
                top3Calories[2] = maxCalories;
            }
            else if (Top2CaloriesCheck(calories))
            {
                top3Calories[0] = top3Calories[1];
                top3Calories[1] = calories;
            }
            else if(Top3CaloriesCheck(calories))
            {
                top3Calories[0] = calories;
            }
        }

        private static bool Top2CaloriesCheck(int calories)
        {
            return calories >= top3Calories[1];
        }

        private static bool Top3CaloriesCheck(int calories)
        {
            return calories >= top3Calories[0];
        }
    }
}
