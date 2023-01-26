using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace AdventOfCode5
{
    internal class Program
    {
        static Stack[] stacks = new Stack[9];
        static Stack[] stacks2 = new Stack[9];

        static void Main(string[] args)
        {
            InitStacks();
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Assets\containers.txt");
            string[] lines = System.IO.File.ReadAllLines(path);
            int indexEndOfStart = 0;
            bool startingContainersSet = false;
            foreach (string line in lines)
            {
                if (line.Length == 0)
                {
                    indexEndOfStart -= 1;
                    AddStartingContainers(lines, indexEndOfStart);
                    startingContainersSet = true;
                }
                else if (startingContainersSet)
                {
                    MoveContainers(line);
                    MoveContainersReverse(line);
                }
                indexEndOfStart += 1;
            }
            foreach (Stack stack in stacks)
            {
                Console.Write(stack.Pop() + "");
            }
            Console.WriteLine();
            foreach (Stack stack in stacks2)
            {
                 Console.Write(stack.Pop() + "");
            }
        }

        private static void InitStacks()
        {
            for (int i = 0; i < stacks.Length; i++)
            {
                stacks[i] = new Stack();
                stacks2[i] = new Stack();
            }
        }

        private static void AddStartingContainers(string[] lines, int startIndex)
        {
            String line;
            char[] startingContainerData;
            int indexContainerStart = 0;
            int indexContainerData = 0;
            char containerData;

            for (int lineIndex = startIndex; lineIndex >= 0; lineIndex--)
            {
                line = lines[lineIndex];
                startingContainerData = line.ToCharArray();
                for (int i = 0; i <= startingContainerData.Length / 4; i++)
                {
                    indexContainerStart = i * 4;
                    indexContainerData = i * 4 + 1;
                    if (startingContainerData[indexContainerStart] == '[')
                    {
                        containerData = startingContainerData[indexContainerData];
                        stacks[i].Push(containerData);
                        stacks2[i].Push(containerData);
                    }
                }
            }
        }

        private static void MoveContainers(string line)
        {
            int indexNumOfContainers = 1;  
            int indexFromPoistion = 3;
            int indexToPosition = 5;
            int numOfContainers = 0;
            int fromPosition = 0;
            int toPosition = 0;
            String[] data = line.Split(" ");
            numOfContainers = int.Parse(data[indexNumOfContainers]);
            fromPosition = int.Parse(data[indexFromPoistion]) - 1;
            toPosition = int.Parse(data[indexToPosition]) - 1;
            char container;
            for(int i = 0; i < numOfContainers; i++)
            {
                container = (char)stacks[fromPosition].Pop();
                stacks[toPosition].Push(container);
            }
        }

        private static void MoveContainersReverse(string line)
        {
            int indexNumOfContainers = 1;
            int indexFromPoistion = 3;
            int indexToPosition = 5;
            int numOfContainers = 0;
            int fromPosition = 0;
            int toPosition = 0;
            String[] data = line.Split(" ");
            numOfContainers = int.Parse(data[indexNumOfContainers]);
            fromPosition = int.Parse(data[indexFromPoistion]) - 1;
            toPosition = int.Parse(data[indexToPosition]) - 1;
            char container;
            Stack<char> tempStack = new Stack<char>();
            for (int i = 0; i < numOfContainers; i++)
            {
                container = (char)stacks2[fromPosition].Pop();
                tempStack.Push(container);
            }
            for(int a = 0; a < numOfContainers; a++)
            {
                container = (char)tempStack.Pop();
                stacks2[toPosition].Push(container);
            }
        }
    }
}
