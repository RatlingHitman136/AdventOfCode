using AdventOfCode.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Day6
{
    public static class Day6
    {
        public static void Part1()
        {
            string[] inputs = InputLoader.GetInput();

            int res = 1;

            string[] time = inputs[0].Split(":")[1].Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string[] dist = inputs[1].Split(":")[1].Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);



            for (int i = 0; i < time.Length; i++) {
                int timeInt = int.Parse(time[i]);
                int distInt = int.Parse(dist[i]);

                int des = timeInt * timeInt - 4 * distInt;
                if (des < 0)
                    continue;
                double a = (-timeInt + Math.Sqrt(des)) / -2;
                double b = (-timeInt - Math.Sqrt(des)) / -2;

                int counter = 0;
                for(int j = (int)a; j < b; j++)
                    if(j > a && j < b)
                        counter++;
                res *= counter;
            }

            OutputLoader.WriteOutput(res);
        }

        public static void Part2()
        {
            string[] inputs = InputLoader.GetInput();

            long res = 1;

            string[] time = inputs[0].Split(":")[1].Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string[] dist = inputs[1].Split(":")[1].Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);

            string nT = "";
            foreach (string s in time) { nT += s; }
            string nD = "";
            foreach (string s in dist) { nD += s; }

            time = new string[] { nT };
            dist = new string[] { nD };

            for (int i = 0; i < time.Length; i++)
            {
                long timeInt = long.Parse(time[i]);
                long distInt = long.Parse(dist[i]);

                long des = timeInt * timeInt - 4 * distInt;
                if (des < 0)
                    continue;
                double a = (-timeInt + Math.Sqrt(des)) / -2;
                double b = (-timeInt - Math.Sqrt(des)) / -2;

                long counter = 0;
                for (long j = (long)a; j < b; j++)
                    if (j > a && j < b)
                        counter++;
                res *= counter;
            }

            OutputLoader.WriteOutput(res.ToString());
        }

    }
}
