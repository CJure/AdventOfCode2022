using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode15
{
    internal class SeansorBeacon
    {
        int beaconY;
        int beaconX;
        int sensorY;
        int sensorX;
        int range;

        public SeansorBeacon(string line)
        {
            GetY(line);
            GetX(line);
            range = Math.Abs(sensorX - beaconX) + Math.Abs(sensorY - beaconY);
            CheckForMaxMinX();
        }

        private void GetY(string line)
        {
            String[] data = line.Split(' ');

            String firstYString = data[3].Split('=')[1];
            firstYString = firstYString.Substring(0, firstYString.Length - 1);
            sensorY = int.Parse(firstYString);

            String secondYString = data[9].Split('=')[1];
            beaconY = int.Parse(secondYString);
        }

        private void GetX(string line)
        {
            String[] data = line.Split(' ');

            String firstXString = data[2].Split('=')[1];
            firstXString = firstXString.Substring(0, firstXString.Length - 1);
            sensorX = int.Parse(firstXString);

            String secondXString = data[8].Split('=')[1];
            secondXString = secondXString.Substring(0, secondXString.Length - 1);
            beaconX = int.Parse(secondXString);
        }

        private void CheckForMaxMinX()
        {
            int xPlus = sensorX + range;
            int xMinus = sensorX - range;
            if(xMinus < Program.minX) Program.minX = xMinus;
            if(xPlus > Program.maxX) Program.maxX = xPlus;
            //Console.WriteLine("xPlus = {0}, xMinus = {1}", xPlus, xMinus);
        }

        public int[] GetCoverageForLine(int y)
        {
            int[] coverage = new int[2];
            
            int r = range - Math.Abs(sensorY - y);
            if (r < 0) return null;
            coverage[0] = sensorX - r;
            coverage[1] = sensorX + r;
            //Console.WriteLine("Sensor = {0},{1} coverage = {2},{3} range = {4} r = {5}",sensorX ,sensorY, coverage[0], coverage[1], range, r);
            return coverage;
        }

        internal bool IsBeaconAtCordinates(int y, int x)
        {
            return y == beaconY && x == beaconX;
        }
    }
}
