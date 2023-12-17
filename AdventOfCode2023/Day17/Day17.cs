using AdventOfCode.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Day17
{
    using Map = Dictionary<Vector2, int>;
    public static class Day17
    {
        private static Vector2[] Dirs = { new Vector2(0, 1), new Vector2(1, 0), new Vector2(0, -1), new Vector2(-1, 0) };
        // 0 - down
        // 1 - right
        // 2 - up
        // 3 - left

        public static void Part1()
        {
            string[] inputs = InputLoader.GetInput();
            int loss = int.MaxValue;

            loss = Run(inputs, 1, 3, out var seen);
            var route = GetRoute(seen, inputs);
            PrintRoute(route, inputs);
            OutputLoader.WriteOutput(loss);
        }

        public static void Part2()
        {
            string[] inputs = InputLoader.GetInput();
            int loss = int.MaxValue;

            loss = Run(inputs, 4, 10, out var seen);
            var route = GetRoute(seen, inputs);
            PrintRoute(route, inputs);
            OutputLoader.WriteOutput(loss);
        }

        public static int Run(string[] inputs, int minDist, int maxDist, out HashSet<(Vector2, int, int)> seenRet)
        {


            PriorityQueue<(Vector2, int, int), int> q = new PriorityQueue<(Vector2 pos, int d, int straight), int> ();
            HashSet<(Vector2, int, int)> seen = new HashSet<(Vector2, int, int)> ();
            Vector2 endPos = new Vector2(inputs[0].Length - 1, inputs.Length - 1);

            q.Enqueue((Vector2.Zero, -1, 0), 0);

            while (q.TryDequeue(out (Vector2 pos, int d, int s) data, out int loss))
            {
                if (data.pos == endPos)
                {
                    seenRet = seen;
                    return loss;
                }
                for(int newD = 0; newD < 4; newD++)
                {
                    if(data.s > 2)
                    {
                        int a = 0;
                    }
                    if ((newD + 2) % 4 == data.d)
                        continue;
                    if (newD == data.d && data.s >= maxDist)
                        continue;
                    if (data.d != -1 && newD != data.d && data.s < minDist)
                        continue;
                    int newLoss = loss;

                    Vector2 newPos = data.pos + Dirs[newD];

                    if (!IsPosPossible(newPos, inputs))
                        continue;
                    newLoss += GetLoss(newPos, inputs);
                    if(data.d == newD && data.s > 2)
                    {
                        int b = 0;
                    }

                    int newStraight = data.d == newD ? data.s + 1 : 1;
                    if (!seen.Contains((newPos, newD, newStraight)))
                    {
                        seen.Add((newPos, newD, newStraight));
                        q.Enqueue((newPos, newD, newStraight), newLoss);
                    }
                }
            }
            seenRet = seen;
            return -1;
        }

        public static bool IsPosPossible(Vector2 pos, string[] inputs)
        {
            if (pos.X < 0 || pos.X > inputs[0].Length - 1 || pos.Y < 0 || pos.Y > inputs.Length - 1)
                return false;
            return true;
        }

        public static int GetLoss(Vector2 pos, string[] inputs)
        {
            return int.Parse(inputs[(int)pos.Y][(int)pos.X].ToString());
        }

        public static List<Vector2> GetRoute(HashSet<(Vector2, int, int)> seen, string[] inputs)
        {
            List<Vector2> route = new List<Vector2> ();

            Vector2 curPos = new Vector2(inputs[0].Length - 1, inputs.Length - 1);
            int curDir = seen.First(x => x.Item1 == curPos).Item2;
            int curStraight = seen.First(x => x.Item1 == curPos).Item3;
            route.Add(curPos);

            while(curPos != Vector2.Zero)
            {
                Vector2 newPos = curPos - Dirs[curDir];
                curPos = newPos;
                (_, curDir, curStraight) = seen.First(x => x.Item1 == curPos && ((x.Item2 == curDir && x.Item3 == curStraight-1) || (x.Item2 != curDir)));

                route.Add(newPos);
            }
            route.Reverse();
            return route;
        }

        public static void PrintRoute(IEnumerable<Vector2> route, string[] inputs)
        {
            for (int i = 0; i < inputs.Length; i++)
            {
                for (int j = 0; j < inputs[0].Length; j++)
                {
                    if (route.Contains(new Vector2(j, i)))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(inputs[i][j]);
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                        Console.Write(inputs[i][j]);
                }
                Console.WriteLine();
            }
        }
    }
}
