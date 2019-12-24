using AdventOfCode2019.Utility;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019.TwentyThree
{
    public class DayTwentyThree : IAdventProblemSet
    {
        public string Description()
        {
            return "Category Six [IntCodeComputer]";
        }

        public int SortOrder()
        {
            return 23;
        }

        public string PartA()
        {
            string filePath = @"TwentyThree\DayTwentyThreeInput.txt";
            long result = RunNetwork(filePath);
            return result.ToString();
        }

        public string PartB()
        {
            string filePath = @"Twenty\DayTwentyInput.txt";
            //int result = FindFewestStepsInMaze(filePath, true);
            return ""; //result.ToString();
        }

        public long RunNetwork(string filePath)
        {
            Network network = new Network(filePath, 50);
            do
            {
                long? possibleOutput = network.Execute();

                if (possibleOutput.HasValue)
                    return possibleOutput.Value;
            } while (true);
        }
    }

    public class Network
    {
        private Dictionary<long, IntCodeComputer.IntCodeComputer> _computers;
        private Dictionary<long, Queue<Packet>> _inputQueue;

        public Network(string filePath, int numberOfComputers)
        {
            _computers = new Dictionary<long, IntCodeComputer.IntCodeComputer>();
            _inputQueue = new Dictionary<long, Queue<Packet>>();

            List<string> fileLines = FileUtility.ParseFileToList(filePath, l => l);
            string input = fileLines[0];

            for (int i = 0; i < numberOfComputers; i++)
            {
                IntCodeComputer.IntCodeComputer computer = new IntCodeComputer.IntCodeComputer(input, i);

                _computers.Add(i, computer);
                _inputQueue.Add(i, new Queue<Packet>());
            }
        }

        public long? Execute()
        {
            // loop through each computer and process input instructions, if any
            foreach (long computerKey in _computers.Keys)
            {
                var computer = _computers[computerKey];
                Queue<Packet> queue = _inputQueue[computerKey];

                if (queue.Any())
                {
                    Packet packet = queue.Dequeue();
                    computer.AddInput(packet.X);
                    computer.AddInput(packet.Y);
                }
                else
                {
                    computer.AddInput(-1);
                }

                computer.ClearOutput();
                computer.ProcessInstructions();

                // Send packets
                List<long> output = computer.GetOutput();
                for (int i = 0; i < output.Count; i += 3)
                {
                    // Check for exit condition
                    if (output[i] == 255)
                        return output[i + 2];

                    _inputQueue[output[i]].Enqueue(new Packet(output[i + 1], output[i + 2]));
                }
            }

            return null;
        }
    }

    public class Packet
    {
        public long X { get; set; }

        public long Y { get; set; }

        public Packet(long x, long y)
        {
            X = x;
            Y = y;
        }
    }
}