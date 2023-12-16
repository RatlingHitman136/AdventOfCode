using AdventOfCode.Tools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Day16
{
    public static class Day16
    {
        public static void Part1()
        {
            string[] inputs = InputLoader.GetInput();
            int res = Energies(inputs, (-1, 0, 0));
            OutputLoader.WriteOutput(res.ToString());
        }

        public static void Part2()
        {
            string[] inputs = InputLoader.GetInput();
            int res = 0;
            Stopwatch stopwatch = Stopwatch.StartNew();
            for (int i = 0; i < inputs.Length; i++)
            {
                res = Math.Max(res, Energies(inputs, (-1, i, 0)));
                res = Math.Max(res, Energies(inputs, (inputs[0].Length, i, 2)));
            }
            for (int i = 0; i < inputs[0].Length; i++)
            {
                res = Math.Max(res, Energies(inputs, (i, -1, 1)));
                res = Math.Max(res, Energies(inputs, (i, inputs.Length, 3)));
            }
            OutputLoader.WriteOutput(res.ToString());
            stopwatch.Stop();
            Console.WriteLine("Execution lasted for: {0}", stopwatch.ElapsedMilliseconds);
        }

        public static int Energies(string[] inputs, (int,int,int) start)
        {
            Dictionary<(int, int, int), int> dict = new Dictionary<(int, int, int), int>();
            Queue<(int, int, int)> queue = new Queue<(int, int, int)>();

            queue.Enqueue(start);
            //0 - right
            //1 - down
            //2 - left
            //3 - top

            while (queue.Count() != 0)
            {
                (int, int, int) data = queue.Dequeue();
                int x = data.Item1;
                int y = data.Item2;
                int dir = data.Item3;

                if (dict.ContainsKey((x, y, dir)) && dict[(x, y, dir)] == 1)
                    continue;
                dict.Add((x, y, dir), 1);
                switch (dir)
                {
                    case 0:
                        if (x == inputs[0].Length - 1)
                            break;
                        if (inputs[y][x + 1] == '|')
                        {
                            queue.Enqueue((x + 1, y, 3));
                            queue.Enqueue((x + 1, y, 1));
                        }
                        else if (inputs[y][x + 1] == '/')
                        {
                            queue.Enqueue((x + 1, y, 3));
                        }
                        else if (inputs[y][x + 1] == '\\')
                        {
                            queue.Enqueue((x + 1, y, 1));
                        }
                        else
                        {
                            queue.Enqueue((x + 1, y, 0));
                        }
                        break;
                    case 3:
                        if (y == 0)
                            break;
                        if (inputs[y - 1][x] == '-')
                        {
                            queue.Enqueue((x, y - 1, 2));
                            queue.Enqueue((x, y - 1, 0));
                        }
                        else if (inputs[y - 1][x] == '/')
                        {
                            queue.Enqueue((x, y - 1, 0));
                        }
                        else if (inputs[y - 1][x] == '\\')
                        {
                            queue.Enqueue((x, y - 1, 2));
                        }
                        else
                        {
                            queue.Enqueue((x, y - 1, 3));
                        }
                        break;
                    case 2:
                        if (x == 0)
                            break;
                        if (inputs[y][x - 1] == '|')
                        {
                            queue.Enqueue((x - 1, y, 3));
                            queue.Enqueue((x - 1, y, 1));
                        }
                        else if (inputs[y][x - 1] == '/')
                        {
                            queue.Enqueue((x - 1, y, 1));
                        }
                        else if (inputs[y][x - 1] == '\\')
                        {
                            queue.Enqueue((x - 1, y, 3));
                        }
                        else
                        {
                            queue.Enqueue((x - 1, y, 2));
                        }
                        break;
                    case 1:
                        if (y == inputs.Length - 1)
                            break;
                        if (inputs[y + 1][x] == '-')
                        {
                            queue.Enqueue((x, y + 1, 2));
                            queue.Enqueue((x, y + 1, 0));
                        }
                        else if (inputs[y + 1][x] == '/')
                        {
                            queue.Enqueue((x, y + 1, 2));
                        }
                        else if (inputs[y + 1][x] == '\\')
                        {
                            queue.Enqueue((x, y + 1, 0));
                        }
                        else
                        {
                            queue.Enqueue((x, y + 1, 1));
                        }
                        break;
                }
            }

            Dictionary<(int, int), int> d2 = new Dictionary<(int, int), int>();
            foreach (var item in dict.Keys)
                if (!d2.ContainsKey((item.Item1, item.Item2)))
                    d2.Add((item.Item1, item.Item2), 1);

            return d2.Count-1;
        }
    }
}
