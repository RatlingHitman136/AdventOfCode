using AdventOfCode.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Day21
{
    public static class Day21
    {
        public static void Part1()
        {
            string[] inputs = InputLoader.GetInput();
            int res = 0;
            int sX = 0;
            int sY = 0;

            for (int i = 0; i < inputs.Length; i++)
                if (inputs[i].Contains('S'))
                {
                    sY = i;
                    sX = inputs[i].IndexOf('S');
                }

            //sY = inputs.Length/2;
            //sX = inputs[0].Length;

            for (int i = 0; i < inputs.Length; i++)
                for (int j = 0; j < inputs[0].Length; j++)
                {
                    int path = Math.Abs(i - sY) + Math.Abs(j - sX);
                    if (path % 2 == 1 && path <= 65 && inputs[i][j] != '#')
                    {
                        res++;
                        var a = inputs[i].ToCharArray();    
                        a[j] = '@';
                        inputs[i] = "";
                        a.ToList().ForEach(x => inputs[i] += x.ToString());
                    }
                }
            inputs.ToList().ForEach(x => Console.WriteLine(x));
            OutputLoader.WriteOutput(res.ToString());
        }

        public static void Part2()
        {
            //return;
            string[] inputs = InputLoader.GetInput();

            int sX = 0;
            int sY = 0;

            for (int i = 0; i < inputs.Length; i++)
                if (inputs[i].Contains('S'))
                {
                    sY = i;
                    sX = inputs[i].IndexOf('S');
                }

            long n = 26501365 / 131;
            long a0 = GetCountNew(65, 131, inputs);
            long a1 = GetCountNew(65 + 131, 262, inputs);
            long a2 = GetCountNew(65 + 2*131, 393, inputs);

            long b0 = a0;
            long b1 = a1 - a0;
            long b2 = a2 - a1;
            long res = 0;
            res = b0 + b1 * n + (n * (n - 1) / 2) * (b2 - b1);



            inputs.ToList().ForEach(x => Console.WriteLine(x));
            OutputLoader.WriteOutput(res.ToString());
        }

        public static int GetCount(int sX, int sY, int maxPath, string[] inputs)
        {
            int res = 0;
            for (int i = 0; i < inputs.Length; i++)
                for (int j = 0; j < inputs[0].Length; j++)
                {
                    int path = Math.Abs(i - sY) + Math.Abs(j - sX);
                    if (path % 2 == 1 && path <= maxPath && inputs[i][j] != '#')
                    {
                        res++;
                    }
                }
            return res;
        }

        public static int GetCountNew(int maxPath, int startInd, string[] inputs)
        {

            int res = 0;
            for (int i = -startInd; i < startInd; i++)
                for (int j = -startInd; j < startInd; j++)
                {
                    int path = Math.Abs(i) + Math.Abs(j);
                    if (path % 2 == 1 && path <= maxPath)
                    {
                        int x = (j + 65) % 131;
                        x = x < 0 ? x + 131 : x;
                        int y = (i + 65) % 131;
                        y = y < 0 ? y + 131 : y;
                        if (inputs[y][x] != '#')
                            res++;
                    }
                }
            return res;
        }
    }
}
