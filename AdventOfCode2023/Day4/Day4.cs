using AdventOfCode.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Day4
{
    public static class Day4
    {
        public static void Part1()
        {
            string[] inputs = InputLoader.GetInput();
            int sum = 0;
            foreach (string input in inputs)
            {
                string[] gC = input.Trim().Split(':');
                string[] wNums = gC[1].Trim().Split('|')[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                List<int> wNumsL = new List<int>();
                foreach (string w in wNums)
                {
                    wNumsL.Add(Convert.ToInt32(w));
                }
                int points = 0;
                string[] myNums = gC[1].Trim().Split('|')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                foreach (string my in myNums)
                {
                    int myNum = Convert.ToInt32(my);
                    if (wNums.Contains(my))
                        if (points == 0)
                            points = 1;
                        else
                            points *= 2;
                }

                sum += points;
            }

            OutputLoader.WriteOutput(new string[] { sum.ToString() });
        }

        public static void Part2()
        {
            string[] inputs = InputLoader.GetInput();
            int[] numOfInst = new int[inputs.Length];
            for (int i = 0; i < numOfInst.Length; i++)
                numOfInst[i] = 1;
            int sum = 0;
            for (int i = 0; i < inputs.Length; i++)
            {
                string input = inputs[i];
                string[] gC = input.Trim().Split(':');
                string[] wNums = gC[1].Trim().Split('|')[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                List<int> wNumsL = new List<int>();
                foreach (string w in wNums)
                {
                    wNumsL.Add(Convert.ToInt32(w));
                }
                int points = 0;
                string[] myNums = gC[1].Trim().Split('|')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                foreach (string my in myNums)
                {
                    int myNum = Convert.ToInt32(my);
                    if (wNums.Contains(my))
                        points++;
                }

                for (int j = i+1; j <= i + points; j++)
                {
                    numOfInst[j]+=numOfInst[i];
                }

                sum += numOfInst[i];
            }

            OutputLoader.WriteOutput(new string[] { sum.ToString() });
        }
    }
}
