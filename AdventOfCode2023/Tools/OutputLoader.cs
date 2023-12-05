using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Tools
{
    public static class OutputLoader
    {
        const string pathOut = "output.out";
        public static void WriteOutput(string[] lines)
        {
            Console.WriteLine("Saved data is:");
            foreach (string line in lines)
                Console.WriteLine(line);
            Stream stream = File.OpenWrite(pathOut);
            StreamWriter streamWriter = new StreamWriter(stream);
            streamWriter.Write(lines);
            streamWriter.Close();
        }

        public static void WriteOutput(string line)
        {
            WriteOutput(new string[] { line });
        }

        public static void WriteOutput(int num)
        {
            WriteOutput(new string[] { num.ToString() });
        }
    }
}
