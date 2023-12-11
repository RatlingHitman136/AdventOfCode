using AdventOfCode.Tools;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Day11
{
    public static class Day11
    {
        public static void Part1()
        {
            string[] input = InputLoader.GetInput();
            int res = 0;

            List<(int,int)> coords = new List<(int,int)> ();
            int[] rows = new int[input.Length];
            int[] cols = new int[input[0].Length];

            int counter = 0;
            for (int i = 0; i < input.Length; i++)
            {
                string? item = input[i];
                for (int i1 = 0; i1 < item.Length; i1++)
                {
                    char c = item[i1];
                    if (c == '#')
                        coords.Add((i,i1));
                }

                rows[i] = counter;

                if (!item.Contains('#'))
                    counter += 2;
                else
                    counter += 1;
            }

            counter = 0;
            for(int i = 0; i < input[0].Length; i++)
            {
                bool flag = false;
                cols[i] = counter; 
                for(int j = 0; j < input.Length; j++)
                {
                    if (input[j][i] == '#')
                        flag = true;
                }

                if (!flag)
                    counter += 2;
                else
                    counter += 1;
            }
            counter = 0;
            List<int> dist = new List<int>();

            for(int i = 0; i< coords.Count; i++)
            {
                for(int j = i + 1; j < coords.Count; j++)
                {
                    (int, int) coord1 = coords[i];
                    (int, int) coord2 = coords[j];
                    counter++;
                    int dis = Math.Abs(rows[coord2.Item1] - rows[coord1.Item1]) + Math.Abs(cols[coord2.Item2] - cols[coord1.Item2]);
                    dist.Add(dis);
                    res += dis;
                }
            }


            OutputLoader.WriteOutput(res.ToString());
        }

        public static void Part2()
        {
            string[] input = InputLoader.GetInput();
            long res = 0;

            List<(int, int)> coords = new List<(int, int)>();
            int[] rows = new int[input.Length];
            int[] cols = new int[input[0].Length];

            int counter = 0;
            for (int i = 0; i < input.Length; i++)
            {
                string? item = input[i];
                for (int i1 = 0; i1 < item.Length; i1++)
                {
                    char c = item[i1];
                    if (c == '#')
                        coords.Add((i, i1));
                }

                rows[i] = counter;

                if (!item.Contains('#'))
                    counter += 1000000;
                else
                    counter += 1;
            }

            counter = 0;
            for (int i = 0; i < input[0].Length; i++)
            {
                bool flag = false;
                cols[i] = counter;
                for (int j = 0; j < input.Length; j++)
                {
                    if (input[j][i] == '#')
                        flag = true;
                }

                if (!flag)
                    counter += 1000000;
                else
                    counter += 1;
            }
            counter = 0;
            List<int> dist = new List<int>();

            for (int i = 0; i < coords.Count; i++)
            {
                for (int j = i + 1; j < coords.Count; j++)
                {
                    (int, int) coord1 = coords[i];
                    (int, int) coord2 = coords[j];
                    counter++;
                    int dis = Math.Abs(rows[coord2.Item1] - rows[coord1.Item1]) + Math.Abs(cols[coord2.Item2] - cols[coord1.Item2]);
                    dist.Add(dis);
                    res += dis;
                }
            }


            OutputLoader.WriteOutput(res.ToString());
        }
    }
}
