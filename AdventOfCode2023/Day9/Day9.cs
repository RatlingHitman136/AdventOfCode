using AdventOfCode.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Day9
{
    public static class Day9
    {
        public static void Part1()
        {
            string[] inputs = InputLoader.GetInput();
            int res = 0;

            foreach (string input in inputs)
            {
                string[] b = input.Split(' ');
                List<int> a = new List<int>();
                foreach (string b2 in b)
                    a.Add(int.Parse(b2));
                res += Extrapolate(a.ToArray());
            }

            OutputLoader.WriteOutput(res.ToString());
        }

        public static int Extrapolate(int[] a)
        {
            List<int> diff = new List<int>();
            for(int i = 0; i< a.Length-1; i ++)
            {
                diff.Add(a[i+1] - a[i]);
            }

            if (diff.Count(x => x == 0) == a.Length - 1)
                return a[a.Length - 1];
            return a[a.Length - 1] + Extrapolate(diff.ToArray());
        }

        public static void Part2() {
            string[] inputs = InputLoader.GetInput();
            int res = 0;

            foreach (string input in inputs)
            {
                string[] b = input.Split(' ');
                List<int> a = new List<int>();
                foreach (string b2 in b)
                    a.Add(int.Parse(b2));
                res += Extrapolate2(a.ToArray());
            }

            OutputLoader.WriteOutput(res.ToString());
        }

        public static int Extrapolate2(int[] a)
        {
            List<int> diff = new List<int>();
            for (int i = 0; i < a.Length - 1; i++)
            {
                diff.Add(a[i + 1] - a[i]);
            }

            if (diff.Count(x => x == 0) == a.Length - 1)
                return a[0];
            return a[0] - Extrapolate2(diff.ToArray());
        }
    }
}
