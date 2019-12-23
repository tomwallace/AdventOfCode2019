﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using AdventOfCode2019.Utility;

namespace AdventOfCode2019.Seventeen
{
    public class DaySeventeen : IAdventProblemSet
    {
        public const string INPUT = "1,330,331,332,109,4286,1102,1,1182,16,1101,1491,0,24,102,1,0,570,1006,570,36,1002,571,1,0,1001,570,-1,570,1001,24,1,24,1106,0,18,1008,571,0,571,1001,16,1,16,1008,16,1491,570,1006,570,14,21102,58,1,0,1105,1,786,1006,332,62,99,21101,0,333,1,21101,0,73,0,1105,1,579,1102,0,1,572,1102,1,0,573,3,574,101,1,573,573,1007,574,65,570,1005,570,151,107,67,574,570,1005,570,151,1001,574,-64,574,1002,574,-1,574,1001,572,1,572,1007,572,11,570,1006,570,165,101,1182,572,127,1002,574,1,0,3,574,101,1,573,573,1008,574,10,570,1005,570,189,1008,574,44,570,1006,570,158,1106,0,81,21101,0,340,1,1106,0,177,21101,0,477,1,1106,0,177,21102,514,1,1,21102,176,1,0,1106,0,579,99,21102,1,184,0,1105,1,579,4,574,104,10,99,1007,573,22,570,1006,570,165,101,0,572,1182,21101,0,375,1,21101,0,211,0,1106,0,579,21101,1182,11,1,21101,0,222,0,1105,1,979,21102,388,1,1,21102,233,1,0,1106,0,579,21101,1182,22,1,21102,244,1,0,1105,1,979,21101,0,401,1,21102,255,1,0,1105,1,579,21101,1182,33,1,21101,266,0,0,1106,0,979,21101,414,0,1,21102,277,1,0,1106,0,579,3,575,1008,575,89,570,1008,575,121,575,1,575,570,575,3,574,1008,574,10,570,1006,570,291,104,10,21101,0,1182,1,21101,313,0,0,1105,1,622,1005,575,327,1101,0,1,575,21102,1,327,0,1105,1,786,4,438,99,0,1,1,6,77,97,105,110,58,10,33,10,69,120,112,101,99,116,101,100,32,102,117,110,99,116,105,111,110,32,110,97,109,101,32,98,117,116,32,103,111,116,58,32,0,12,70,117,110,99,116,105,111,110,32,65,58,10,12,70,117,110,99,116,105,111,110,32,66,58,10,12,70,117,110,99,116,105,111,110,32,67,58,10,23,67,111,110,116,105,110,117,111,117,115,32,118,105,100,101,111,32,102,101,101,100,63,10,0,37,10,69,120,112,101,99,116,101,100,32,82,44,32,76,44,32,111,114,32,100,105,115,116,97,110,99,101,32,98,117,116,32,103,111,116,58,32,36,10,69,120,112,101,99,116,101,100,32,99,111,109,109,97,32,111,114,32,110,101,119,108,105,110,101,32,98,117,116,32,103,111,116,58,32,43,10,68,101,102,105,110,105,116,105,111,110,115,32,109,97,121,32,98,101,32,97,116,32,109,111,115,116,32,50,48,32,99,104,97,114,97,99,116,101,114,115,33,10,94,62,118,60,0,1,0,-1,-1,0,1,0,0,0,0,0,0,1,42,16,0,109,4,1202,-3,1,586,21001,0,0,-1,22101,1,-3,-3,21102,1,0,-2,2208,-2,-1,570,1005,570,617,2201,-3,-2,609,4,0,21201,-2,1,-2,1106,0,597,109,-4,2106,0,0,109,5,1202,-4,1,629,21002,0,1,-2,22101,1,-4,-4,21102,1,0,-3,2208,-3,-2,570,1005,570,781,2201,-4,-3,653,20101,0,0,-1,1208,-1,-4,570,1005,570,709,1208,-1,-5,570,1005,570,734,1207,-1,0,570,1005,570,759,1206,-1,774,1001,578,562,684,1,0,576,576,1001,578,566,692,1,0,577,577,21101,0,702,0,1106,0,786,21201,-1,-1,-1,1106,0,676,1001,578,1,578,1008,578,4,570,1006,570,724,1001,578,-4,578,21102,1,731,0,1106,0,786,1106,0,774,1001,578,-1,578,1008,578,-1,570,1006,570,749,1001,578,4,578,21102,1,756,0,1106,0,786,1106,0,774,21202,-1,-11,1,22101,1182,1,1,21102,1,774,0,1105,1,622,21201,-3,1,-3,1105,1,640,109,-5,2106,0,0,109,7,1005,575,802,21002,576,1,-6,20101,0,577,-5,1106,0,814,21102,0,1,-1,21102,1,0,-5,21101,0,0,-6,20208,-6,576,-2,208,-5,577,570,22002,570,-2,-2,21202,-5,43,-3,22201,-6,-3,-3,22101,1491,-3,-3,2101,0,-3,843,1005,0,863,21202,-2,42,-4,22101,46,-4,-4,1206,-2,924,21102,1,1,-1,1106,0,924,1205,-2,873,21102,1,35,-4,1105,1,924,1202,-3,1,878,1008,0,1,570,1006,570,916,1001,374,1,374,1202,-3,1,895,1102,2,1,0,1201,-3,0,902,1001,438,0,438,2202,-6,-5,570,1,570,374,570,1,570,438,438,1001,578,558,921,21001,0,0,-4,1006,575,959,204,-4,22101,1,-6,-6,1208,-6,43,570,1006,570,814,104,10,22101,1,-5,-5,1208,-5,65,570,1006,570,810,104,10,1206,-1,974,99,1206,-1,974,1101,0,1,575,21101,973,0,0,1106,0,786,99,109,-7,2105,1,0,109,6,21101,0,0,-4,21101,0,0,-3,203,-2,22101,1,-3,-3,21208,-2,82,-1,1205,-1,1030,21208,-2,76,-1,1205,-1,1037,21207,-2,48,-1,1205,-1,1124,22107,57,-2,-1,1205,-1,1124,21201,-2,-48,-2,1106,0,1041,21102,-4,1,-2,1105,1,1041,21101,-5,0,-2,21201,-4,1,-4,21207,-4,11,-1,1206,-1,1138,2201,-5,-4,1059,2102,1,-2,0,203,-2,22101,1,-3,-3,21207,-2,48,-1,1205,-1,1107,22107,57,-2,-1,1205,-1,1107,21201,-2,-48,-2,2201,-5,-4,1090,20102,10,0,-1,22201,-2,-1,-2,2201,-5,-4,1103,2101,0,-2,0,1105,1,1060,21208,-2,10,-1,1205,-1,1162,21208,-2,44,-1,1206,-1,1131,1106,0,989,21102,1,439,1,1106,0,1150,21102,1,477,1,1106,0,1150,21102,514,1,1,21101,0,1149,0,1105,1,579,99,21101,0,1157,0,1105,1,579,204,-2,104,10,99,21207,-3,22,-1,1206,-1,1138,1201,-5,0,1176,2102,1,-4,0,109,-6,2105,1,0,4,11,32,1,9,1,32,1,9,1,32,1,9,1,32,1,9,1,32,1,9,1,32,1,9,1,32,1,9,1,32,13,40,1,1,1,40,1,1,1,40,1,1,1,40,11,34,1,7,1,34,1,7,1,34,1,7,1,34,11,3,13,24,1,1,1,3,1,36,1,1,1,3,1,36,1,1,1,3,1,36,1,1,1,3,1,36,1,1,1,3,1,36,1,1,1,3,1,36,1,1,1,3,1,28,9,1,1,3,1,28,1,9,1,3,1,16,9,3,1,5,9,16,1,7,1,3,1,5,1,3,1,20,1,7,1,1,13,20,1,7,1,1,1,1,1,5,1,24,1,7,1,1,1,1,1,5,1,24,1,7,1,1,1,1,1,5,1,24,1,7,1,1,1,1,1,5,1,24,1,7,1,1,1,1,1,5,1,24,13,5,1,32,1,1,1,7,1,20,13,1,1,7,1,20,1,13,1,7,1,20,1,13,9,20,1,42,1,1,9,32,1,1,1,40,1,1,1,40,1,1,1,40,1,1,1,40,1,1,1,40,1,1,1,40,1,1,1,40,11,34,1,7,1,34,1,7,1,34,1,7,1,34,11,40,1,1,1,40,1,1,1,40,1,1,1,40,13,32,1,9,1,32,1,9,1,32,1,9,1,32,1,9,1,32,1,9,1,32,1,9,1,32,1,9,1,32,11,20";
        
