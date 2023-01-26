using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode16
{
    internal class Valve
    {
        public int flowRate;
        public ArrayList connectedValves = new ArrayList();
        public string name;

        public Valve(string line)
        {
            ParseLine(line);
        }

        private void ParseLine(string line)
        {
            int indexName = 1;
            int indexFlowRate = 4;
            int indexConnectedValvesStart = 9;
            string[] data = line.Split(' ');
            name = data[indexName];
            string flowRateString = data[indexFlowRate];
            Console.WriteLine(flowRateString.Length);
            flowRateString = flowRateString.Substring(5, flowRateString.Length - 6);
            flowRate = int.Parse(flowRateString);
            for(int i = indexConnectedValvesStart; i < data.Length; i++)
            {
                data[i] = data[i].Replace(",", string.Empty);
                connectedValves.Add(data[i]);
            }
        }

        override
        public string ToString()
        {
            return ("Valve " + name + " has flow reate " + flowRate);
        }
    }
}
