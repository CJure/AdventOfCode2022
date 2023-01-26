using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection;

namespace AdventOfCode11
{
    internal class Program
    {
        public static Dictionary<string, Monkey> monkeys = new Dictionary<string, Monkey>();
        public static Dictionary<string, MonkeyIgnoreRelief> monkeysNoRelief = new Dictionary<string, MonkeyIgnoreRelief>();

        static void Main(string[] args)
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Assets\monkeys.txt");
            string[] lines = System.IO.File.ReadAllLines(path);
            //lines = lines.Where(x => !string.IsNullOrEmpty(x)).ToArray();
            Monkey[] monkeysArray = MonkeyParser.GetMonkeysForLines(lines);
            foreach (Monkey currentMonkey in monkeysArray)
            {
                //Console.WriteLine("Adding monkey {0}",currentMonkey.GetName());
                monkeys[currentMonkey.GetName()] = currentMonkey;
            }
            MonkeyTurns(lines);
            Console.WriteLine("Monkey buissnes {0}", GetMonkeyBuissnes());
            MonkeyIgnoreRelief[] monkeysArrayNoReleif = MonkeyParser.GetMonkeysForLinesNoReleif(lines);
            
            foreach (MonkeyIgnoreRelief currentMonkey in monkeysArrayNoReleif)
            {
                //Console.WriteLine("Adding monkey {0}", currentMonkey.GetName());
                monkeysNoRelief[currentMonkey.GetName()] = currentMonkey;
            }
            MonkeyTurnsIncrement();
            Console.WriteLine("Monkey buissnes {0}", GetMonkeyBuissnesIgnore());

        }

        private static void MonkeyTurns(string[] lines)
        {
            String name;
            Monkey currentMonkey;
            for(int i = 0; i < 20; i ++)
            {
                //Console.WriteLine("turn {0}", i);
                for(int a = 0; a < monkeys.Count; a++)
                {
                    name = "Monkey " + a;
                    //Console.WriteLine("Doing turn for monkey {0}", name);
                    currentMonkey = monkeys[name];
                    currentMonkey.InspectAndThrowItems();
                }
            }    
        }

        private static void MonkeyTurnsIncrement()
        {
            String name;
            MonkeyIgnoreRelief currentMonkey;
            Console.WriteLine("monkeyIgnore size = {0}", monkeysNoRelief.Count);
            for (int i = 0; i < 10000; i++)
            {
                //Console.WriteLine("ignore turn {0}", i);
                if (i == 1) GetMonkeyBuissnesIgnore();
                if (i == 20) GetMonkeyBuissnesIgnore();
                if (i == 1000) GetMonkeyBuissnesIgnore();
                if (i == 2000) GetMonkeyBuissnesIgnore();
                if (i == 3000) GetMonkeyBuissnesIgnore();
                if (i == 4000) GetMonkeyBuissnesIgnore();
                //Console.WriteLine("turn {0}", i);
                for (int a = 0; a < monkeys.Count; a++)
                {
                    name = "Monkey " + a;
                    //Console.WriteLine("Doing turn for monkey {0}", name);
                    currentMonkey = monkeysNoRelief[name];
                    currentMonkey.InspectAndThrowItemsWihtoutDivide();
                }
            }
        }

        private static double GetMonkeyBuissnesIgnore()
        {
            MonkeyIgnoreRelief monkey;
            double max = 0;
            double seccondMax = 0;
            foreach (KeyValuePair<string, MonkeyIgnoreRelief> entry in monkeysNoRelief)
            {
                monkey = entry.Value;
                Console.WriteLine("{0} buissnes {1}", monkey.GetName(), monkey.GetItemsInspected());
                if (monkey.GetItemsInspected() > max)
                {
                    seccondMax = max;
                    max = monkey.GetItemsInspected();
                }
                else if(monkey.GetItemsInspected() > seccondMax) seccondMax = monkey.GetItemsInspected();

            }
            Console.WriteLine("max = {0} seccondMax = {1}", max, seccondMax);
            return seccondMax * max;
        }

        private static double GetMonkeyBuissnes()
        {
            Monkey monkey;
            double max = 0;
            double seccondMax = 0;
            foreach (KeyValuePair<string, Monkey> entry in monkeys)
            {
                monkey = entry.Value;
                Console.WriteLine("{0} buissnes {1}", monkey.GetName(), monkey.GetItemsInspected());
                if (monkey.GetItemsInspected() > max)
                {
                    seccondMax = max;
                    max = monkey.GetItemsInspected();
                }

            }
            Console.WriteLine("max = {0} seccondMax = {1}", max, seccondMax);
            return seccondMax * max;
        }
        

    }
}