        public string Description()
        {
            return "Set and Forget [IntCodeComputer] [HARD]";
        }

        public int SortOrder()
        {
            return 17;
        }

        public string PartA()
        {
            long alignmentParameters = CalculateCameraAlignmentParameters(INPUT);
            return alignmentParameters.ToString();
        }

        public string PartB()
        {
            long output = GetOutputAfterTraversal(INPUT);
            return output.ToString();
        }

        public long CalculateCameraAlignmentParameters(string memoryInput)
        {
            IntCodeComputer.IntCodeComputer computer = new IntCodeComputer.IntCodeComputer(memoryInput, 0);
            computer.ProcessInstructions();
            List<long> output = computer.GetOutput();

            PrintLayout(output);

            char[,] grid = MakeGrid(output);
            
            List<int> cameraAlignments = new List<int>();

            for (int x = 0; x < grid.GetLength(0); x++)
            {
                for (int y = 0; y < grid.GetLength(1); y++)
                {
                    char current = grid[x, y];
                    if (current == '#')
                    {
                        bool north = FindChar(grid, x, y + 1) == '#';
                        bool east = FindChar(grid, x+1, y) == '#';
                        bool south = FindChar(grid, x, y - 1) == '#';
                        bool west = FindChar(grid, x-1, y) == '#';

                        // It is an intersection
                        if (north && east && south && west)
                        {
                            int alignment = x * y;
                            cameraAlignments.Add(alignment);
                        }
                    }
                }
            }

            return cameraAlignments.Sum();
        }

