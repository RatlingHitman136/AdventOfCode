using AdventOfCode.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Day23
{
    public static class Day23
    {
        public static void Part1()
        {
            string[] inputs = InputLoader.GetInput();
            int res = 0;
            Dictionary<(int,int), int> dist = new Dictionary<(int, int), int> ();
            Dictionary<(int,int), (int,int)> prev = new Dictionary<(int, int), (int, int)> ();
            PriorityQueue<(int, int), int> q = new PriorityQueue<(int, int), int>();

            for (int i = 0; i < inputs.Length; i++)
            {
                for(int j = 0; j < inputs[0].Length; j++)
                {
                    if (inputs[i][j] != '#')
                    {
                        dist.Add((i, j), int.MinValue);
                        prev.Add((i, j), (-1, -1));
                    }
                }
            }

            q.Enqueue((0,1), int.MaxValue);
            dist[(0,1)] = 0;

            while (q.TryDequeue(out (int,int) pos, out int prior))
            {
                int x = pos.Item1;
                int y = pos.Item2;

                if (dist.ContainsKey((x + 1, y)) && ".v".Contains(inputs[x][y]))
                {
                    int altDist = dist[(x, y)] + 1;
                    if (altDist > dist[(x + 1, y)])
                    {
                        dist[(x + 1, y)] = altDist;
                        prev[(x + 1, y)] = (x, y);
                        q.Enqueue((x + 1, y), int.MaxValue - altDist);
                    }
                }

                if(dist.ContainsKey((x - 1, y)) && ".".Contains(inputs[x][y]))
                {
                    int altDist = dist[(x, y)] + 1;
                    if (altDist > dist[(x - 1, y)])
                    {
                        dist[(x - 1, y)] = altDist;
                        prev[(x - 1, y)] = (x, y);
                        q.Enqueue((x + 1, y), int.MaxValue - altDist);
                    }
                }

                if(dist.ContainsKey((x, y + 1)) && ".>".Contains(inputs[x][y]))
                {
                    int altDist = dist[(x, y)] + 1;
                    if (altDist > dist[(x, y + 1)])
                    {
                        dist[(x, y + 1)] = altDist;
                        prev[(x, y + 1)] = (x, y);
                        q.Enqueue((x + 1, y), int.MaxValue - altDist);
                    }
                }
                
                if(dist.ContainsKey((x, y - 1)) && ".<".Contains(inputs[x][y]))
                {
                    int altDist = dist[(x, y)] + 1;
                    if (altDist > dist[(x, y - 1)])
                    {
                        dist[(x, y - 1)] = altDist;
                        prev[(x, y - 1)] = (x, y);
                        q.Enqueue((x + 1, y), int.MaxValue - altDist);
                    }
                }
            }


            OutputLoader.WriteOutput(dist[(inputs.Length - 1, inputs[0].Length-2)]);
        }

        public static void Part2()
        {

        }
    }
}
