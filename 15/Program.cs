using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode15
{
    internal class Program
    {
        static ArrayList sensorBeacons = new ArrayList();
        static int maxY = int.MinValue;
        public static int maxX= int.MinValue;
        static int minY = int.MaxValue;
        public static int minX = int.MaxValue;
        static int mapSize = 4000000;

        static void Main(string[] args)
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Assets\sensorBeacons.txt");
            string[] lines = System.IO.File.ReadAllLines(path);
            ParseLine(lines);
            int numNotPosiblePosition = GetNotPosiblePositionsForY(10);
            Console.WriteLine("num of taken positions = {0}", numNotPosiblePosition);
            //GetFrequency();
            int numOfLines = mapSize / 10;
            Thread[] threads = new Thread[10];
            for(int i = 0; i < 10; i++)
            {
                int start = i * numOfLines;
                threads[i] = new Thread(() => GetFrequencyThread(start, numOfLines));
                threads[i].Start();
            }


        }

        private static void ParseLine(string[] lines)
        {
            foreach(string line in lines)
            {
                sensorBeacons.Add(new SeansorBeacon(line));
                CheckForX(line);
                CheckForY(line);
                //Console.WriteLine("maxY ={0}, minY={1}, maxX={2}, minX={3}", maxY, minY, maxX, minX);
            }
        }

        private static void CheckForY(string line)
        {
            String[] data = line.Split(' ');
            String firstYString = data[3].Split('=')[1];
            firstYString = firstYString.Substring(0, firstYString.Length - 1);
            int firstY = int.Parse(firstYString);

            if(firstY > maxY) maxY = firstY;
            if(firstY < minY) minY = firstY;

            String secondYString = data[9].Split('=')[1];
            int secondY = int.Parse(secondYString);

            if(secondY > maxY) maxY = secondY;
            if(secondY < minY) minY = secondY;
            //Console.WriteLine("firstY={0} secondY={1}",firstY, secondY);
        }

        private static void CheckForX(string line)
        {
            String[] data = line.Split(' ');
            String firstXString = data[2].Split('=')[1];
            firstXString = firstXString.Substring(0, firstXString.Length - 1);
            int firstX = int.Parse(firstXString);

            if (firstX > maxX) maxX = firstX;
            if (firstX < minX) minX = firstX;

            String secondXString = data[8].Split('=')[1];
            secondXString = secondXString.Substring(0, secondXString.Length - 1);
            int secondX = int.Parse(secondXString);

            if (secondX > maxX) maxX = secondX;
            if (secondX < minX) minX = secondX;

            //Console.WriteLine("firstX={0} secondX={1}", firstX, secondX);
        }

        private static int GetNotPosiblePositionsForY(int y)
        {
            int sizeOfX = Math.Abs(minX) + maxX;
            //Console.WriteLine("Size of X = {0}", sizeOfX);
            int offset = 0;
            if (minX < 0) offset = minX;
            bool[] linePositons = new bool[sizeOfX];
            Array.Fill(linePositons, false);
            int takenPositons = 0;

            int[] sensorBeaconCoverageForLine = new int[sizeOfX];
            foreach(SeansorBeacon seansorBeacon in sensorBeacons)
            {
                sensorBeaconCoverageForLine = seansorBeacon.GetCoverageForLine(y);
                //Console.WriteLine("sensorCovrage = {0}", sensorBeaconCoverageForLine);
                if(sensorBeaconCoverageForLine != null)
                {
                    for(int xPosition = sensorBeaconCoverageForLine[0] - offset; xPosition <= sensorBeaconCoverageForLine[1] - offset; xPosition++)
                    {
                        if (!DoPositionContainBeacon(y, xPosition + offset)) linePositons[xPosition] = true;
                    }
                }
            }
            foreach(bool position in linePositons)
            {
                if(position) takenPositons++;
            }
            return takenPositons;
        }

        private static bool DoPositionContainBeacon(int y, int x)
        {
            foreach(SeansorBeacon seansorBeacon in sensorBeacons)
            {
                if (seansorBeacon.IsBeaconAtCordinates(y, x)) return true;
            }
            return false;
        }

        private static void GetFrequency()
        {
            int[] positionOfBeacon = new int[2];
            for(int y = 0; y <= mapSize; y++)
            {
                //Console.WriteLine("calc for line = {0}", y);
                positionOfBeacon[1] = GetPositionOfDistressBeacon(y);
                if(positionOfBeacon[1] >= 0)
                {
                    Console.WriteLine("found position {0}", positionOfBeacon[1]);
                    positionOfBeacon[0] = y;
                    break;
                }
            }
            Console.WriteLine("Position of beacon is {0},{1}", positionOfBeacon[0], positionOfBeacon[1]);
            UInt64 frequecy = (ulong)positionOfBeacon[1] * (ulong)4000000 + (ulong)positionOfBeacon[0];
            Console.WriteLine("frequency = {0}", frequecy);
        }

        private static void GetFrequencyThread(int start, int numberOfLinnes)
        {
            int[] positionOfBeacon = new int[2];
            Console.WriteLine("starting search from {0} and searching to {1}", start, numberOfLinnes + start);
            for (int y = start; y <= start + numberOfLinnes; y++)
            {
                //Console.WriteLine("calc for line = {0}", y);
                positionOfBeacon[1] = GetPositionOfDistressBeacon(y);
                if (positionOfBeacon[1] >= 0)
                {
                    Console.WriteLine("found position {0}", positionOfBeacon[1]);
                    positionOfBeacon[0] = y;
                    Console.WriteLine("Position of beacon is {0},{1}", positionOfBeacon[0], positionOfBeacon[1]);
                    UInt64 frequecy = (ulong)positionOfBeacon[1] * (ulong)4000000 + (ulong)positionOfBeacon[0];
                    Console.WriteLine("frequency = {0}", frequecy);
                    break;
                }
                else positionOfBeacon[0] = -1;
            }
            

        }

        private static int GetPositionOfDistressBeacon(int y)
        {
            int sizeOfX = Math.Abs(minX) + maxX;
            int offset = 0;
            if (minX < 0) offset = minX;
            bool[] linePositons = new bool[mapSize];
            Array.Fill(linePositons, false);

            int[] sensorBeaconCoverageForLine = new int[sizeOfX];
            foreach (SeansorBeacon seansorBeacon in sensorBeacons)
            {
                sensorBeaconCoverageForLine = seansorBeacon.GetCoverageForLine(y);
                //Console.WriteLine("sensorCovrage = {0}", sensorBeaconCoverageForLine);
                if (sensorBeaconCoverageForLine != null)
                {
                    for (int xPosition = sensorBeaconCoverageForLine[0]; xPosition <= sensorBeaconCoverageForLine[1]; xPosition++)
                    {
                        //Console.Write("{0},{1} ", xPosition, y);
                        if (xPosition >= 0 && xPosition < mapSize)
                        {
                            linePositons[xPosition] = true;
                            //Console.Write("{0},{1} ", xPosition, y);
                        }
                    }
                    //Console.WriteLine();
                }
            }
            for (int x = 0; x < linePositons.Length; x++)
            {
                if (!linePositons[x])
                {
                    //Console.WriteLine("position false = {0}", x);
                    return x;
                }
            }
            return -1;
        }
    }
}
