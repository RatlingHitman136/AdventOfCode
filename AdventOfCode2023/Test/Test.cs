using AdventOfCode.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Test
{
    public static class Test
    {
        public static void ERA_HA_10_4Fach_Cache()
        {
            string[] data = InputLoader.GetInput();
            string output = "";
            foreach (var item in data)
            {
                int val = Convert.ToInt32(item, 16);
                int set = (val & 56) >> 3;
                int saveData = val >> 6;
                output += "set: " + set.ToString() + " - val: " + saveData.ToString("X") + "\n"; 
            }

            OutputLoader.WriteOutput(output);
        }
    }
}
