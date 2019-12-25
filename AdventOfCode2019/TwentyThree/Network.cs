using AdventOfCode2019.Utility;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019.TwentyThree
{
    public class Network
    {
        private Dictionary<long, IntCodeComputer.IntCodeComputer> _computers;
        private Dictionary<long, Queue<Packet>> _inputQueue;

        private Packet _nat;
        private bool _useNat;
        private HashSet<long> _natSentYs;

        public Network(string filePath, int numberOfComputers, bool useNat)
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

            _nat = null;
            _useNat = useNat;
            _natSentYs = new HashSet<long>();
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
                    if (output[i] == 255)
                    {
                        // Check for exit condition - no nat
                        if (!_useNat)
                            return output[i + 2];

                        // Otherwise, sent to nat
                        _nat = new Packet(output[i + 1], output[i + 2]);
                    }
                    else
                    {
                        _inputQueue[output[i]].Enqueue(new Packet(output[i + 1], output[i + 2]));
                    }
                }
            }

            // Check to see if all are idle
            if (_useNat && _inputQueue.Sum(i => i.Value.Count) == 0)
            {
                // Check for exit condition
                if (_natSentYs.Contains(_nat.Y))
                    return _nat.Y;

                _inputQueue[0].Enqueue(_nat);
                _natSentYs.Add(_nat.Y);
            }

            return null;
        }
    }
}