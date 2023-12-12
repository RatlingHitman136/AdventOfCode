using AdventOfCode.Tools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PostSharp.Patterns.Caching;
using PostSharp.Patterns.Caching.Backends;

namespace AdventOfCode2023.Day12
{
    public static class Day12
    {
        public static void Part1()
        {
            string[] input = InputLoader.GetInput();
            int res = 0;

            foreach (string s in input)
            {
                string m = s.Split(' ')[0];
                string[] numsString = s.Split(' ')[1].Split(',');
                int[] ints = new int[numsString.Length];
                for(int i = 0; i< numsString.Length; i++)
                    ints[i] = int.Parse(numsString[i]);

                res += CountComb(m, ints, 0);
            }


            OutputLoader.WriteOutput(res);
        }   

        public static int CountComb(string s, int[] ints, int curPointer)
        {
            int res = 0;
            for(int i = curPointer; i<s.Length; i++)
            {
                if (s[i] == '?')
                {
                    var cs = s.ToCharArray();
                    cs[i] = '.';
                    res += CountComb(new string(cs), ints, i);
                    cs[i] = '#';
                    res += CountComb(new string(cs), ints, i);

                    return res;
                }
            }

            if (IsValid(s, ints))
                return 1;
            else
                return 0;
        }

        public static bool IsValid(string s, int[] ints)
        {
            var r = s.Split('.', StringSplitOptions.RemoveEmptyEntries);
            for(int i = 0; i< r.Length; i++)
            {
                if (i >= ints.Length)
                    return false;

                if (r[i].Count() != ints[i])
                    return false;
            }
            if (r.Length != ints.Length)
                return false;
            return true;
        }

        public static void Part2()
        {
            string[] input = InputLoader.GetInput();
            int res = 0;

            foreach (string s in input)
            {
                string m = s.Split(' ')[0];
                string[] numsString = s.Split(' ')[1].Split(',');
                int[] ints = new int[numsString.Length];
                for (int i = 0; i < numsString.Length; i++)
                    ints[i] = int.Parse(numsString[i]);

                string newM = m + "?" + m + "?" + m + "?" + m + "?" + m;
                int[] newInts = new int[ints.Length*5];
                for (int i = 0; i < newInts.Length; i++)
                    newInts[i] = ints[i % ints.Length];

                res += CountComb2(newM, newInts, 0, 0, 0);
                Console.WriteLine(m +" - " +CountComb2(m, ints, 0, 0, 0));
            }


            OutputLoader.WriteOutput(res);
        }

        [Cache]
        public static int CountComb2(string s, int[] ints, int i, int seqCount, int seqId)
        {
            if (i >= s.Length)
            {
                if (seqId == ints.Length)
                {
                    //Console.WriteLine(s);
                    return 1;
                }
                else if (seqId == ints.Length - 1 && seqCount == ints[ints.Length-1])
                {
                    //Console.WriteLine(s);
                    return 1;
                }

                return 0;
            }
            else if (s[i] == '#')
            {
                if (seqId >= ints.Length || ints[seqId] < seqCount + 1)
                    return 0;
                else
                    return CountComb2(s, ints, i + 1, seqCount + 1, seqId);
            }
            else if (s[i] == '.')
            {
                if(seqCount != 0)
                {
                    if (ints[seqId] != seqCount)
                        return 0;
                    else
                        return CountComb2(s, ints, i + 1, 0, seqId + 1);
                }
                else
                {
                    return CountComb2(s, ints, i + 1, 0, seqId);
                }
            }
            else
            {
                int res = 0;

                var cs = s.ToCharArray();
                cs[i] = '.';
                res += CountComb2(new string(cs), ints, i, seqCount, seqId);
                cs[i] = '#';
                res += CountComb2(new string(cs), ints, i, seqCount, seqId);

                return res;
            }
        }
    }
}
