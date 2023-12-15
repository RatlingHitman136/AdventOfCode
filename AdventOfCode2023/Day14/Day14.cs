using AdventOfCode.Tools;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Day14
{
    public static class Day14
    {
        public static void Part1()
        {
            string[] input = InputLoader.GetInput();
            int res = 0;

            for(int i = 0; i< input[0].Length; i++)
            {
                int sum = 0;
                int firstFree = 0;
                int counter = 0;
                for(int j = 0; j< input.Length; j++)
                {
                    char c = input[j][i];
                    if (c == 'O')
                        counter++;
                    if(c == '#')
                    {
                        sum += (input.Length - firstFree - counter) * counter + counter * (counter + 1) / 2;
                        counter = 0;
                        firstFree = j + 1;
                    }
                }

                sum += (input.Length - firstFree - counter) * counter + counter * (counter + 1) / 2;

                res += sum;
            }

            OutputLoader.WriteOutput(res.ToString());
        }

        public static void Part2()
        {
            string[] input = InputLoader.GetInput();
            int res = 0;
            string[] tmpPrev = new string[input.Length];
            input.CopyTo(tmpPrev,0);

            for (int num = 0; num < 1000000000; num++)
            {
               
                for (int i = 0; i < input[0].Length; i++)
                {

                    int firstFree = 0;
                    int counter = 0;
                    for (int j = 0; j < input.Length; j++)
                    {
                        char c = input[j][i];
                        if (c == 'O')
                        {
                            char[] cs = input[j].ToCharArray();
                            cs[i] = '.';
                            input[j] = string.Concat(cs);

                            cs = input[firstFree + counter].ToCharArray();
                            cs[i] = 'O';
                            input[firstFree + counter] = string.Concat(cs);
                            counter++;
                        }
                        if (c == '#')
                        {
                            counter = 0;
                            firstFree = j + 1;
                        }
                    }
                }

                for (int i = 0; i < input.Length; i++)
                {

                    int firstFree = 0;
                    int counter = 0;
                    for (int j = 0; j < input[0].Length; j++)
                    {
                        char c = input[i][j];
                        if (c == 'O')
                        {
                            char[] cs = input[i].ToCharArray();
                            cs[j] = '.';
                            input[i] = string.Concat(cs);

                            cs = input[i].ToCharArray();
                            cs[firstFree + counter] = 'O';
                            input[i] = string.Concat(cs);
                            counter++;
                        }
                        if (c == '#')
                        {
                            counter = 0;
                            firstFree = j + 1;
                        }
                    }
                }

                for (int i = input[0].Length - 1; i >= 0; i--)
                {

                    int firstFree = input.Length - 1;
                    int counter = 0;
                    for (int j = input.Length - 1; j >= 0; j--)
                    {
                        char c = input[j][i];
                        if (c == 'O')
                        {
                            char[] cs = input[j].ToCharArray();
                            cs[i] = '.';
                            input[j] = string.Concat(cs);

                            cs = input[firstFree - counter].ToCharArray();
                            cs[i] = 'O';
                            input[firstFree - counter] = string.Concat(cs);
                            counter++;
                        }
                        if (c == '#')
                        {
                            counter = 0;
                            firstFree = j - 1;
                        }
                    }
                }

                for (int i = input.Length - 1; i >= 0; i--)
                {

                    int firstFree = input[0].Length - 1;
                    int counter = 0;
                    for (int j = input[0].Length - 1; j >= 0; j--)
                    {
                        char c = input[i][j];
                        if (c == 'O')
                        {
                            char[] cs = input[i].ToCharArray();
                            cs[j] = '.';
                            input[i] = string.Concat(cs);

                            cs = input[i].ToCharArray();
                            cs[firstFree - counter] = 'O';
                            input[i] = string.Concat(cs);
                            counter++;
                        }
                        if (c == '#')
                        {
                            counter = 0;
                            firstFree = j - 1;
                        }
                    }
                }
                bool flag = true;
                for (int i = 0; i < tmpPrev.Length; i++)
                {
                    string item = tmpPrev[i];
                    if (item != input[i])
                    {
                        flag = false;
                        Console.WriteLine(item + " - " + input[i]);
                        break;
                    }
                }
                if (flag) { break; }
                input.CopyTo(tmpPrev, 0);

            }

            for (int i = 0; i < input[0].Length; i++)
            {
                int sum = 0;
                int firstFree = 0;
                int counter = 0;
                for (int j = 0; j < input.Length; j++)
                {
                    char c = input[j][i];
                    if (c == 'O')
                        counter++;
                    if (c == '#')
                    {
                        sum += (input.Length - firstFree - counter) * counter + counter * (counter + 1) / 2;
                        counter = 0;
                        firstFree = j + 1;
                    }
                }

                sum += (input.Length - firstFree - counter) * counter + counter * (counter + 1) / 2;

                res += sum;
            }

            OutputLoader.WriteOutput(res.ToString());
        }
    }
}
