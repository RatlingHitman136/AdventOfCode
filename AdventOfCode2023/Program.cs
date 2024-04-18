
using AdventOfCode.Tools;
using AdventOfCode2023.Day1;
using AdventOfCode2023.Day10;
using AdventOfCode2023.Day11;
using AdventOfCode2023.Day12;
using AdventOfCode2023.Day13;
using AdventOfCode2023.Day14;
using AdventOfCode2023.Day15;
using AdventOfCode2023.Day16;
using AdventOfCode2023.Day17;
using AdventOfCode2023.Day18;
using AdventOfCode2023.Day19;
using AdventOfCode2023.Day2;
using AdventOfCode2023.Day20;
using AdventOfCode2023.Day21;
using AdventOfCode2023.Day22;
using AdventOfCode2023.Day23;
using AdventOfCode2023.Day24;
using AdventOfCode2023.Day25;
using AdventOfCode2023.Day3;
using AdventOfCode2023.Day4;
using AdventOfCode2023.Day5;
using AdventOfCode2023.Day6;
using AdventOfCode2023.Day7;
using AdventOfCode2023.Day8;
using AdventOfCode2023.Day9;
using AdventOfCode2023.Test;
using System.Diagnostics;

namespace AdventOfCode
{
    class Program
    {
        static public void Main(String[] args)
        {
            int a = 12;
            int b = 13;
            b = a = a++;
            Console.WriteLine(a.ToString() + " " + b.ToString());
        }
    }
}