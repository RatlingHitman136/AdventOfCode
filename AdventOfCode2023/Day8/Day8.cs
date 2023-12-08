using AdventOfCode.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Day8
{
    public static class Day8
    {
        public static void Part1()
        {
            string[] inputs = InputLoader.GetInput();
            int res  = 0;
            Dictionary<string, (string, string)> d = new Dictionary<string, (string, string)>();
            string instr = inputs[0];
            for (int i = 2; i < inputs.Length; i++)
            {
                string input = inputs[i];
                string src = input.Substring(0, 3);
                string l = input.Substring(7, 3);
                string r = input.Substring(12, 3);
                d.Add(src, (l, r));
            }

            string cur = "AAA";
            while(cur != "ZZZ")
            {
                if (instr[res%instr.Length] == 'L')
                    cur = d[cur].Item1;
                else if (instr[res % instr.Length] == 'R')
                    cur = d[cur].Item2;
                res++;
            }

            OutputLoader.WriteOutput(res.ToString());
        }

        public static void Part2()
        {
            string[] inputs = InputLoader.GetInput();
            int res = 0;
            Dictionary<string, (string, string)> d = new Dictionary<string, (string, string)>();
            string instr = inputs[0];

            List<string> c = new List<string>();
       

            for (int i = 2; i < inputs.Length; i++)
            {
                string input = inputs[i];
                string src = input.Substring(0, 3);
                string l = input.Substring(7, 3);
                string r = input.Substring(12, 3);
                d.Add(src, (l, r));

                if(src.EndsWith("A"))
                {
                    c.Add(src);
                }
            }

            (string, int,int, bool)[] creg = new (string, int, int, bool)[c.Count()];
            for (int i = 0; i < creg.Length; i++)
                creg[i] = ("", 0, 0, false);
            /*
            while (!c.All(x => x.EndsWith("Z")))
            {
                for (int i = 0; i < c.Count; i++)
                {
                    string s = c[i];
                    if (instr[res % instr.Length] == 'L')
                        c[i] = d[s].Item1;
                    else if (instr[res % instr.Length] == 'R')
                        c[i] = d[s].Item2;
                }
                res++;
                Console.WriteLine(res);
            }
            */
            while (!creg.All(x => x.Item4))
            {
                for (int i = 0; i < 6; i++)
                {
                    string s = c[i];
                    if (instr[res % instr.Length] == 'L')
                        c[i] = d[s].Item1;
                    else if (instr[res % instr.Length] == 'R')
                        c[i] = d[s].Item2;

                    if (creg[i].Item4)
                        continue;
                    
                    if (c[i].EndsWith('Z') && creg[i].Item1 == c[i])
                    {
                        if (c[i] == creg[i].Item1)
                        {
                            creg[i].Item3 = res+1 - creg[i].Item2;
                            creg[i].Item4 = true;
                        }
                    }
                    else if (c[i].EndsWith('Z'))
                    {
                        creg[i].Item1 = c[i];
                        creg[i].Item2 = res+1;
                    }
                }
                res++;
            }
            ulong sum = (ulong)creg[0].Item2;

            for (int i = 1; i < 6; i++)
            {
                sum = (sum * (ulong)creg[i].Item2) / GCD(sum, (ulong)creg[i].Item2);
            }

            OutputLoader.WriteOutput(sum.ToString());
        }

        private static ulong GCD(ulong a, ulong b)
        {
            while (a != 0 && b != 0)
            {
                if (a > b)
                    a %= b;
                else
                    b %= a;
            }

            return a | b;
        }
    }
}
