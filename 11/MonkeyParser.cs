using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode11
{
    internal class MonkeyParser
    {
        internal static Monkey[] GetMonkeysForLines(string[] lines)
        {
            Console.WriteLine("nummber of lines {0}", lines.Length);
            int numberOfMonkeys = (lines.Length + 1) / 8 + 1;
            Monkey[] monkeys = new Monkey[numberOfMonkeys];
            Monkey monkey;

            for (int i = 0; i < numberOfMonkeys; i++)
            {
                Console.WriteLine("creating monkey {0}", i);
                Console.WriteLine("line {0}", (lines[i * 7]));
                monkey = new Monkey();
                monkey.SetName(GetName(lines[i * 7]));
                Console.WriteLine("items {0}", GetItems(lines[i * 7 + 1]).Length);
                monkey.SetItems(GetItems(lines[i * 7 + 1]));
                monkey.SetOperation(GetOperation(lines[i * 7 + 2]));
                monkey.SetOperationValue(GetOperationValue(lines[i * 7 + 2]));
                monkey.SetDivideTest(GetDivideTest(lines[i * 7 + 3]));
                monkey.SetTrueMonkey(GetMonkeyName(lines[i * 7 + 4]));
                monkey.SetFalseMonkey(GetMonkeyName(lines[i * 7 + 5]));
                monkeys[i] = monkey;
            }
            return monkeys;
        }

        internal static MonkeyIgnoreRelief[] GetMonkeysForLinesNoReleif(string[] lines)
        {
            Console.WriteLine("nummber of lines {0}", lines.Length);
            int numberOfMonkeys = (lines.Length + 1) / 8 + 1;
            MonkeyIgnoreRelief[] monkeys = new MonkeyIgnoreRelief[numberOfMonkeys];
            MonkeyIgnoreRelief monkey;

            for (int i = 0; i < numberOfMonkeys; i++)
            {
                Console.WriteLine("creating monkey {0}", i);
                Console.WriteLine("line {0}", (lines[i * 7]));
                monkey = new MonkeyIgnoreRelief();
                monkey.SetName(GetName(lines[i * 7]));
                Console.WriteLine("items {0}", GetItems(lines[i * 7 + 1]).Length);
                monkey.SetItems(GetItems(lines[i * 7 + 1]));
                monkey.SetOperation(GetOperation(lines[i * 7 + 2]));
                monkey.SetOperationValue(GetOperationValue(lines[i * 7 + 2]));
                monkey.SetDivideTest(GetDivideTest(lines[i * 7 + 3]));
                monkey.SetTrueMonkey(GetMonkeyName(lines[i * 7 + 4]));
                monkey.SetFalseMonkey(GetMonkeyName(lines[i * 7 + 5]));
                monkeys[i] = monkey;
            }
            return monkeys;
        }

        private static string GetName(string lineWithName)
        {
            return lineWithName.Substring(0, lineWithName.Length - 1);
        }

        private static string[] GetItems(string linev)
        {
            string[] describtionAnData = linev.Split(':');
            string[] items = describtionAnData[1].Trim().Split(',');
            return items;
        }

        private static string GetOperation(string line)
        {
            string[] data = line.Split(" ");
            if (data[data.Length - 1] == "old") return "square";
            return data[data.Length - 2];
        }

        private static UInt64 GetOperationValue(string line)
        {
            string[] data = line.Split(" ");
            if (data[data.Length - 1] == "old") return 1;
            return UInt64.Parse(data[data.Length - 1]);
        }

        private static UInt64 GetDivideTest(string line)
        {
            string[] data = line.Split(" ");
            return UInt64.Parse(data[data.Length - 1]);
        }

        private static string GetMonkeyName(string line)
        {
            string[] data = line.Split(" ");
            return "Monkey " + data[data.Length - 1];
        }
    }
}
