using AdventOfCode.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Day22
{
    public record Brick(Vector3 start, Vector3 end, List<Brick> supUp, int supDown);
    public static class Day22
    {
        public static void Part1()
        {
            string[] inputs = InputLoader.GetInput();
            int res = 0;

            List<Brick> bricks = new List<Brick>();
            for(int i = 0; i < inputs.Length; i++)
            {
                string[] s = inputs[i].Split('~')[0].Split(',');
                string[] e = inputs[i].Split('~')[1].Split(',');
                Vector3 sV = new Vector3(int.Parse(s[0]), int.Parse(s[1]), int.Parse(s[2]));
                Vector3 eV = new Vector3(int.Parse(e[0]), int.Parse(e[1]), int.Parse(e[2]));
                Brick b = new Brick(sV, eV, new List<Brick>(), 0);
                bricks.Add(b);
            }
            bricks.OrderBy(x => Math.Min(x.start.Z, x.end.Z));

            while (TryApplyGrav(bricks)) ;

            res = bricks.Count(x => x.supUp.All(x => x.supDown > 1));

            OutputLoader.WriteOutput(res.ToString());
        }

        public static bool TryApplyGrav(List<Brick> b)
        {
            bool flag = false;
            for (int i = 0; i < b.Count; i++)
            {
                if (b[i] != null && TryApplyGrav(b, b[i], i))
                    flag = true;
            }

            return flag;
        }

        public static bool TryApplyGrav(List<Brick> b, Brick selBrick, int ind)
        {
            if (Math.Min(selBrick.start.Z, selBrick.end.Z) == 1)
                return false;
            Vector3 dir = (selBrick.end - selBrick.start);
            dir = Vector3.Normalize(dir);
            float minHeightGap = 40000;
            List<Brick> sup = new List<Brick>();
            for (Vector3 s = selBrick.start; true; s += dir)
            {
                for (int i = 0; i < ind; i++)
                {
                    if (b[i] == null) continue;
                    Vector3 bs = b[i].start;
                    Vector3 be = b[i].end;

                    if (s.X >= Math.Min(bs.X, be.X) && s.X <= Math.Max(be.X, bs.X))
                        if (s.Y >= Math.Min(bs.Y, be.Y) && s.Y <= Math.Max(be.Y, bs.Y))
                        {
                            if (s.Z - Math.Max(bs.Z, be.Z) == minHeightGap && !sup.Contains(b[i]))
                            {
                                sup.Add(b[i]);
                            }
                            else if (s.Z - Math.Max(bs.Z, be.Z) < minHeightGap)
                            {
                                sup.Clear();
                                sup.Add(b[i]);
                            }
                            minHeightGap = Math.Min(s.Z - Math.Max(bs.Z, be.Z), minHeightGap);
                        }
                }
                if (s == selBrick.end)
                    break;
            }
            if (minHeightGap == 40000)
            {
                Brick newB =
                    new Brick(selBrick.start - new Vector3(0, 0, selBrick.start.Z - 1), selBrick.end - new Vector3(0, 0, selBrick.end.Z - 1), new List<Brick>(), sup.Count);
                sup.ForEach(x =>
                {
                    if (!x.supUp.Contains(newB))
                        x.supUp.Add(newB);
                });
                b[ind] = newB;
                return true;
            }
            else if (minHeightGap > 1)
            {
                Brick newB =
                    new Brick(selBrick.start - new Vector3(0, 0, minHeightGap - 1), selBrick.end - new Vector3(0, 0, minHeightGap - 1), new List<Brick>(), sup.Count);
                sup.ForEach(x =>
                {
                    if (!x.supUp.Contains(newB))
                        x.supUp.Add(newB);
                });
                b[ind] = newB;
                return true;
            }
            else
            {
                Brick newB =
                    new Brick(selBrick.start, selBrick.end , new List<Brick>(), sup.Count);
                sup.ForEach(x =>
                {
                    if (!x.supUp.Contains(newB))
                        x.supUp.Add(newB);
                });
                b[ind] = newB;
            }

            

            return false;
        }

        public static void Part2()
        {

        }
    }
}
