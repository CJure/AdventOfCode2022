using System;
using System.IO;
using System.Reflection;

namespace RockPaperScissors
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Assets\rcp.txt");
            string[] lines = System.IO.File.ReadAllLines(path);
            int resultFirstPuzle = 0;
            int resultSecondPuzle = 0;
            String firstChar;
            String secondChar;
            String[] values;
            foreach(string line in lines)
            {
                values = line.Split(" ");
                firstChar = values[0];
                secondChar = values[1];
                resultFirstPuzle += CalulcateResultForValues(firstChar, secondChar);
                resultSecondPuzle += CalulcateResultForMatch(firstChar, secondChar);
            }
            Console.WriteLine("first puzle = {0}", resultFirstPuzle);
            Console.WriteLine("seccond puzle = {0}", resultSecondPuzle);
        }

        private static int CalulcateResultForValues(string firstChar, string secondChar)
        {
            int result = 0;
            result += GetMatchPoints(firstChar, secondChar);
            result += GetHandPoints(secondChar);
            return result;
        }

        private static int GetMatchPoints(string firstChar, string secondChar)
        {
            if(firstChar == "A")
            {
                if (secondChar == "X") return 3;
                else if(secondChar == "Z") return 0;
                return 6;
            }
            else if(firstChar == "B")
            {
                if (secondChar == "Y") return 3;
                else if (secondChar == "X") return 0;
                return 6;
            }
            else if (firstChar == "C")
            {
                if (secondChar == "Z") return 3;
                else if (secondChar == "Y") return 0;
                return 6;
            }
            return 0;
        }

        private static int GetHandPoints(string secondChar)
        {
            switch(secondChar)
            { 
                case "X": return 1;
                case "Y": return 2;
                case "Z": return 3;
                default: return 0;
            }
        }

        private static int CalulcateResultForMatch(string firstChar, string secondChar)
        {
            string chosenChar = GetCharForRound(firstChar, secondChar);
            int result = 0;
            result += GetMatchPoints(firstChar, chosenChar);
            result += GetHandPoints(chosenChar);
            return result;
        }

        private static string GetCharForRound(string firstChar, string secondChar)
        {
            if (firstChar == "A")
            {
                if (secondChar == "Y") return "X"; //draw
                else if (secondChar == "Z") return "Y"; //win
                else return "Z";
            }
            else if (firstChar == "B")
            {
                if (secondChar == "Y") return "Y"; //draw
                else if (secondChar == "Z") return "Z"; //win
                else return "X";
            }
            else if (firstChar == "C")
            {
                if (secondChar == "Y") return "Z"; //draw
                else if (secondChar == "Z") return "X"; //win
                else return "Y";
            }
            return null;
        }
    }
}
