using AdventOfCode.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Day19
{
    public record Part(Dictionary<char, int> values, char status);

    public record Rule(char val, char compChar, int compVal, string nextWorkflow);

    public record Workflow(List<Rule> rules, string endWorkflow);

    public static class Day19
    {
        public static void Part1()
        {
            string[] inputs = InputLoader.GetInput();
            int res = 0;

            List<Part> parts = new List<Part>();
            Dictionary<string, Workflow> workflows = new Dictionary<string, Workflow>();

            int tmpCounter = 0;

            for (int i = 0; i < inputs.Length; i++)
            {
                string s = inputs[i];
                if (s == "")
                {
                    tmpCounter = i;
                    break;
                }

                string workflowName = s.Split("{")[0];
                string[] workflowContents = s.Split("{")[1].Substring(0, s.Split("{")[1].Length - 1).Split(",");
                List<Rule> l_rules = new List<Rule>();
                string endWorkflow = "";
                foreach (string content in workflowContents)
                {
                    if (content.Split(":").Length == 1)
                    {
                        endWorkflow = content;
                        break;
                    }

                    Rule newRule = new Rule(content[0], content[1], int.Parse(content.Split(":")[0].Substring(2)), content.Split(":")[1]);
                    l_rules.Add(newRule);
                }
                workflows.Add(workflowName, new Workflow(l_rules, endWorkflow));
            }

            for(int i = tmpCounter + 1; i < inputs.Length; i++)
            {
                string s = inputs[i];
                string content = s.Substring(1, s.Length - 2);
                string[] vals = content.Split(",");
                Dictionary<char, int> valsDict = new Dictionary<char, int>();
                foreach (var item in vals)
                {
                    char key = item[0];
                    int val = int.Parse(item.Substring(2));
                    valsDict.Add(key, val);
                }

                Part p = new Part(valsDict, 'U');
                parts.Add(p);
            }

            int counter = 0;

            foreach (Part part in parts)
            {
                char resChar = GoThrough(part, workflows);
                if(resChar == 'A')
                {
                    counter++;
                    //Console.WriteLine("{0}, {1}, {2}, {3}", part.values['x'], part.values['m'], part.values['a'], part.values['s']);
                    res += part.values.Sum(x => x.Value);
                }
            }

            OutputLoader.WriteOutput(res.ToString());
        }

        public static char GoThrough(Part part, Dictionary<string, Workflow> workflows)
        {
            Workflow cur = workflows["in"];

            while (true)
            {
                bool flag = true;
                for(int i = 0; i < cur.rules.Count; i++)
                {
                    Rule curRule = cur.rules[i];

                    if(curRule.compChar == '>')
                    {
                        if (part.values[curRule.val] > curRule.compVal)
                        {
                            if (curRule.nextWorkflow == "A")
                                return 'A';
                            if (curRule.nextWorkflow == "R")
                                return 'R';
                            cur = workflows[curRule.nextWorkflow];
                            flag = false;
                            break;
                        }
                    }
                    else
                    {
                        if (part.values[curRule.val] < curRule.compVal)
                        {
                            if (curRule.nextWorkflow == "A")
                                return 'A';
                            if (curRule.nextWorkflow == "R")
                                return 'R';
                            cur = workflows[curRule.nextWorkflow];
                            flag = false;
                            break;
                        }
                    }
                }
                if (flag && cur.endWorkflow == "A")
                    return 'A';
                if (flag && cur.endWorkflow == "R")
                    return 'R';
                if(flag)
                    cur = workflows[cur.endWorkflow];
            }

            throw new Exception();
        }

        public static void Part2()
        {
            string[] inputs = InputLoader.GetInput();
            long res = 0;

            Dictionary<string, Workflow> workflows = new Dictionary<string, Workflow>();

            int tmpCounter = 0;
            for (int i = 0; i < inputs.Length; i++)
            {
                string s = inputs[i];
                if (s == "")
                {
                    tmpCounter = i;
                    break;
                }

                string workflowName = s.Split("{")[0];
                string[] workflowContents = s.Split("{")[1].Substring(0, s.Split("{")[1].Length - 1).Split(",");
                List<Rule> l_rules = new List<Rule>();
                string endWorkflow = "";
                foreach (string content in workflowContents)
                {
                    if (content.Split(":").Length == 1)
                    {
                        endWorkflow = content;
                        break;
                    }

                    Rule newRule = new Rule(content[0], content[1], int.Parse(content.Split(":")[0].Substring(2)), content.Split(":")[1]);
                    l_rules.Add(newRule);
                }
                workflows.Add(workflowName, new Workflow(l_rules, endWorkflow));
            }

            res = CalcCombForWorkflow(workflows["in"], workflows, 1, 4000, 1, 4000, 1, 4000, 1, 4000);

            OutputLoader.WriteOutput(res.ToString());
        }

        public static long CalcCombForWorkflow(Workflow cur, Dictionary<string, Workflow> workflows, int minX, int maxX, int minM, int maxM, int minA, int maxA, int minS, int maxS)
        {
            long res = 0;

            foreach (Rule rule in cur.rules)
            {
                if(rule.compChar == '>')
                {
                    if(rule.nextWorkflow == "R")
                        switch (rule.val)
                        {
                            case 'x': maxX = rule.compVal; break;
                            case 'a': maxA = rule.compVal; break;
                            case 'm': maxM = rule.compVal; break;
                            case 's': maxS = rule.compVal; break;
                        }
                    else if(rule.nextWorkflow == "A")
                        switch (rule.val)
                        {
                            case 'x': res += CalcComb(rule.compVal + 1, maxX, minM, maxM, minA, maxA, minS, maxS); maxX = rule.compVal; break;
                            case 'a': res += CalcComb(minX, maxX, minM, maxM, rule.compVal + 1, maxA, minS, maxS); maxA = rule.compVal; break;
                            case 'm': res += CalcComb(minX, maxX, rule.compVal + 1, maxM, minA, maxA, minS, maxS); maxM = rule.compVal; break;
                            case 's': res += CalcComb(minX, maxX, minM, maxM, minA, maxA, rule.compVal + 1, maxS); maxS = rule.compVal; break;
                        }
                    else
                        switch (rule.val)
                        {
                            case 'x': res += CalcCombForWorkflow(workflows[rule.nextWorkflow], workflows, rule.compVal + 1, maxX, minM, maxM, minA, maxA, minS, maxS); maxX = rule.compVal; break;
                            case 'a': res += CalcCombForWorkflow(workflows[rule.nextWorkflow], workflows, minX, maxX, minM, maxM, rule.compVal + 1, maxA, minS, maxS); maxA = rule.compVal; break;
                            case 'm': res += CalcCombForWorkflow(workflows[rule.nextWorkflow], workflows, minX, maxX, rule.compVal + 1, maxM, minA, maxA, minS, maxS); maxM = rule.compVal; break;
                            case 's': res += CalcCombForWorkflow(workflows[rule.nextWorkflow], workflows, minX, maxX, minM, maxM, minA, maxA, rule.compVal + 1, maxS); maxS = rule.compVal; break;
                        }
                }
                else
                {
                    if (rule.nextWorkflow == "R")
                        switch (rule.val)
                        {
                            case 'x': minX = rule.compVal; break;
                            case 'a': minA = rule.compVal; break;
                            case 'm': minM = rule.compVal; break;
                            case 's': minS = rule.compVal; break;
                        }
                    else if (rule.nextWorkflow == "A")
                        switch (rule.val)
                        {
                            case 'x': res += CalcComb(minX, rule.compVal - 1, minM, maxM, minA, maxA, minS, maxS); minX = rule.compVal; break;
                            case 'a': res += CalcComb(minX, maxX, minM, maxM, minA, rule.compVal - 1, minS, maxS); minA = rule.compVal; break;
                            case 'm': res += CalcComb(minX, maxX, minM, rule.compVal - 1, minA, maxA, minS, maxS); minM = rule.compVal; break;
                            case 's': res += CalcComb(minX, maxX, minM, maxM, minA, maxA, minS, rule.compVal - 1); minS = rule.compVal; break;
                        }
                    else
                        switch (rule.val)
                        {
                            case 'x': res += CalcCombForWorkflow(workflows[rule.nextWorkflow], workflows, minX, rule.compVal - 1, minM, maxM, minA, maxA, minS, maxS); minX = rule.compVal; break;
                            case 'a': res += CalcCombForWorkflow(workflows[rule.nextWorkflow], workflows, minX, maxX, minM, maxM, minA, rule.compVal - 1, minS, maxS); minA = rule.compVal; break;
                            case 'm': res += CalcCombForWorkflow(workflows[rule.nextWorkflow], workflows, minX, maxX, minM, rule.compVal - 1, minA, maxA, minS, maxS); minM = rule.compVal; break;
                            case 's': res += CalcCombForWorkflow(workflows[rule.nextWorkflow], workflows, minX, maxX, minM, maxM, minA, maxA, minS, rule.compVal - 1); minS = rule.compVal; break;
                        }
                }
            }

            if (cur.endWorkflow == "A")
                return res + CalcComb(minX, maxX, minM, maxM, minA, maxA, minS, maxS);
            else if (cur.endWorkflow == "R")
                return res;
            return res + CalcCombForWorkflow(workflows[cur.endWorkflow], workflows, minX, maxX, minM, maxM, minA, maxA, minS, maxS);
        }

        public static long CalcComb(long minX, long maxX, long minM, long maxM, long minA, long maxA, long minS, long maxS)
        {
            if (maxX - minX < 0 || maxM - minM < 0 || maxS - minS < 0 || maxA - minA < 0)
                return 0;
            return (maxX - minX + 1) * (maxM - minM + 1) * (maxS - minS + 1) * (maxA - minA + 1);
        }
    }
}
