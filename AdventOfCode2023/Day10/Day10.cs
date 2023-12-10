using AdventOfCode.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AdventOfCode2023.Day10
{
    public static class Day10
    {
        public static void Part1()
        {
            string[] inputs = InputLoader.GetInput();
            int res = 0;
            char[][] map = new char[inputs.Length][];
            int[][] walkable = new int[inputs.Length][];

            (int, int) sPos = (-1, -1);

            for (int i = 0; i < inputs.Length; i++)
            {
                string input = inputs[i];
                map[i] = new char[input.Length];
                walkable[i] = new int[input.Length];
                for(int j = 0; j< input.Length; j++)
                {
                    map[i][j] = input[j];
                    walkable[i][j] = -1;

                    if (input[j] == 'S')
                        sPos = (i, j);
                }
            }

            Queue<(int, int, int)> qToGo = new Queue<(int, int, int)>();
            qToGo.Enqueue((sPos.Item1, sPos.Item2, 0));

            while (qToGo.Count > 0)
            {
                (int, int, int) cur = qToGo.Dequeue();
                
                int x = cur.Item1;
                int y = cur.Item2;
                int val = cur.Item3;

                if (walkable[x][y] != -1)
                    continue;

                char c = map[x][y];
                walkable[x][y] = val;
                res = Math.Max(val, res);

                if (x > 0)
                    if ("S|LJ".Contains(c))
                    {
                        char c2 = map[x - 1][y];
                        if ("|7F".Contains(c2))
                            qToGo.Enqueue((x - 1, y, cur.Item3 + 1));
                    }

                if (x < map.Length-1)
                    if ("S|7F".Contains(c))
                    {
                        char c2 = map[x + 1][y];
                        if ("|LJ".Contains(c2))
                            qToGo.Enqueue((x + 1, y, cur.Item3 + 1));
                    }

                if (y > 0)
                    if ("S7J-".Contains(c))
                    {
                        char c2 = map[x][y - 1];
                        if ("FL-".Contains(c2))
                            qToGo.Enqueue((x, y - 1, cur.Item3 + 1));
                    }

                if (y < map[0].Length - 1)
                    if ("SFL-".Contains(c))
                    {
                        char c2 = map[x][y + 1];
                        if ("7J-".Contains(c2))
                            qToGo.Enqueue((x, y + 1, cur.Item3 + 1));
                    }

            }

            OutputLoader.WriteOutput(res.ToString());
        }

        public static void Part2()
        {
            string[] inputs = InputLoader.GetInput();
            int res = 0;
            char[][] map = new char[inputs.Length][];
            int[][] walkable = new int[inputs.Length][];

            (int, int) sPos = (-1, -1);

            for (int i = 0; i < inputs.Length; i++)
            {
                string input = inputs[i];
                map[i] = new char[input.Length];
                walkable[i] = new int[input.Length];
                for (int j = 0; j < input.Length; j++)
                {
                    map[i][j] = input[j];
                    walkable[i][j] = -1;

                    if (input[j] == 'S')
                        sPos = (i, j);
                }
            }

            Queue<(int, int, int)> qToGo = new Queue<(int, int, int)>();
            qToGo.Enqueue((sPos.Item1, sPos.Item2, 0));

            while (qToGo.Count > 0)
            {
                (int, int, int) cur = qToGo.Dequeue();

                int x = cur.Item1;
                int y = cur.Item2;
                int val = cur.Item3;

                if (walkable[x][y] != -1)
                    continue;

                char c = map[x][y];
                walkable[x][y] = val;
                res = Math.Max(val, res);

                if (x > 0)
                    if ("S|LJ".Contains(c))
                    {
                        char c2 = map[x - 1][y];
                        if ("|7F".Contains(c2))
                            qToGo.Enqueue((x - 1, y, cur.Item3 + 1));
                    }

                if (x < map.Length - 1)
                    if ("S|7F".Contains(c))
                    {
                        char c2 = map[x + 1][y];
                        if ("|LJ".Contains(c2))
                            qToGo.Enqueue((x + 1, y, cur.Item3 + 1));
                    }

                if (y > 0)
                    if ("S7J-".Contains(c))
                    {
                        char c2 = map[x][y - 1];
                        if ("FL-".Contains(c2))
                            qToGo.Enqueue((x, y - 1, cur.Item3 + 1));
                    }

                if (y < map[0].Length - 1)
                    if ("SFL-".Contains(c))
                    {
                        char c2 = map[x][y + 1];
                        if ("7J-".Contains(c2))
                            qToGo.Enqueue((x, y + 1, cur.Item3 + 1));
                    }

            }

            for (int i = 0; i < map.Length; i++)
            {
                for (int j = 0; j < map[0].Length; j++)
                {
                    if (walkable[i][j] != -1)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(map[i][j]);
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else if (map[i][j] == '.')
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("*");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                        Console.Write(map[i][j]);
                }
                Console.WriteLine();
            }
        }
    }
}
