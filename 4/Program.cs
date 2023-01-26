using System;
using System.IO;
using System.Reflection;

namespace AdventOfCode4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Assets\pairs.txt");
            string[] lines = System.IO.File.ReadAllLines(path);
            int overlapingPairsCount = GetNumberOfOverlapingPairs(lines);
            int partlyOverlapingPairsCound = GetNumberOfPartlyOverlapingPairs(lines);
            Console.WriteLine("overlaping pair = {0}", overlapingPairsCount);
            Console.WriteLine("partly overlaping pair = {0}", partlyOverlapingPairsCound);
        }

        private static int GetNumberOfOverlapingPairs(string[] lines)
        {
            int overlapingPairsCount = 0;
            foreach(string line in lines)
            {
                String[] pairsArray = line.Split(',');
                int[] firstPairMinMax = GetIntValuesForPair(pairsArray[0]);
                int[] secondPairMinMax = GetIntValuesForPair(pairsArray[1]);
                if (IsOverlapingPair(firstPairMinMax, secondPairMinMax)) overlapingPairsCount += 1;
            }
            return overlapingPairsCount;
        }

        private static int[] GetIntValuesForPair(string pair)
        {
            int[] values = new int[2];
            String[] stringValues = pair.Split('-');
            values[0] = int.Parse(stringValues[0]);
            values[1] = int.Parse(stringValues[1]);
            return values;
        }

        private static bool IsOverlapingPair(int[] firstPairMinMax, int[] secondPairMinMax)
        {
            if(firstPairMinMax[0] <= secondPairMinMax[0] && firstPairMinMax[1] >= secondPairMinMax[1]) return true;
            if (firstPairMinMax[0] >= secondPairMinMax[0] && firstPairMinMax[1] <= secondPairMinMax[1]) return true;
            return false;
        }

        private static int GetNumberOfPartlyOverlapingPairs(string[] lines)
        {
            int partlyOverlapingPairs = 0;
            foreach (string line in lines)
            {
                Console.WriteLine(line);
                String[] pairsArray = line.Split(',');
                int[] firstPairMinMax = GetIntValuesForPair(pairsArray[0]);
                int[] secondPairMinMax = GetIntValuesForPair(pairsArray[1]);
                if (IsPartlyOverlapingPair(firstPairMinMax, secondPairMinMax)) partlyOverlapingPairs += 1;
                Console.WriteLine(partlyOverlapingPairs);
            }
            return partlyOverlapingPairs;
        }

        private static bool IsPartlyOverlapingPair(int[] firstPairMinMax, int[] secondPairMinMax)
        {
            if (firstPairMinMax[0] <= secondPairMinMax[0] && firstPairMinMax[1] >= secondPairMinMax[0]) return true;
            if (firstPairMinMax[0] <= secondPairMinMax[1] && firstPairMinMax[1] >= secondPairMinMax[1]) return true;
            if (firstPairMinMax[0] > secondPairMinMax[0] && firstPairMinMax[1] < secondPairMinMax[1]) return true;
            return false;
        }
    }
}
