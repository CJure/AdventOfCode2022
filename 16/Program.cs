// See https://aka.ms/new-console-template for more information
using AdventOfCode16;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

internal class Program
{
    public static Dictionary<string, Valve> valves = new Dictionary<string, Valve>();
    public static ArrayList paths = new ArrayList();
    public static ArrayList newPaths = new ArrayList();

    static void Main(string[] args)
    {
        string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Assets\valves.txt");
        string[] lines = System.IO.File.ReadAllLines(path);

        Console.WriteLine("file loaded");
        ParseData(lines);
        Ticks();
        OutputPressureForPaths();
    }

    static void ParseData(String[] lines)
    {
        Valve tempValve;
        foreach (string line in lines)
        {
            tempValve = new Valve(line);
            valves[tempValve.name] = tempValve;
        }
        foreach(var item in valves)
        {
            Valve valve = item.Value;
            Console.WriteLine(valve.name);
        }
    }

    static void Ticks()
    {
        int minutes = 30;
        ValvePath firstPath = new ValvePath();
        firstPath.SetCurrentValve(valves["AA"]);
        paths.Add(firstPath);
        //ValvePath[] pathsArray;
        ValvePath tempPath;
        for (int minute = 1; minute <= minutes; minute++)
        {
            Console.WriteLine("tick {0} and number of paths {1}", minute, paths.Count);
            //pathsArray = (ValvePath[])paths.ToArray(typeof(ValvePath));
            for(int i = 0; i < paths.Count; i++)
            {
                tempPath = (ValvePath)paths[i];
                tempPath.Tick();
                if (minute > 10 && tempPath.visitedValves.Count < 4) paths.RemoveAt(i);
                if (minute > 15 && tempPath.visitedValves.Count < 8) paths.RemoveAt(i);
                if (minute > 20 && tempPath.visitedValves.Count < 10) paths.RemoveAt(i);

            }
            Console.WriteLine("tick {0} and number of paths {1}", minute, paths.Count);
            foreach (ValvePath path in newPaths)
            {
                paths.Add(path);
            }
            Console.WriteLine("tick {0} and number of paths {1}", minute, paths.Count);
            newPaths.Clear();

            //foreach (ValvePath path in pathsArray)
            //{
            //    path.Tick();
            //    if (path.visitedValves.Count < minute / 2) paths.Remove(path);
            //}
            
        }
    }

    private static void OutputPressureForPaths()
    {
        int maxPressure = 0;
        foreach(ValvePath path in paths)
        {
            if(path.pressure > maxPressure) maxPressure = path.pressure;
        }
        Console.WriteLine("Path pressure = {0}", maxPressure);
    }
}



