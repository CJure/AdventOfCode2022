using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace AdventOfCode9
{
    internal class Program
    {
        static Dictionary<String, int> tailPositions = new Dictionary<string, int>();
        static Dictionary<String, int> tail9Positions = new Dictionary<string, int>();
        static Position tailPositon = new Position();
        static Position[] tailsPositon = new Position[9];
        //static Position[] tailsPositonOld = new Position[9];
        static Position headPosition = new Position();
        static string UP = "U";
        static string DOWN = "D";
        static string LEFT = "L";
        static string RIGHT = "R";


        static void Main(string[] args)
        {
            CreateTails();
            tailsPositon[0] = tailPositon;
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Assets\rope.txt");
            string[] lines = System.IO.File.ReadAllLines(path);
            foreach (string line in lines)
            {
                Console.WriteLine();
                Console.WriteLine(line);
                MoveCommand(line);
            }
            Console.WriteLine("Nummber of tail positions = {0}", tailPositions.Count);
            Console.WriteLine("Nummber of tail9 positions = {0}", tail9Positions.Count);
            
        }

        private static void CreateTails()
        {
            for(int i = 0; i < tailsPositon.Length; i++)
            {
                tailsPositon[i] = new Position();
                //tailsPositonOld[i] = new Position();
            }
        }

        private static void MoveCommand(string line)
        {
            string[] data = line.Split(' ');
            string direction = data[0];
            int distance = int.Parse(data[1]);
            for(int i = 0; i < distance; i++)
            {
                //SaveOldTailsPosition();
                //Console.WriteLine("direction = {0}", direction);
                MoveRopePart(headPosition, direction);
                Console.WriteLine("HEAD position = {0}", headPosition.PositionToString());
                MoveTailIfNeeded(direction);
                Console.WriteLine("TAIL 1 position = {0} tail 1 array={1}", tailPositon.PositionToString(), tailsPositon[0].PositionToString());
                MoveTailsIfNeeded(direction);
                if (tailPositions.ContainsKey(tailPositon.PositionToString())) tailPositions[tailPositon.PositionToString()] += 1;
                else tailPositions[tailPositon.PositionToString()] = 0;
                if (tail9Positions.ContainsKey(tailsPositon[8].PositionToString())) tail9Positions[tailsPositon[8].PositionToString()] += 1;
                else tail9Positions[tailsPositon[8].PositionToString()] = 0;
                Console.WriteLine("!!!TAIL 9 position = {0}", tailsPositon[8].PositionToString());
                DrawPosition();
            }
            Console.WriteLine("");
            Console.WriteLine("HEAD position = {0}", headPosition.PositionToString());
            Console.WriteLine("TAIL 1 position = {0}", tailPositon.PositionToString());
            
            for(int i = 1; i < tailsPositon.Length; i++)
            {
                Console.WriteLine("TAIL {0} position = {1}", i + 1, tailsPositon[i].PositionToString());
            }
        }

        //private static void SaveOldTailsPosition()
        //{
        //    for (int i = 0; i < tailsPositonOld.Length; i++)
        //    {
        //        tailsPositonOld[i].x = tailsPositon[i].x;
        //        tailsPositonOld[i].y = tailsPositon[i].y;
        //    }
        //}

        private static void MoveRopePart(Position ropePosition, string direction)
        {
            if (direction == UP) ropePosition.y += 1;
            else if (direction == DOWN) ropePosition.y -= 1;
            else if (direction == LEFT) ropePosition.x -= 1;
            else if (direction == RIGHT) ropePosition.x += 1;
        }

        private static void MoveTailIfNeeded(string direction)
        {
            int distanceBetwenPosition = GetDistanceBetwenPositions(tailPositon, headPosition);
            if (distanceBetwenPosition == 2 && !(DistanceOnAxis(tailPositon.x, headPosition.x) == 1 
                && DistanceOnAxis(tailPositon.y, headPosition.y) == 1)) MoveRopePart(tailPositon, direction);
            else if(distanceBetwenPosition == 3)
            {
                if(headPosition.x - tailPositon.x > 0) MoveRopePart(tailPositon, RIGHT);
                else MoveRopePart(tailPositon, LEFT);
                if(headPosition.y - tailPositon.y > 0) MoveRopePart(tailPositon, UP);
                else MoveRopePart(tailPositon, DOWN);
            }
        }

        private static void MoveTailsIfNeeded(string direction)
        {
            Position tailPositon;
            Position headPosition;
            //Position oldHeadPosition;
            for (int i = 1; i < tailsPositon.Length; i++)
            {
                headPosition = tailsPositon[i-1];
                //oldHeadPosition = tailsPositonOld[i - 1];
                tailPositon = tailsPositon[i];
                int distanceBetwenPosition = GetDistanceBetwenPositions(tailPositon, headPosition);
                Console.WriteLine("Distance betwen tail{0} and tail{1} is {2}", i + 1, i, distanceBetwenPosition);
                if (distanceBetwenPosition == 2 && !(DistanceOnAxis(tailPositon.x, headPosition.x) == 1
                    && DistanceOnAxis(tailPositon.y, headPosition.y) == 1))
                {
                    MoveRopePart(tailPositon, GetDirection(headPosition,tailPositon));
                    //tailPositon.y = oldHeadPosition.y;
                    //tailPositon.x = oldHeadPosition.x;
                }
                else if (distanceBetwenPosition >= 3)
                {
                    //tailPositon.y = oldHeadPosition.y;
                    //tailPositon.x = oldHeadPosition.x;
                    if (headPosition.x - tailPositon.x > 0) MoveRopePart(tailPositon, RIGHT);
                    else MoveRopePart(tailPositon, LEFT);
                    if (headPosition.y - tailPositon.y > 0) MoveRopePart(tailPositon, UP);
                    else MoveRopePart(tailPositon, DOWN);
                }
                Console.WriteLine("TAIL {0} position = {1}", i + 1, tailsPositon[i].PositionToString());
            }
        }

        private static string GetDirection(Position headPosition, Position tailPositon)
        {
            Console.WriteLine("xD={0}, yD={1}", headPosition.x - tailPositon.x, headPosition.y - tailPositon.y);
            if (headPosition.y - tailPositon.y > 1) return UP;
            if (headPosition.y - tailPositon.y < -1) return DOWN;
            if (headPosition.x - tailPositon.x > 1) return RIGHT;
            else return LEFT;
        }

        private static int GetDistanceBetwenPositions(Position tailPositon, Position headPosition)
        {
            return Math.Abs(tailPositon.x - headPosition.x) + Math.Abs(tailPositon.y - headPosition.y);
        }

        static int DistanceOnAxis(int xy1, int xy2)
        {
            return Math.Abs(xy1 - xy2);
        }

        private static void DrawPosition()
        {
            //int maxX = int.MinValue;
            //int maxY = int.MinValue;
            //int minX = int.MaxValue;
            //int minY = int.MaxValue;

            //foreach (Position position in tailsPositon)
            //{
            //    if (position.x > maxX) maxX = position.x;
            //    if (position.y > maxY) maxY = position.y;
            //    if (position.x < minX) minX = position.x;
            //    if (position.x > maxX) maxX = position.x;
            //}
            //if (headPosition.x > maxX) maxX = headPosition.x;
            //if (headPosition.y > maxY) maxY = headPosition.y;
            //if (headPosition.x < minX) minX = headPosition.x;
            //if (headPosition.y < minY) minY = headPosition.y;
            //Console.WriteLine("maxX={0} minX={1} maxY={2} minY={3}", maxX, minX, maxY, minY);
            //int xSize = Math.Abs(maxX - minX) + 2;
            //int ySize = Math.Abs(maxY - minY) + 2;
            //Console.WriteLine("xSize={0} ySize={1} ", xSize, ySize);
            //int[,] arrayPoistion = new int[ySize, xSize];
            //int tail = 1;
            //foreach (Position position in tailsPositon)
            //{
            //    arrayPoistion[position.y, position.x] = tail;
            //    tail += 1;
            //}
            //Console.WriteLine("headY={1} headX={0}",headPosition.y, headPosition.x);
            //Console.WriteLine("ArrayY={0} ArrayX={1}", arrayPoistion.GetLength(0), arrayPoistion.GetLength(1));
            //arrayPoistion[headPosition.y, headPosition.x] = 99;
            //for (int i = 0; i < arrayPoistion.GetLength(0); i++)
            //{
            //    for (int j = 0; j < arrayPoistion.GetLength(1); j++)
            //    {
            //        Console.Write(arrayPoistion[i, j] + " ");
            //    }
            //    Console.WriteLine();
            //}
        }
    }
}
