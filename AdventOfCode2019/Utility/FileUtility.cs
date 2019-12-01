using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2019.Utility
{
    public static class FileUtility
    {
        /// <summary>
        /// Splits a file into values of type T by carriage return, using a defined parser function.
        /// Returns the values as a List of type T
        /// </summary>
        /// <typeparam name="T">The type to cast each line into</typeparam>
        /// <param name="filePath">The path of the file, relative to the root of the main project</param>
        /// <param name="parser">The parser function used to cast each row into type T</param>
        /// <returns>A List of type T from each line of the file</returns>
        public static List<T> ParseFileToList<T>(string filePath, Func<string, T> parser)
        {
            List<T> splits = new List<T>();
            string line;
            StreamReader file = new StreamReader(filePath);

            // Iterate over each line in the input
            while ((line = file.ReadLine()) != null)
            {
                splits.Add(parser(line));
            }
            file.Close();
            return splits;
        }
    }
}