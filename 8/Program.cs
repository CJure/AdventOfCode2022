using System;
using System.IO;
using System.Reflection;

namespace AdventOfCode8
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Assets\forest.txt");
            string[] lines = System.IO.File.ReadAllLines(path);
            int visibleTrees = (lines.Length * 4) - 4;
            char[,] trees = LinesTo2DCharArray(lines);
            visibleTrees += CountVisibleTrees(trees);
            Console.WriteLine("VisibleTrees = {0}", visibleTrees);
            int maxScenicScore = GetMaxScenicScore(trees);
            Console.WriteLine("Max scenic score = {0}", maxScenicScore);
        }

        private static char[,] LinesTo2DCharArray(string[] lines)
        {
            char[,] tempTrees = new char[lines.Length,lines.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    tempTrees[i, j] = lines[i][j];
                }
            }
            return tempTrees;
        }

        private static int CountVisibleTrees(char[,] trees)
        {
            int visibleTreesCount = 0;
            for(int i = 1; i < trees.GetLength(0) - 1; i++)
            {
                for(int a = 1; a < trees.GetLength(1) - 1; a++)
                {
                    if (TreeVisible(trees, i, a))
                    {
                        visibleTreesCount += 1;
                    }
                }
            }
            return visibleTreesCount;
        }

        private static bool TreeVisible(char[,] trees, int i, int a)
        {
            int treeSize = trees[i, a] - '0';
            int treeToCheck;
            bool seenFromLeft = true;
            bool seenFromRight = true;
            bool seenFromTop = true;
            bool seenFromBelow = true;

            for(int treesToLeft = 0; treesToLeft < a; treesToLeft++)
            {
                treeToCheck = trees[i, treesToLeft] - '0';
                if(treeToCheck >= treeSize)
                {
                    seenFromLeft = false;
                    break;
                }
            }
            for (int treesToRight = trees.GetLength(1) - 1; treesToRight > a; treesToRight--)
            {
                treeToCheck = trees[i, treesToRight] - '0';
                if (treeToCheck >= treeSize)
                {
                    seenFromRight = false;
                    break;
                }
            }
            for (int treesTop = 0; treesTop <i; treesTop++)
            {
                treeToCheck = trees[treesTop, a] - '0';
                if (treeToCheck >= treeSize)
                {
                    seenFromTop = false;
                    break;
                }
            }
            for (int treesBelow = trees.GetLength(0) - 1; treesBelow > i; treesBelow--)
            {
                treeToCheck = trees[treesBelow, a] - '0';
                if (treeToCheck >= treeSize)
                {
                    seenFromBelow = false;
                    break;
                }
            }
            return seenFromLeft || seenFromRight || seenFromBelow || seenFromTop;
        }

        private static int GetMaxScenicScore(char[,] trees)
        {
            int maxScenicScore = 0;
            int score = 0;
            for (int i = 1; i < trees.GetLength(0) - 1; i++)
            {
                for (int a = 1; a < trees.GetLength(1) - 1; a++)
                {
                    score = GetScenicSCore(trees, i, a);
                    if (score > maxScenicScore)
                    {
                        maxScenicScore = score;
                    }
                }
            }
            return maxScenicScore;
        }

        private static int GetScenicSCore(char[,] trees, int i, int a)
        {
            int treeSize = trees[i, a] - '0';
            int treeToCheck;
            int seenFromLeft = 0;
            int seenFromRight = 0;
            int seenFromTop = 0;
            int seenFromBelow = 0;

            for (int treesToLeft = a - 1; treesToLeft >= 0; treesToLeft--)
            {
                seenFromLeft += 1;
                treeToCheck = trees[i, treesToLeft] - '0';
                if (treeToCheck >= treeSize) break;
               
            }
            for (int treesToRight = a + 1; treesToRight < trees.GetLength(0); treesToRight++)
            {
                seenFromRight += 1;
                treeToCheck = trees[i, treesToRight] - '0';
                if(treeToCheck >= treeSize) break;
            }
            for (int treesTop = i  - 1; treesTop >= 0; treesTop--)
            {
                seenFromTop += 1;
                treeToCheck = trees[treesTop, a] - '0';
                if(treeToCheck >= treeSize) break;
            }
            for (int treesBelow = i + 1; treesBelow < trees.GetLength(1); treesBelow++)
            {
                seenFromBelow += 1;
                treeToCheck = trees[treesBelow, a] - '0';
                if(treeToCheck >= treeSize) break;
            }

            return seenFromLeft * seenFromRight * seenFromTop * seenFromBelow;
        }
    }
}
