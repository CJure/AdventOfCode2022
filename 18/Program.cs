// See https://aka.ms/new-console-template for more information
using System.Collections;
using System.Reflection;

internal class Program
{
    static bool[,,] cubes = new bool[25,25,25];
    static ArrayList insideCubes = new ArrayList();
    static ArrayList visitedCubes = new ArrayList();
    static int numberOfSides;

    static void Main(string[] args)
    {
        string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Assets\cubes.txt");
        string[] lines = System.IO.File.ReadAllLines(path);
        CreateCubes(lines);
        numberOfSides = CountSides();
        Console.WriteLine("Sides count {0}", numberOfSides);
        FindInsideCubes(); //got hind about dfs on reedit
        Console.WriteLine("Sides count {0}", numberOfSides);


    }

    private static void CreateCubes(string[] lines)
    {
        foreach (string line in lines)
        {
            String[] data = line.Trim().Split(",");
            int y = int.Parse(data[0]) + 1; //we add 1 so that in seccond part we know that 0 there is nothing
            int x = int.Parse(data[1]) + 1; //we add 1 so that in seccond part we know that 0 there is nothing
            int z = int.Parse(data[2]) + 1; //we add 1 so that in seccond part we know that 0 there is nothing
            cubes[y, x, z] = true;
        }
    }

    private static int CountSides()
    {
        int sideCount = 0;
        bool lastCubeState = false;
        bool currentCube;

        for(int y = 0; y < cubes.GetLength(0); y++)
        {
            for (int z = 0; z < cubes.GetLength(2); z++)
            {
                for (int x = 0; x < cubes.GetLength(1); x++)
                {
                    currentCube = cubes[y, x, z];
                    if (currentCube && !lastCubeState) sideCount++;
                    else if(!currentCube && lastCubeState) sideCount++;
                    lastCubeState = currentCube;
                }
            }
        }

        for (int y = 0; y < cubes.GetLength(0); y++)
        {
            for (int x = 0; x < cubes.GetLength(1); x++)
            {
                for (int z = 0; z < cubes.GetLength(2); z++)
                {
                    currentCube = cubes[y, x, z];
                    if (currentCube && !lastCubeState) sideCount++;
                    else if (!currentCube && lastCubeState) sideCount++;
                    lastCubeState = currentCube;
                }
            }
        }

        for (int x = 0; x < cubes.GetLength(1); x++)
        {
            for (int z = 0; z < cubes.GetLength(2); z++)
            {
                for (int y = 0; y < cubes.GetLength(0); y++)
                {
                    currentCube = cubes[y, x, z];
                    if (currentCube && !lastCubeState) sideCount++;
                    else if (!currentCube && lastCubeState) sideCount++;
                    lastCubeState = currentCube;
                }
            }
        }

        return sideCount;
    }

    private static void FindInsideCubes()
    {
        for (int y = 0; y < cubes.GetLength(0); y++)
        {
            for (int z = 0; z < cubes.GetLength(2); z++)
            {
                for (int x = 0; x < cubes.GetLength(1); x++)
                {
                    if (cubes[y, x, z] == false)
                    {
                        if (!CanCubeReach000(y, x, z))
                        {
                            //insideCubes.Add(y + "" + x + "" + z);
                            //Console.WriteLine("inside cube = {0},{1},{2}", y, x, z);
                            RemoveSidesForInsideCube(y,x,z);
                        }
                        else
                        {
                            //Console.WriteLine("cube can reach outside = {0},{1},{2}", y, x, z);
                        }
                    }
                }
            }
        }
}
    private static bool CanCubeReach000(int y, int x, int z)
    {
        //Console.WriteLine("checking cube = {0},{1},{2}", y, x, z);
        visitedCubes.Clear();
        visitedCubes.Add(x + "" + y + "" + z);
        bool canReachOutside = CanReachOutside(y, x, z);
        return canReachOutside;
    }

    private static bool CanReachOutside(int y, int x, int z)
    {
        //Console.WriteLine("CanReachOutside = {0},{1},{2}", y, x, z);
        //Console.WriteLine("visited nodes = {0}", visitedCubes.Count);
        bool yplus1 = false;
        bool yminus1 = false;
        bool xplus1 = false;
        bool xminus1 = false;
        bool zplus1 = false;
        bool zminus1 = false;
        visitedCubes.Add(y + "" + x + "" + z);
        if(y == 0 || x == 0 || z == 0) return true;
        if(y == 25 || x == 25 || z == 25) return true;
        if (cubes[y, x, z] == true) return false;
        if(y >= 25 || x >= 25 || z >= 25) return false;
        if(y < 0 || x < 0 || z < 0) return false;
        if (!visitedCubes.Contains((y + 1) + "" + x + "" + z)) yplus1 = CanReachOutside((y + 1), x, z);
        if(yplus1 == true) return true;
        if (!visitedCubes.Contains((y - 1) + "" + x + "" + z)) yminus1 = CanReachOutside((y - 1), x, z);
        if(yminus1 == true) return true;
        if (!visitedCubes.Contains(y + "" + (x + 1) + "" + z)) xplus1 = CanReachOutside(y, (x + 1), z);
        if(xplus1 == true) return true;
        if (!visitedCubes.Contains(y + "" + (x - 1) + "" + z)) xminus1 = CanReachOutside(y, (x - 1), z);
        if(xminus1 == true) return true;
        if (!visitedCubes.Contains(y + "" + x + "" + (z + 1))) zplus1 = CanReachOutside(y, x, (z + 1));
        if(zplus1 == true) return true;
        if (!visitedCubes.Contains(y  + "" + x + "" + (z - 1))) zminus1 = CanReachOutside(y, x, (z - 1));
        if(zminus1 == true) return true;
        return false;
    }

    private static void RemoveSidesForInsideCube(int y, int x, int z)
    {
        if (cubes[y + 1, x, z] == true) numberOfSides--;
        if (cubes[y - 1, x, z] == true) numberOfSides--;
        if (cubes[y, x + 1, z] == true) numberOfSides--;
        if (cubes[y, x - 1, z] == true) numberOfSides--;
        if (cubes[y, x, z + 1] == true) numberOfSides--;
        if (cubes[y, x, z -1] == true) numberOfSides--;
    }
}

       


