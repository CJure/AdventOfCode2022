using System;
using System.IO;
using System.Reflection;

namespace AdventOfCode10
{
    internal class Program
    {
        static int tickCount = 0;
        static int registerValue = 1;
        static int sumOfValues = 0;
        static int pixelPositon = 0;

        static void Main(string[] args)
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Assets\commands.txt");
            string[] lines = System.IO.File.ReadAllLines(path);
            ProccessLines(lines);
            //Console.WriteLine("SumOfValues {0}", sumOfValues);
        }

        private static void ProccessLines(string[] lines)
        {
            String[] data;
            foreach(string line in lines)
            {
                data = line.Split(" ");
                if (data.Length == 2)
                {
                    IncreaseTick();
                    IncreaseTick();
                    AddValueToRegister(data[1]);
                }
                else IncreaseTick();
            }
        }

        private static void IncreaseTick()
        {
            tickCount++;
            DrawPixel();
            pixelPositon++;
            //Console.WriteLine("Tick {0}, reg value {1}", tickCount, registerValue);
            if (tickCount == 20 || (tickCount - 20) % 40 == 0)
            {
                //Console.WriteLine("Value on tick {0} is {1}", tickCount, RegisterValueForTick());
                sumOfValues += RegisterValueForTick();
            }
            else if (tickCount % 40 == 0)
            {
                Console.WriteLine();
                pixelPositon = 0;
            }
        }

        private static void DrawPixel()
        {
            //Console.WriteLine("Pixel position {0}", pixelPositon);
            bool litPixel = pixelPositon >= registerValue - 1 && pixelPositon <= registerValue + 1;
            if (litPixel) Console.Write("#");
            else Console.Write(".");
        }

        private static void AddValueToRegister(string v)
        {
            int value = int.Parse(v);
            registerValue += value;
        }

        private static int RegisterValueForTick()
        {
            return registerValue* tickCount;
        }
    }
}
