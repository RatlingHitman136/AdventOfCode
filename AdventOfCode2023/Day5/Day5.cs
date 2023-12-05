using AdventOfCode.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Day5
{
    public static class Day5
    {

        public static void Part1()
        {
            string[] inputs = InputLoader.GetInput();
            string[] seeds = inputs[0].Trim().Split(':')[1].Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            List<long> seedsInt = new List<long>();
            foreach(string s in seeds)
                seedsInt.Add(Convert.ToInt64(s));

            int lineInd = 3; // seed to soil

            for (int c = 0; c < 7; c++)
            {
                bool[] b = new bool[seeds.Length];
                while (lineInd < inputs.Length && inputs[lineInd] != "") // seeds to soil
                {
                    long dest = long.Parse(inputs[lineInd].Split(' ')[0]);
                    long source = long.Parse(inputs[lineInd].Split(' ')[1]);
                    long len = long.Parse(inputs[lineInd].Split(' ')[2]);
                    List<long> tmp = new List<long>();
                    foreach (long num in seedsInt)
                    {
                        if (num >= source && num < source + len && !b[tmp.Count])
                        {
                            long delt = num - source;
                            tmp.Add(delt + dest);
                            b[tmp.Count - 1] = true;
                        }
                        else
                            tmp.Add(num);
                    }
                    seedsInt = tmp;
                    lineInd++;
                }
                lineInd += 2;
            }


            long s2 = Int64.MaxValue;

            foreach(long num in seedsInt)
            {
                if (s2 > num) s2 = num;
            }
            OutputLoader.WriteOutput(s2.ToString());
        }

        public static void Part2()
        {
            string[] inputs = InputLoader.GetInput();
            string[] seeds = inputs[0].Trim().Split(':')[1].Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);

            List<(long, long)> l = new List<(long, long)>();

            for (int i = 0; i < seeds.Length; i += 2)
                l.Add((long.Parse(seeds[i]), long.Parse(seeds[i]) + long.Parse(seeds[i+1]) - 1));

            int lineInd = 3;

            for (int c = 0; c < 7; c++)
            {
                List<(long, long)> tmp = new List<(long, long)>();

                while (lineInd < inputs.Length && inputs[lineInd] != "") // seeds to soil
                {
                    long dest = long.Parse(inputs[lineInd].Split(' ')[0]);
                    long source = long.Parse(inputs[lineInd].Split(' ')[1]);
                    long len = long.Parse(inputs[lineInd].Split(' ')[2]);

                    long endS = source + len - 1;
                    long endD = dest + len - 1;

                    for(int i = 0; i< l.Count; i++)
                    {
                        long start = l[i].Item1;
                        long end = l[i].Item2;
                        if (start == -1) continue;

                        if (start >= source && end <= endS)
                        {
                            tmp.Add((start - source + dest, end - source + dest));
                            l[i] = (-1, -1);
                        }
                        else if (start >= source && start <= endS && end > endS)
                        {
                            tmp.Add((start - source + dest, endS - source + dest));
                            l[i] = (endS + 1, end);
                        }
                        else if (start < source && end >= source && end <= endS)
                        {
                            tmp.Add((source - source + dest, end - source + dest));
                            l[i] = (start, source - 1);
                        }
                        else if (start < source && end > endS)
                        {
                            tmp.Add((source - source + dest, endS - source + dest));
                            l[i] = (start, source - 1);
                            l.Add((endS + 1, end));
                        }
                        Console.WriteLine(i);
                    }
                    lineInd++;
                }
                Console.WriteLine("--------------------------------------");
                foreach (var p in l)
                    if (p.Item1 != -1)
                        tmp.Add(p);
                l = tmp;

                lineInd += 2;
            }

            long s2 = Int64.MaxValue;

            foreach ((long, long) num in l)
            {
                if (s2 > num.Item1) s2 = num.Item1;
            }
            OutputLoader.WriteOutput(s2.ToString());
        }
    }
}
