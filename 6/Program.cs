using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace AdventOfCode6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Assets\code.txt");
            string[] lines = System.IO.File.ReadAllLines(path);
            int positionOfStart = FindStart(lines[0]);
            int positionOfMsg = FindMessage(lines[0], positionOfStart);
            Console.WriteLine("start of signal = {0}", positionOfStart);
            Console.WriteLine("start of msg = {0}", positionOfMsg);
        }

        private static int FindStart(string signal)
        {
            char[] buffer = new char[4];
            for (int i = 3; i < signal.Length; i++)
            {
                buffer[0] = signal[i - 3];
                buffer[1] = signal[i - 2];
                buffer[2] = signal[i - 1];
                buffer[3] = signal[i];
                if (buffer.Distinct().Count() == 4) return i + 1;
            }
            return -1;
        }

        private static int FindMessage(string signal, int positionOfStart)
        {
            positionOfStart -= 1;
            if (positionOfStart < 13) positionOfStart = 13;
            char[] buffer;
            for (int i = positionOfStart; i < signal.Length; i++)
            {
                buffer = FillBuffer(i, signal);
                if (buffer.Distinct().Count() == 14) return i + 1;
            }
            return -1;
        }

        private static char[] FillBuffer(int i, string signal)
        {
            char[] tempBuffer = new char[14];
            int bufferIndex = 0;
            for (int a = i - 13; a <= i; a++)
            {
                tempBuffer[bufferIndex] = signal[a];
                bufferIndex += 1;
            }
            return tempBuffer;
        }
    }
}
