using AdventOfCode.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Day3
{
    public static class Day3
    {
        public static void Part1()
        {
            string[] inputs = InputLoader.GetInput();
            long sum = 0;
            for (int i = 0; i < inputs.Length; i++)
            {
                string input = inputs[i];
                int curIndex = 0;
                bool isNumber = false;

                int leftInd = 0;
                int rightInd = 0;
                int num = 0;

                while (curIndex < input.Length)
                {
                    if (getN(input[curIndex]) != -1)
                    {
                        if (!isNumber)
                        {
                            num = 0;
                            isNumber = true;
                            leftInd = curIndex;
                        }
                        num = num * 10 + getN(input[curIndex]);
                    }
                    else
                    {
                        if (isNumber)
                        {
                            isNumber = false;
                            rightInd = curIndex - 1;
                            bool isAdj = false;
                            if (i == 0) 
                                isAdj = IsAdj(leftInd, rightInd, null, inputs[i], inputs[i+1]);
                            else if (i == inputs.Length-1 )
                                isAdj = IsAdj(leftInd, rightInd, inputs[i-1], inputs[i], null);
                            else
                                isAdj = IsAdj(leftInd, rightInd, inputs[i - 1], inputs[i], inputs[i+1]);

                            if (isAdj)
                                sum += num;
                        }
                    }

                    if(isNumber && curIndex == input.Length-1)
                    {
                        isNumber = false;
                        rightInd = curIndex - 1;
                        bool isAdj = false;
                        if (i == 0)
                            isAdj = IsAdj(leftInd, rightInd, null, inputs[i], inputs[i + 1]);
                        else if (i == inputs.Length - 1)
                            isAdj = IsAdj(leftInd, rightInd, inputs[i - 1], inputs[i], null);
                        else
                            isAdj = IsAdj(leftInd, rightInd, inputs[i - 1], inputs[i], inputs[i + 1]);

                        if (isAdj)
                            sum += num;
                    }

                    curIndex++;
                }
            }

            OutputLoader.WriteOutput(new string[] { sum.ToString() });
        }

        public static bool IsAdj(int leftIndex, int rightIndex, string rowAbove, string curRow, string rowBelow)
        {
            for (int i = Math.Max(0, leftIndex - 1); i <= Math.Min(rightIndex + 1, curRow.Length - 1); i++)
            {
                if (rowAbove is not null && rowAbove.Length > i && getN(rowAbove[i]) == -1 && rowAbove[i] != '.')
                    return true;
                if (curRow is not null && curRow.Length > i && getN(curRow[i]) == -1 && curRow[i] != '.')
                    return true;
                if (rowBelow is not null && rowBelow.Length > i && getN(rowBelow[i]) == -1 && rowBelow[i] != '.')
                    return true;
            }
            return false;
        }

        public static int getN(char c)
        {
            if(c > 47 && c < 58 )
                        return (c - 48);
            return -1;
        }

        public static void Part2()
        {
            string[] inputs = InputLoader.GetInput();
            long sum = 0;

            for (int i = 0; i < inputs.Length; i++)
            {
                for (int j = 0; j < inputs[i].Length; j++)
                {
                    if (inputs[i][j] != '*')
                        continue;

                    List<int> numbers = new List<int>();

                    if(i - 1 >= 0)
                    {
                        if (getN(inputs[i - 1][j]) != -1)
                            numbers.Add(getWholeNumber(j, inputs[i - 1]));
                        else
                        {
                            if (j - 1 >= 0 && getN(inputs[i - 1][j-1]) != -1) 
                            {
                                numbers.Add(getWholeNumber(j-1, inputs[i - 1]));
                            }

                            if (j + 1 < inputs[i-1].Length && getN(inputs[i - 1][j + 1]) != -1)
                            {
                                numbers.Add(getWholeNumber(j + 1, inputs[i - 1]));
                            }
                        }
                    }
                    if (i + 1 < inputs.Length)
                    {
                        if (getN(inputs[i + 1][j]) != -1)
                            numbers.Add(getWholeNumber(j, inputs[i + 1]));
                        else
                        {
                            if (j - 1 >= 0 && getN(inputs[i + 1][j - 1]) != -1)
                            {
                                numbers.Add(getWholeNumber(j - 1, inputs[i + 1]));
                            }

                            if (j + 1 < inputs[i + 1].Length && getN(inputs[i + 1][j + 1]) != -1)
                            {
                                numbers.Add(getWholeNumber(j + 1, inputs[i + 1]));
                            }
                        }
                    }

                    if (j - 1 >= 0)
                    {
                        if (getN(inputs[i][j - 1]) != -1)
                            numbers.Add(getWholeNumber(j - 1, inputs[i]));
                    }

                    if (j + 1 < inputs[i].Length)
                    {
                        if (getN(inputs[i][j + 1]) != -1)
                            numbers.Add(getWholeNumber(j + 1, inputs[i]));
                    }

                    if (numbers.Count > 1)
                        sum += numbers[0] * numbers[1];

                }
            }

            OutputLoader.WriteOutput(new string[] { sum.ToString() });
        }

        public static int getWholeNumber(int index, string row)
        {
            int curI = index;
            while (curI >= 0 && getN(row[curI]) != -1)
                curI--;
            curI++;
            int res = 0;
            while(curI < row.Length && getN(row[curI]) != -1)
            {
                res = res * 10 + getN(row[curI]);
                curI++;
            }
            return res;
        }
    }
}