        public long GetOutputAfterTraversal(string memoryInput)
        {
            // TODO: I have triple checked these directions and they are right, but my solution still is incorrect.
            List<long> inputs = ConvertStringToAscii("A,B,A,B,C,C,D,A,B,C");
            inputs.AddRange(ConvertStringToAscii("L,12,L,10,R,8"));
            inputs.AddRange(ConvertStringToAscii("L,12,R,8,R,10,R,12"));
            inputs.AddRange(ConvertStringToAscii("L,10,R,12,R,8"));
            inputs.AddRange(ConvertStringToAscii("R,8,R,10,R,12"));
            inputs.AddRange(ConvertStringToAscii("y"));

            IntCodeComputer.IntCodeComputer computer = new IntCodeComputer.IntCodeComputer(memoryInput, inputs.ToArray());
            computer.SetMemoryLocation(0,2);
            int code = computer.ProcessInstructions();

            PrintLayout(computer.GetOutput());

            long output = computer.GetDiagnosticCode();
            return output;
        }

        private List<long> ConvertStringToAscii(string line)
        {
            char[] split = line.ToCharArray();
            List<long> list = split.Select(c => (long) c).ToList();

            if (list.Count > 20)
                throw new ArgumentException("Each line of program can only be up to 20 instructions long.");

            // Add the line break
            list.Add(10);

            return list;
        }

        private char? FindChar(char[,] grid, int x, int y)
        {
            if (x >= grid.GetLength(0) || x < 0)
                return null;

            if (y >= grid.GetLength(1) - 1 || y < 0)
                return null;

            return grid[x, y];
        }

        private void PrintLayout(List<long> output)
        {
            Debug.WriteLine("Printing Layout ---------------------------");
            Debug.WriteLine("");
            
            foreach (long unicode in output)
            {
                char character = (char)unicode;
                Debug.Write(character.ToString());
            }

            Debug.WriteLine("");
            Debug.WriteLine("End Layout ---------------------------");
        }

        private void PrintLayout(char[,] grid)
        {
            // TODO: Need to fix the output, as it is inversed again
            
            Debug.WriteLine("Printing Layout ---------------------------");
            Debug.WriteLine("");

            for (int colPointer = grid.GetLength(1) - 1; colPointer >= 0; colPointer--)
            {
                string row = "";

                for (int rowPointer = grid.GetLength(0) - 1; rowPointer >= 0; rowPointer--)
                {
                    row = $"{row}{grid[colPointer, rowPointer]}";
                }

                Debug.WriteLine(row);
            }

            Debug.WriteLine("");
            Debug.WriteLine("End Layout ---------------------------");
        }

        private char[,] MakeGrid(List<long> output)
        {
            int rows = output.Count(o => o == 10) - 1;
            int cols = output.IndexOf(10);

            char[,] grid = new char[cols,rows];
            int x = 0;
            int y = 0;

            foreach (long unicode in output)
            {
                if (unicode == 10)
                {
                    x = 0;
                    y++;
                }
                else
                {
                    char character = (char)unicode;
                    grid[x, y] = character;

                    x++;
                }
            }

            //PrintLayout(grid);

            return grid;
        }
    }
}