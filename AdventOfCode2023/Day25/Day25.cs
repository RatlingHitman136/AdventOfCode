using AdventOfCode.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Day25
{
    public static class Day25
    {
        public static void Part1()
        {
            string[] inputs = InputLoader.GetInput();
            int res = 0;

            Dictionary<string, List<string>> connections = new Dictionary<string, List<string>>();
            foreach (string input in inputs)
            {
                string source = input.Split(": ")[0];
                string[] rest = input.Split(": ")[1].Split(" ");
                if(!connections.ContainsKey(source))
                    connections.Add(source, new List<string>());
                foreach (var item in rest)
                {
                    connections[source].Add(item);
                    if (!connections.ContainsKey(item))
                        connections.Add(item, new List<string>());
                    connections[item].Add(source);
                }
            }



            bool a = IsSplitted(connections, out res);


            OutputLoader.WriteOutput(res.ToString());
        }

        public static bool IsSplitted(Dictionary<string, List<string>> conns, out int res)
        {
            res = 0;
            Dictionary<string, bool> hasSeen = new Dictionary<string, bool>();
            conns.Keys.ToList().ForEach(key => hasSeen.Add(key, false));

            string start = conns.Keys.First();
            Queue<string> q = new Queue<string>();
            q.Enqueue(start);
            while(q.Count > 0)
            {
                string key = q.Dequeue();
                if (hasSeen[key])
                    continue;
                hasSeen[key] = true;
                res++;
                foreach (var dest in conns[key])
                {
                    if (!hasSeen[dest])
                    {
                        q.Enqueue(dest);
                    }
                }
            }
            if (hasSeen.Values.All(x => x))
                return false;
            res = res * (hasSeen.Count() - res);
            return true;

        }

        public static void Part2()
        {

        }
    }
}
