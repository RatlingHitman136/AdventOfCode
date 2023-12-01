using AdventOfCode.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Day1
{
    public static class Day1
    {
        public static void Part1()
        {
            string[] inputs = InputLoader.GetInput();
            int sum = 0;
            
            foreach(var line in inputs)
            {
                int res = 0;
                int leftPointer = 0;
                int rightPointer = line.Length-1;
                for(int i = 0; i < line.Length; i++)
                {
                    if (line[leftPointer] > 47 && line[leftPointer] < 58 )
                        res = (line[leftPointer] - 48) * 10;
                    else 
                        leftPointer++;
                    if (line[rightPointer] > 47 && line[rightPointer] < 58)
                        res += (line[rightPointer] - 48);
                    else
                        rightPointer--;
                }

                sum += res;
            }

            OutputLoader.WriteOutput(new string[] { sum.ToString() });
        }

        public static void Part2()
        {
            string[] inputs = InputLoader.GetInput();
            int sum = 0;

            foreach (var line in inputs)
            {
                int res = 0;
                int leftPointer = 0;
                int rightPointer = line.Length - 1;
                for (int i = 0; i < line.Length; i++)
                {
                    int leftNum = GetNumFromChars(line, leftPointer);
                    int rightNum = GetNumFromChars(line, rightPointer);
                    if (leftNum != -1)
                        res = leftNum * 10;
                    else
                        leftPointer++;
                    if (rightNum != -1)
                        res += rightNum;
                    else
                        rightPointer--;
                }

                sum += res;
            }

            OutputLoader.WriteOutput(new string[] { sum.ToString() });
        }

        public static int GetNumFromChars(string line, int startCharIndex) 
        {
            if (line[startCharIndex] > 47 && line[startCharIndex] < 58)
                return (line[startCharIndex] - 48);
            if (line.Length >= startCharIndex + 3 && line.Substring(startCharIndex, 3) == "one")
                return 1;
            if (line.Length >= startCharIndex + 3 && line.Substring(startCharIndex, 3) == "two")
                return 2;
            if (line.Length >= startCharIndex + 5 && line.Substring(startCharIndex, 5) == "three")
                return 3;
            if (line.Length >= startCharIndex + 4 && line.Substring(startCharIndex, 4) == "four")
                return 4;
            if (line.Length >= startCharIndex + 4 && line.Substring(startCharIndex, 4) == "five")
                return 5;
            if (line.Length >= startCharIndex + 3 && line.Substring(startCharIndex, 3) == "six")
                return 6;
            if (line.Length >= startCharIndex + 5 && line.Substring(startCharIndex, 5) == "seven")
                return 7;
            if (line.Length >= startCharIndex + 5 && line.Substring(startCharIndex, 5) == "eight")
                return 8;
            if (line.Length >= startCharIndex + 4 && line.Substring(startCharIndex, 4) == "nine")
                return 9;
            return -1;
        }
    }
}
