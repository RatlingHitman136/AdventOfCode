using AdventOfCode.Tools;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Day7
{
    public static class Day7
    {
        public static void Part1()
        {
            string[] inputs = InputLoader.GetInput();

            List<int> bids = new List<int>();

            int[] power = new int[inputs.Length];
            int[][] handsList = new int[inputs.Length][];

            List<(int, int, int, int, int, int, int)> data = new List<(int, int, int, int, int, int, int)>();

            for (int i = 0; i < inputs.Length; i++)
            {
                string input = inputs[i];
                string hand = input.Split(' ')[0];
                List<int> handNums = new List<int>();
                foreach (char c in hand)
                {
                    if(c >= 50 && c <= 57)
                        handNums.Add(c-48);
                    if (c == 'A')
                        handNums.Add(14);
                    if (c == 'K')
                        handNums.Add(13);
                    if (c == 'Q')
                        handNums.Add(12);
                    if (c == 'J')
                        handNums.Add(11);
                    if (c == 'T')
                        handNums.Add(10);
                }
                int bid = int.Parse(input.Split(' ')[1].Trim());

                if (handNums.Distinct().Count() == 1)
                {
                    data.Add((6, handNums[0], handNums[1], handNums[2], handNums[3], handNums[4], bid));
                }
                else if (handNums.Distinct().Count() == 2 && handNums.Where(x => handNums.Count(y => x == y) == 4).Any())
                {
                    data.Add((5, handNums[0], handNums[1], handNums[2], handNums[3], handNums[4], bid));
                }
                else if (handNums.Distinct().Count() == 2)
                {
                    data.Add((4, handNums[0], handNums[1], handNums[2], handNums[3], handNums[4], bid));
                }
                else if (handNums.Distinct().Count() == 3 && handNums.Where(x => handNums.Count(y => x == y) == 3).Any())
                {
                    data.Add((3, handNums[0], handNums[1], handNums[2], handNums[3], handNums[4], bid));
                }
                else if (handNums.Distinct().Count() == 3 && handNums.Where(x => handNums.Count(y => x == y) == 2).Distinct().Count() == 2)
                {
                    data.Add((2, handNums[0], handNums[1], handNums[2], handNums[3], handNums[4], bid));
                }
                else if (handNums.Distinct().Count() == 4)
                {
                    data.Add((1, handNums[0], handNums[1], handNums[2], handNums[3], handNums[4], bid));
                }
                else if (handNums.Distinct().Count() == 5)
                {
                    data.Add((0, handNums[0], handNums[1], handNums[2], handNums[3], handNums[4], bid));
                }
            }


            data = data.OrderBy(x => x.Item6).OrderBy(x => x.Item5).OrderBy(x => x.Item4).OrderBy(x => x.Item3).OrderBy(x => x.Item2).OrderBy(x => x.Item1).ToList();
            //var  k = data.Where(x => data.Count(y => x == y) == 2).ToList();
            int sum = 0;

            for (int i = 0; i < data.Count; i++)
            {
                sum += data[i].Item7 * (i + 1);             
            }

            OutputLoader.WriteOutput(sum);

        }

        public static void Part2()
        {
            string[] inputs = InputLoader.GetInput();

            List<int> bids = new List<int>();

            int[] power = new int[inputs.Length];
            int[][] handsList = new int[inputs.Length][];

            List<(int, int, int, int, int, int, int)> data = new List<(int, int, int, int, int, int, int)>();

            for (int i = 0; i < inputs.Length; i++)
            {
                string input = inputs[i];
                string hand = input.Split(' ')[0];
                List<int> handNums = new List<int>();
                foreach (char c in hand)
                {
                    if (c >= 50 && c <= 57)
                        handNums.Add(c - 48);
                    if (c == 'A')
                        handNums.Add(14);
                    if (c == 'K')
                        handNums.Add(13);
                    if (c == 'Q')
                        handNums.Add(12);
                    if (c == 'T')
                        handNums.Add(10);
                    if (c == 'J')
                        handNums.Add(1);
                }
                int bid = int.Parse(input.Split(' ')[1].Trim());

                if (handNums.Where(x => handNums.Count(y => x == y || y == 1) == 5).Any())
                {
                    data.Add((6, handNums[0], handNums[1], handNums[2], handNums[3], handNums[4], bid));
                }
                else if (handNums.Where(x => handNums.Count(y => x == y || y == 1) == 4).Any())
                {
                    data.Add((5, handNums[0], handNums[1], handNums[2], handNums[3], handNums[4], bid));
                }
                else if (handNums.Contains(1) && handNums.Where(x => handNums.Count(y => x == y) == 2).Count() == 4)
                {
                    data.Add((4, handNums[0], handNums[1], handNums[2], handNums[3], handNums[4], bid));
                }
                else if (!handNums.Contains(1) && handNums.Where(x => handNums.Count(y => x == y) == 3).Any() && handNums.Where(x => handNums.Count(y => x == y) == 2).Any())
                {
                    data.Add((4, handNums[0], handNums[1], handNums[2], handNums[3], handNums[4], bid));
                }
                else if (handNums.Where(x => handNums.Count(y => x == y || y == 1) == 3).Any())
                {
                    data.Add((3, handNums[0], handNums[1], handNums[2], handNums[3], handNums[4], bid));
                }
                else if (handNums.Where(x => handNums.Count(y => x == y) == 2).Count() == 4)
                {
                    data.Add((2, handNums[0], handNums[1], handNums[2], handNums[3], handNums[4], bid));
                }
                else if (handNums.Where(x => handNums.Count(y => x == y) == 2).Count() == 2)
                {
                    data.Add((1, handNums[0], handNums[1], handNums[2], handNums[3], handNums[4], bid));
                }else if(handNums.Contains(1))
                {
                    data.Add((1, handNums[0], handNums[1], handNums[2], handNums[3], handNums[4], bid));
                }
                else
                {
                    data.Add((0, handNums[0], handNums[1], handNums[2], handNums[3], handNums[4], bid));
                }
            }


            data = data.OrderBy(x => x.Item6).OrderBy(x => x.Item5).OrderBy(x => x.Item4).OrderBy(x => x.Item3).OrderBy(x => x.Item2).OrderBy(x => x.Item1).ToList();
            //var  k = data.Where(x => data.Count(y => x == y) == 2).ToList();
            int sum = 0;

            for (int i = 0; i < data.Count; i++)
            {
                sum += data[i].Item7 * (i + 1);
            }

            OutputLoader.WriteOutput(sum);

        }
    }
}
