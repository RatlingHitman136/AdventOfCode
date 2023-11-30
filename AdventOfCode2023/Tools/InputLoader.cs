using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Tools
{
    public static class InputLoader
    {
        const string pathIn = "AdventOfCode.input.in";

        public static string[] GetInput()
        {
            Stream stream = Assembly.GetCallingAssembly().GetManifestResourceStream(pathIn);
            StreamReader sr = new StreamReader(stream);
            List<string> lines = new List<string>();
            while(!sr.EndOfStream)
                lines.Add(sr.ReadLine());
            sr.Close();
            return lines.ToArray();
        } 
    }
}
