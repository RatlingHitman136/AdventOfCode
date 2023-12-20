using AdventOfCode.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Day20
{
    record Module(char type, List<string> output)
    {
        public override string ToString()
        {
            return "module";
        }
    }
    record ConjModule(char type, List<string> output, Dictionary<string, int> prevInput):Module(type, output)
    {
        public int areAllSame()
        {
            if (prevInput.All(x => x.Value == 1))
                return 1;
            if (prevInput.All(x => x.Value == 0))
                return 0;
            return -1;
        }

        public override string ToString()
        {
            string res = "";
            prevInput.Values.ToList().ForEach(x => res += x.ToString());
            return "conjModule" + res;
        }
    }
    record FlipFLopModule(char type, List<string> output):Module(type, output)
    {
        public int state = 0;
        public void flip()
        {
            state = 1 - state;
        }
        public override string ToString()
        {
            return "conjModule" + state.ToString();
        }
    }
    record Signal(int type, string moduleName, string prevModuleName);

    public static class Day20
    {

        public static void Part1()
        {
            string[] inputs = InputLoader.GetInput();
            int res = 0;
            Dictionary<string, Module> modules = new Dictionary<string, Module>();
            Queue<Signal> signals = new Queue<Signal>();
            HashSet<string> set = new HashSet<string>();

            foreach (string input in inputs)
            {
                if(input.Split(' ')[0] == "broadcaster")
                {
                    string[] des = input.Split(" -> ")[1].Split(", ");
                    modules.Add("broadcaster", new Module('b', des.ToList()));
                }
                else
                {

                    string name = input.Split(" -> ")[0].Substring(1);
                    string[] des = input.Split(" -> ")[1].Split(", ");

                    if (input[0] == '&')
                        modules.Add(name, new ConjModule(input[0], des.ToList(), new Dictionary<string, int>()));
                    else if (input[0] == '%')
                    {
                        var ffmod = new FlipFLopModule(input[0], des.ToList());
                        modules.Add(name, ffmod);
                    }
                    else
                        modules.Add(name, new Module(input[0], des.ToList()));
                }
            }

            foreach (var module in modules)
            {
                foreach (var output in module.Value.output)
                {
                    if (modules.ContainsKey(output) && modules[output] is ConjModule)
                    {
                        var m = modules[output] as ConjModule;
                        m.prevInput.Add(module.Key,0);
                    }
                }
            }

            int countLow = 0;
            int countHigh = 0;

            int countLowPrev = 0;
            int countHighPrev = 0;

            for (int i = 0; i < 1000; i++)
            {

                signals.Enqueue(new Signal(0, "broadcaster", "button"));

                while (signals.Count > 0)
                {
                    Signal s = signals.Dequeue();
                    if (s.type == 0)
                        countLow++;
                    else
                        countHigh++;

                    if (!modules.ContainsKey(s.moduleName))
                        continue;
                    Module m = modules[s.moduleName];
                    if (m is FlipFLopModule)
                    {
                        var m_ff = m as FlipFLopModule;
                        if (s.type == 0)
                        {
                            m_ff.flip();
                            m_ff.output.ForEach(x => signals.Enqueue(new Signal(m_ff.state, x, s.moduleName)));
                        }
                    }
                    else if (m is ConjModule)
                    {
                        var m_conj = m as ConjModule;
                        m_conj.prevInput[s.prevModuleName] = s.type;
                        int r = m_conj.areAllSame();
                        if (r == -1)
                            continue;
                        m_conj.output.ForEach(x => signals.Enqueue(new Signal(1 - r, x, s.moduleName)));
                    }
                    else
                    {
                        m.output.ForEach(x => signals.Enqueue(new Signal(s.type, x, s.moduleName)));
                    }
                }

                string hash = "";
                modules.Values.ToList().ForEach(x => hash += x.ToString());
                if (set.Contains(hash))
                {
                    Console.WriteLine("exists " + set.Count);
                    res = (1000 / set.Count) * countLowPrev * (1000 / set.Count) * countHighPrev;
                    break;
                }
                else
                {
                    set.Add(hash);
                    countLowPrev = countLow;
                    countHighPrev = countHigh;
                }
            }
            if(res == 0)
                res = countLow*countHigh;
            OutputLoader.WriteOutput(res.ToString());
        }

        public static void Part2()
        {

        }
    }
}
