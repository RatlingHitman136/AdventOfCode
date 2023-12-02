using AdventOfCode.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Day2
{
    public static class Day2
    {
        public static void Part1()
        {
            string[] inputs = InputLoader.GetInput();

            int red = 12;
            int green = 13;
            int blue = 14;

            int resSum = 0;

            foreach (string input in inputs)
            {
                string[] sub = input.Split(':');
                int gameId = Convert.ToInt32(sub[0].Split(' ')[1]);
                bool isGamePos = true;
                string[] rounds = sub[1].Trim().Split(';');
                foreach (string round in rounds)
                {
                    string[] cubes = round.Trim().Split(",");
                    foreach (string cube in cubes)
                    {
                        int num = Convert.ToInt32(cube.Trim().Split(' ')[0]);
                        string cubeType = (cube.Trim().Split(' ')[1]);
                        switch (cubeType)
                        {
                            case "blue":
                                if(num > blue)
                                    isGamePos = false;
                                break;
                            case "red":
                                if(num>red)
                                    isGamePos = false;
                                break;
                            case "green":
                                if(num>green)
                                    isGamePos = false;
                                break;
                        }
                    }
                }

                if (isGamePos)
                    resSum += gameId;
            }

            OutputLoader.WriteOutput(new string[] { resSum.ToString() });
        }


        public static void Part2()
        {
            string[] inputs = InputLoader.GetInput();

            int resSum = 0;

            foreach (string input in inputs)
            {
                string[] sub = input.Split(':');
                int gameId = Convert.ToInt32(sub[0].Split(' ')[1]);
                string[] rounds = sub[1].Trim().Split(';');

                int minRed = 0;
                int minGreen = 0;
                int minBlue = 0;


                foreach (string round in rounds)
                {
                    string[] cubes = round.Trim().Split(",");
                    foreach (string cube in cubes)
                    {
                        int num = Convert.ToInt32(cube.Trim().Split(' ')[0]);
                        string cubeType = (cube.Trim().Split(' ')[1]);
                        switch (cubeType)
                        {
                            case "blue":
                                if (num > minBlue)
                                    minBlue = num;
                                break;
                            case "red":
                                if (num > minRed)
                                    minRed = num;
                                break;
                            case "green":
                                if (num > minGreen)
                                    minGreen = num;
                                break;
                        }
                    }
                }

                resSum += minBlue * minGreen * minRed; ;
            }

            OutputLoader.WriteOutput(new string[] { resSum.ToString() });
        }
    }
}
