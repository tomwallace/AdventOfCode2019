using System;
using System.Collections.Generic;

namespace AdventOfCode2019.Eleven
{
    public class PaintingRobot
    {
        public int X { get; set; }
        public int Y { get; set; }

        // 0 = up, 1 = right, 2 = down, 3 = left
        public int Facing { get; set; }

        public Dictionary<string, long> PaintedHullSections { get; set; }

        public PaintingRobot()
        {
            PaintedHullSections = new Dictionary<string, long>();
            Facing = 0;
        }

        public void ProcessInstruction(long color, long changeFacing)
        {
            // Paint section standing on
            if (PaintedHullSections.ContainsKey($"{X},{Y}"))
                PaintedHullSections[$"{X},{Y}"] = color;
            else
                PaintedHullSections.Add($"{X},{Y}", color);

            NewFacing(changeFacing);
            Move();
        }

        public long ColorCurrentlyOver()
        {
            if (PaintedHullSections.ContainsKey($"{X},{Y}"))
                return PaintedHullSections[$"{X},{Y}"];

            return 0;
        }

        private void NewFacing(long changeFacing)
        {
            // Turn left
            if (changeFacing == 0)
            {
                Facing--;
                if (Facing < 0)
                    Facing = 3;
                return;
            }

            // Turn right
            if (changeFacing == 1)
            {
                Facing++;
                if (Facing > 3)
                    Facing = 0;
                return;
            }

            throw new ArgumentException("Unrecognized facing change input");
        }

        private void Move()
        {
            // Move one square forward
            switch (Facing)
            {
                case 0:
                    Y++;
                    break;

                case 1:
                    X++;
                    break;

                case 2:
                    Y--;
                    break;

                case 3:
                    X--;
                    break;

                default:
                    throw new ArgumentException("Illegal Facing direction");
            }
        }
    }
}