using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace AdventOfCode12
{
    internal class Program
    {
        static Node startingNode;
        static Node endingNode;
        static ArrayList visitedNodes = new ArrayList();
        static Queue<Node> queue = new Queue<Node>();
        static Node[,] nodes;

        static void Main(string[] args)
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Assets\path.txt");
            string[] lines = System.IO.File.ReadAllLines(path);
            
            char[,] terrain = GetTerrain(lines);
            nodes = CreateNodes(terrain);

            int pathLength = GetPathLength();
            Console.WriteLine("length is {0}", pathLength);

            
            int pathLengthToA = GetPathLengthToA2();
            Console.WriteLine("length to a is {0}", pathLengthToA);
        }

        private static int GetPathLengthToA2()
        {
           
            ArrayList nodesWithA = GetAllA(nodes);
            Console.WriteLine("number of a nodes = {0}", nodesWithA.Count);
            int shortestaAdistance = int.MaxValue;
            int distance = int.MaxValue;
            foreach (Node node in nodesWithA)
            {
                visitedNodes.Clear();
                queue.Clear();
                node.distance = 0;
                startingNode = node;
                distance = GetPathLength();
                //Console.WriteLine("distance to A = {0}", distance);
                if (distance < shortestaAdistance)
                {
                    Console.WriteLine("shoretst disance = {0}", shortestaAdistance);
                    shortestaAdistance = distance;
                }

            }
            return shortestaAdistance;
        }

        private static Node[,] CreateNodes(char[,] terrain)
        {
            Node tempNode;
            Node[,] tempNodes = new Node[terrain.GetLength(0), terrain.GetLength(1)];
            int elevation;
            for(int y = 0; y < terrain.GetLength(0); y++)
            {
                for(int x = 0; x < terrain.GetLength(1); x++)
                {
                    if(terrain[y, x] == 'S')
                    {
                        elevation = 'a';
                        tempNode = new Node(y, x, elevation);
                        startingNode = tempNode;
                    }
                    else if(terrain[y, x] == 'E')
                    {
                        elevation = 'z';
                        tempNode = new Node(y, x, elevation);
                        endingNode = tempNode;
                    }
                    else
                    {
                        elevation = terrain[y, x];
                        tempNode = new Node(y, x, elevation);
                    }
                    tempNodes[y, x] = tempNode;
                }
            }
            return tempNodes;
        }

        private static char[,] GetTerrain(string[] lines)
        {
            char[,] tempTerrain = new char[lines.Length, lines[0].Length];
            int y = 0;
            foreach(string line in lines)
            {
                for(int x = 0; x < line.Length; x++)
                {
                    //Console.Write("line[i] = {0} with position {1},{2}", line[x], y, x);
                    tempTerrain[y,x] = line[x];
                }
                //Console.WriteLine();
                y++;
            }
            return tempTerrain;
        }

        private static int GetPathLength()
        {
            Node currentNode = null;
            ArrayList neighbourNodes;
            queue.Enqueue(startingNode);
            //Console.WriteLine("nodes.GetLength(0) = {0}, nodes.GetLength(1) = {1}", nodes.GetLength(0), nodes.GetLength(1));
            while (queue.Count > 0)
            {
                currentNode = queue.Dequeue();
                visitedNodes.Add(currentNode);
                //Console.WriteLine("current node is {0},{1}", currentNode.y, currentNode.x);
                if (currentNode == endingNode)
                {
                    return currentNode.distance;
                }
                neighbourNodes = GetNeighbourNodes(currentNode);
                AddNeigbourNodesToQueue(neighbourNodes, currentNode);
            }
            return int.MaxValue;
        }

        private static ArrayList GetNeighbourNodes(Node currentNode)
        {
            ArrayList neighbourNodes = new ArrayList();
            Node neigoburNode;
            if (currentNode.x > 0)
            {
                neigoburNode = nodes[currentNode.y, currentNode.x - 1];
                if (neigoburNode.elevation - currentNode.elevation < 2) neighbourNodes.Add(neigoburNode);
            }
            if (currentNode.y > 0)
            {
                neigoburNode = nodes[currentNode.y - 1, currentNode.x];
                if (neigoburNode.elevation - currentNode.elevation < 2) neighbourNodes.Add(neigoburNode);
            }
            if (currentNode.x < nodes.GetLength(1) - 1)
            {
                neigoburNode = nodes[currentNode.y, currentNode.x + 1];
                if (neigoburNode.elevation - currentNode.elevation < 2) neighbourNodes.Add(neigoburNode);
            }
            if (currentNode.y < nodes.GetLength(0) - 1)
            {
                neigoburNode = nodes[currentNode.y + 1, currentNode.x];
                if (neigoburNode.elevation - currentNode.elevation < 2) neighbourNodes.Add(neigoburNode);
            }
            return neighbourNodes;
        }

        private static void AddNeigbourNodesToQueue(ArrayList neighbourNodes, Node currentNode)
        {
            foreach (Node tempNode in neighbourNodes)
            {
                if (!visitedNodes.Contains(tempNode) && !queue.Contains(tempNode))
                {
                    //Console.WriteLine("Adding node {0},{1} to queue", tempNode.y, tempNode.x);
                    tempNode.distance = currentNode.distance + 1;
                    
                    queue.Enqueue(tempNode);
                }
            }
        }

        private static int GetPathLengthToA()
        {
            Node currentNode = null;
            ArrayList neighbourNodes;
            endingNode.distance = 0;
            queue.Enqueue(endingNode);
            //Console.WriteLine("nodes.GetLength(0) = {0}, nodes.GetLength(1) = {1}", nodes.GetLength(0), nodes.GetLength(1));
            while (queue.Count > 0)
            {
                currentNode = queue.Dequeue();
                visitedNodes.Add(currentNode);
                //Console.WriteLine("current node is {0},{1}", currentNode.y, currentNode.x);
                if (currentNode.elevation == 'a')
                {
                    return currentNode.distance;
                }
                neighbourNodes = GetNeighbourNodesForA(currentNode);
                AddNeigbourNodesToQueue(neighbourNodes, currentNode);
            }
            return -1;
        }

        private static ArrayList GetNeighbourNodesForA(Node currentNode)
        {
            ArrayList neighbourNodes = new ArrayList();
            Node neigoburNode;
            if (currentNode.x > 0)
            {
                neigoburNode = nodes[currentNode.y, currentNode.x - 1];
                if (neigoburNode.elevation - currentNode.elevation < 2 && neigoburNode.elevation - currentNode.elevation > -2) neighbourNodes.Add(neigoburNode);
            }
            if (currentNode.y > 0)
            {
                neigoburNode = nodes[currentNode.y - 1, currentNode.x];
                if (neigoburNode.elevation - currentNode.elevation < 2 && neigoburNode.elevation - currentNode.elevation > -2) neighbourNodes.Add(neigoburNode);
            }
            if (currentNode.x < nodes.GetLength(1) - 1)
            {
                neigoburNode = nodes[currentNode.y, currentNode.x + 1];
                if (neigoburNode.elevation - currentNode.elevation < 2 && neigoburNode.elevation - currentNode.elevation > -2) neighbourNodes.Add(neigoburNode);
            }
            if (currentNode.y < nodes.GetLength(0) - 1)
            {
                neigoburNode = nodes[currentNode.y + 1, currentNode.x];
                if (neigoburNode.elevation - currentNode.elevation < 2 && neigoburNode.elevation - currentNode.elevation > -2) neighbourNodes.Add(neigoburNode);
            }
            return neighbourNodes;
        }

        private static ArrayList GetAllA(Node[,] nodes)
        {
            ArrayList tempArrayList = new ArrayList();
            tempArrayList.Add(startingNode);
            foreach (Node node in nodes)
            {
                if(node.elevation == 'a') tempArrayList.Add(node);
            }
            return tempArrayList;
        }
    }
}
