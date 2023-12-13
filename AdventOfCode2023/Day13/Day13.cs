using AdventOfCode.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Day13
{
    public static class Day13
    {
        public static void Part1()
        {
            string[] inputs = InputLoader.GetInput();
            int res = 0;
            int cur = 0;
            while (cur<inputs.Length-1) {
                List<string> pattern = new List<string>();
                for (int i = cur; i < inputs.Length; i++)
                {
                    if (inputs[i] == "")
                    {
                        cur = i+1;
                        break;
                    }
                        
                    pattern.Add(inputs[i]);
                    if(i == inputs.Length-1)
                        cur = i+1;
                }

                int hor = FindHorReflection(pattern);
                int ver = FindVerReflection(pattern);

                if (hor != -1)
                    res += 100 * (hor+1);
                if(ver != -1)
                    res += ver+1;
            }

            OutputLoader.WriteOutput(res.ToString());
        }

        public static int FindHorReflection(List<string> pattern)
        {
            for (int i = 0; i < pattern.Count - 1; i++)
            {
                bool flag = true;
                for (int j = 0; i + 1 + j < pattern.Count && i - j >= 0; j++)
                {
                    if (pattern[i + 1 + j] != pattern[i - j])
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag)
                    return i;
            }
            return -1;
        }

        public static int FindVerReflection(List<string> pattern)
        {
            for (int i = 0; i < pattern[0].Length - 1; i++)
            {
                bool flag = true;
                for (int j = 0; i + 1 + j < pattern[0].Length && i - j >= 0; j++)
                {
                    if (pattern.All(x => x[i + j + 1] == x[i - j]))
                        continue;
                    else
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag)
                    return i;
            }
            return -1;
        }

        public static void Part2()
        {
            string[] inputs = InputLoader.GetInput();
            int res = 0;
            int cur = 0;
            while (cur < inputs.Length - 1)
            {
                List<string> pattern = new List<string>();
                for (int i = cur; i < inputs.Length; i++)
                {
                    if (inputs[i] == "")
                    {
                        cur = i + 1;
                        break;
                    }

                    pattern.Add(inputs[i]);
                    if (i == inputs.Length - 1)
                        cur = i + 1;
                }

                int hor = FindHorReflectionSmudge(pattern);
                int ver = FindVerReflectionSmudge(pattern);

                if (hor != -1)
                    res += 100 * (hor + 1);
                if (ver != -1)
                    res += ver + 1;
            }

            OutputLoader.WriteOutput(res.ToString());
        }

        public static int FindHorReflectionSmudge(List<string> pattern)
        {
            for (int i = 0; i < pattern.Count - 1; i++)
            {
                int smudgeCount = 0;
                for (int j = 0; i + 1 + j < pattern.Count && i - j >= 0; j++)
                {
                    for(int a = 0; a < pattern[0].Length; a++)
                    {
                        if (pattern[i + 1 + j][a] != pattern[i - j][a])
                            smudgeCount++;
                    }
                    if (smudgeCount > 1)
                        break;
                }
                if (smudgeCount == 1)
                    return i;
            }
            return -1;
        }

        public static int FindVerReflectionSmudge(List<string> pattern)
        {
            for (int i = 0; i < pattern[0].Length - 1; i++)
            {
                int smudgeCount = 0;
                for (int j = 0; i + 1 + j < pattern[0].Length && i - j >= 0; j++)
                {
                    for (int a = 0; a < pattern.Count; a++)
                    {
                        if (pattern[a][i + 1 + j] != pattern[a][i - j])
                            smudgeCount++;
                    }
                    if (smudgeCount > 1)
                        break;
                }
                if (smudgeCount == 1)
                    return i;
            }
            return -1;
        }
    }
}
