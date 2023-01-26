using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode16
{
    internal class ValvePath
    {
        public int pressure = 0;
        public ArrayList visitedValves = new ArrayList();
        Valve currentValve;

        public void UpdatePressure()
        {
            foreach (Valve valve in visitedValves)
            {
                pressure += valve.flowRate;
            }
        }

        public void SetCurrentValve(Valve valve)
        {
            this.currentValve = valve;
        }

        internal void Tick()
        {
            UpdatePressure();
            if(visitedValves.Count < Program.valves.Count) MoveToNextValveAndCreateNewPaths();
        }

        private void MoveToNextValveAndCreateNewPaths()
        {
            string[] neighbourValves = (string[])currentValve.connectedValves.ToArray(typeof(string));
            if(!visitedValves.Contains(currentValve))visitedValves.Add(currentValve);
            currentValve = Program.valves[neighbourValves[0]];
            ValvePath newPath;
            for (int i = 1; i < neighbourValves.Length; i++)
            {
                newPath = new ValvePath();
                newPath.currentValve = Program.valves[neighbourValves[i]];
                newPath.pressure = pressure;
                foreach(Valve valve in visitedValves)
                {
                    newPath.visitedValves.Add(valve);
                }
                Program.newPaths.Add(newPath);
            }
        }
    }
}
