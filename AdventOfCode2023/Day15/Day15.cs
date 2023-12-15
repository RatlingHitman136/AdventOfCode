using AdventOfCode.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Day15
{
    public static class Day15
    {
        public static void Part1()
        {
            string[] inputs = InputLoader.GetInput();
            int res = 0;

            foreach (var input in inputs)
            {
                string[] ss = input.Split(",");
                foreach (var ss2 in ss)
                {
                    res += Hash(ss2);
                }
            }

            OutputLoader.WriteOutput(res.ToString());
        }

        public static int Hash(string s)
        {
            int res = 0;
            foreach (var item in s)
            {
                res += item;
                res *= 17;
                res = res % 256;
            }
            return res;
        }

        public static void Part2()
        {
            string[] inputs = InputLoader.GetInput();
            int res = 0;

            List<(string, int)>[] hashMap = new List<(string, int)>[256];
            for (int i = 0; i < 256; i++)
                hashMap[i] = new List<(string, int)>();
           
            string[] ops = inputs[0].Split(",");
            foreach (string ops2 in ops)
            {
                
                if (ops2.Last() == '-')
                {
                    string label = ops2.Substring(0, ops2.Length - 1);
                    int hash = Hash(label);
                    int ind = hashMap[hash].FindIndex(x => x.Item1 == label);
                    if(ind != -1)
                        hashMap[hash].RemoveAt(ind);
                }
                else
                {
                    string label = ops2.Split("=")[0];
                    int hash = Hash(label);
                    int foc = int.Parse(ops2.Split("=")[1]);
                    int ind = hashMap[hash].FindIndex(x => x.Item1 == label);
                    if (ind == -1)
                        hashMap[hash].Add((label, foc));
                    else
                        hashMap[hash][ind] = (label, foc);
                }
            }

            for (int i1 = 0; i1 < hashMap.Length; i1++)
            {
                List<(string, int)>? item = hashMap[i1];
                for (int i = 0; i < item.Count; i++)
                {
                    (string, int) item1 = item[i];
                    int lense = 1 + i1;
                    lense *= (i+1);
                    lense *= item1.Item2;
                    res += lense;
                }
            }

            OutputLoader.WriteOutput(res.ToString());
        }
    }
}
