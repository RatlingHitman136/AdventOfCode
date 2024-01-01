using AdventOfCode.Tools;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Day24
{
    public struct Vector3L
    {
        public long X;
        public long Y; 
        public long Z;

        public Vector3L(long x, long y, long z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static Vector3L operator +(Vector3L v1, Vector3L v2)
        {
            return new Vector3L(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        }

        public static Vector3L operator *(Vector3L v, long val)
        {
            return new Vector3L(v.X * val, v.Y * val, v.Z * val);
        }

        public override string ToString()
        {
            return "X:" + X + "  |Y:" + Y + "  |Z:" + Z; 
        }
    }
    public static class Day24
    {
        private static long minCoord = 200000000000000;
        private static long maxCoord = 400000000000000;

        public static void Part1()
        {
            string[] inputs = InputLoader.GetInput();
            int res = 0;

            List<(Vector3L, Vector3L)> vs = new List<(Vector3L, Vector3L)> ();
            foreach (var input in inputs)
            {
                string[] pos = input.Split(" @ ")[0].Trim().Split(", ");
                long xpos = long.Parse(pos[0]);
                long ypos = long.Parse(pos[1]);
                long zpos = long.Parse(pos[2]);
                Vector3L posV = new Vector3L(xpos, ypos, zpos);

                string[] vel = input.Split(" @ ")[1].Trim().Split(", ");
                long xvel = long.Parse(vel[0]);
                long yvel = long.Parse(vel[1]);
                long zvel = long.Parse(vel[2]);
                Vector3L velV = new Vector3L(xvel, yvel, zvel);
                vs.Add((posV, velV));
            }

            for (int i = 0; i < vs.Count; i++)
            {
                for (int j = i + 1; j < vs.Count; j++)
                {
                    int tmp = res;
                    if (TryIntersect2D(vs[i].Item1, vs[i].Item2, vs[j].Item1, vs[j].Item2, out double interX, out double interY, out double t1, out double t2))
                    {
                        if (t1 > 0 && t2 > 0)
                            if (interX >= minCoord && interX <= maxCoord && interY >= minCoord && interY <= maxCoord)
                                res++;
                    }
                }
            }

            OutputLoader.WriteOutput(res);
        }

        public static bool TryIntersect2D(Vector3L l1Pos, Vector3L l1Vel, Vector3L l2Pos, Vector3L l2Vel, out double interX, out double interY, out double t1, out double t2)
        {
            (Vector3L,Vector3L) l1 = (l1Pos, l1Pos + l1Vel);
            (Vector3L,Vector3L) l2 = (l2Pos, l2Pos + l2Vel);

            double slope0 = l1Vel.Y / (double) l1Vel.X;
            double slope1 = l2Vel.Y / (double) l2Vel.X;

            double d = -((l2Pos.Y - slope1 * l2Pos.X) - (l2Pos.Y - slope0 * l2Pos.X));

            if (slope0 == slope1)
            {
                    interX = l1Pos.X;
                    interY = l1Pos.Y;
                    t1 = 0;
                    t2 = 0;
                    return false;
            }

            interX = -((l2Pos.Y - slope1 * l2Pos.X) - (l1Pos.Y - slope0 * l1Pos.X)) / (slope1 - slope0);
            interY = l1Pos.Y + slope0 * (interX - l1Pos.X);
            t1 = (interX - l1Pos.X) / l1Vel.X;
            t2 = (interX - l2Pos.X) / l2Vel.X;

            return true;
        }


        public static void Part2()
        {
            string[] inputs = InputLoader.GetInput();

            List<(Vector3L, Vector3L)> vs = new List<(Vector3L, Vector3L)>();

            foreach (var input in inputs)
            {
                string[] pos = input.Split(" @ ")[0].Trim().Split(", ");
                long xpos = long.Parse(pos[0]);
                long ypos = long.Parse(pos[1]);
                long zpos = long.Parse(pos[2]);
                Vector3L posV = new Vector3L(xpos, ypos, zpos);

                string[] vel = input.Split(" @ ")[1].Trim().Split(", ");
                long xvel = long.Parse(vel[0]);
                long yvel = long.Parse(vel[1]);
                long zvel = long.Parse(vel[2]);
                Vector3L velV = new Vector3L(xvel, yvel, zvel);
                vs.Add((posV, velV));
            }

            Console.WriteLine("r = solve[{");
            string s = "";
            for (int i = 0; i < 5; i++)
            {
                Vector3L pos = vs[i].Item1;
                Vector3L vel = vs[i].Item2;

                Console.WriteLine("{1} + {2} * t{0} == x + xv * t{0},", i, pos.X, vel.X);
                Console.WriteLine("{1} + {2} * t{0} == y + yv * t{0},", i, pos.Y, vel.Y);
                Console.WriteLine("{1} + {2} * t{0} == z + zv * t{0},", i, pos.Z, vel.Z);
                s += " t" + i + ",";
            }
            Console.WriteLine("}, {x, y, z, " + s + "xv, yv, zv}];");
            Console.WriteLine("(x + y + z) //. r[[1]]");
            
        }
    }
}
