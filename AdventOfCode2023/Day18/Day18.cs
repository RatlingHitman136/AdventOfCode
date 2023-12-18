using AdventOfCode.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Day18
{
    public static class Day18
    {
        public static void Part1()
        {
            string[] inputs = InputLoader.GetInput();
            int res = 0;

            List<Vector2> points = new List<Vector2>();

            Vector2 curPoint = Vector2.Zero;
            points.Add(curPoint);
            int sum = 1;

            foreach (string input in inputs)
            {
                char d = input[0];
                int len = int.Parse(input.Split(" ")[1]);
                switch (d)
                {
                    case 'R':
                        curPoint += new Vector2(1, 0) * len;
                        break;
                    case 'D':
                        curPoint += new Vector2(0, 1) * len;
                        break;
                    case 'L':
                        curPoint += new Vector2(-1, 0) * len;
                        break;
                    case 'U':
                        curPoint += new Vector2(0, -1) * len;
                        break;
                }
                points.Add(curPoint);
                sum += len;
            }

            res = (int)polygonArea(points.ToArray(), points.Count);

            res += sum / 2 + 1;

            OutputLoader.WriteOutput(res.ToString());
        }

        public static long polygonArea(Vector2[] p, int n)
        {

            // Initialize area
            long area = 0;

            // Calculate value of shoelace formula
            int j = n - 1;

            for (int i = 0; i < n; i++)
            {
                area += ((long)p[j].X + (long)p[i].X) * ((long)p[j].Y - (long)p[i].Y);

                // j is previous vertex to i
                j = i;
            }

            // Return absolute value
            return Math.Abs(area / 2);
        }

        public static void Part2()
        {
            string[] inputs = InputLoader.GetInput();
            long res = 0;

            List<(char, int)> newInput = new List<(char, int)>();

            foreach (string input in inputs)
            {
                char c = '0';
                int len;

                string hex = input.Split(" ")[2].Substring(2, 6);
                char hexLast = hex[5];
                switch (hexLast)
                {
                    case '0':
                        c = 'R';
                        break;
                    case '1':
                        c = 'D';
                        break;
                    case '2':
                        c = 'L';
                        break;
                    case '3':
                        c = 'U';
                        break;
                }
                string hexLen = hex.Substring(0, 5);
                len = Int32.Parse(hexLen.ToUpper(), System.Globalization.NumberStyles.HexNumber);

                newInput.Add((c, len));
            }

            List<Vector2> points = new List<Vector2>();

            Vector2 curPoint = Vector2.Zero;
            points.Add(curPoint);
            long sum = 1;

            foreach (var input in newInput)
            {
                int len = input.Item2;
                switch (input.Item1)
                {
                    case 'R':
                        curPoint += new Vector2(1, 0) * len;
                        break;
                    case 'D':
                        curPoint += new Vector2(0, 1) * len;
                        break;
                    case 'L':
                        curPoint += new Vector2(-1, 0) * len;
                        break;
                    case 'U':
                        curPoint += new Vector2(0, -1) * len;
                        break;
                }
                points.Add(curPoint);
                sum += len;
            }

            res = (long)polygonArea(points.ToArray(), points.Count);

            res += sum / 2 + 1;

            OutputLoader.WriteOutput(res.ToString());
        }
    }
}
