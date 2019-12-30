using System;
using System.Collections.Generic;

namespace AdventOfCode2019.Twelve
{
    public class Moon
    {
        private Dimension _position;
        private Dimension _velocity;
        private Dictionary<string, long> _previousStates;
        private long _firstStepDuplicated;

        public Moon(string input)
        {
            _position = ParsePosition(input);
            _velocity = new Dimension();
            _previousStates = new Dictionary<string, long>();
            _firstStepDuplicated = -1;
        }

        public void ApplyGravity(Moon otherMoon)
        {
            if (otherMoon.Equals(this))
                return;

            if (otherMoon.GetPosition().X > _position.X)
                _velocity.X++;

            if (otherMoon.GetPosition().X < _position.X)
                _velocity.X--;

            if (otherMoon.GetPosition().Y > _position.Y)
                _velocity.Y++;

            if (otherMoon.GetPosition().Y < _position.Y)
                _velocity.Y--;

            if (otherMoon.GetPosition().Z > _position.Z)
                _velocity.Z++;

            if (otherMoon.GetPosition().Z < _position.Z)
                _velocity.Z--;
        }

        public Dimension GetPosition()
        {
            return _position;
        }

        public Dimension GetVelocity()
        {
            return _velocity;
        }

        public void Move(long stepNumber)
        {
            _position.X += _velocity.X;
            _position.Y += _velocity.Y;
            _position.Z += _velocity.Z;

            if (_firstStepDuplicated == -1 && _previousStates.ContainsKey(ToString()))
                _firstStepDuplicated = stepNumber;

            if (_firstStepDuplicated == -1)
                _previousStates.Add(ToString(), stepNumber);
        }

        public long FirstStepDuplicated()
        {
            return _firstStepDuplicated;
        }

        public int TotalEnergy()
        {
            return (Math.Abs(_position.X) + Math.Abs(_position.Y) + Math.Abs(_position.Z)) *
                   (Math.Abs(_velocity.X) + Math.Abs(_velocity.Y) + Math.Abs(_velocity.Z));
        }

        public override string ToString()
        {
            return $"[{_position}] [{_velocity}]";
        }

        private Dimension ParsePosition(string input)
        {
            Dimension position = new Dimension();

            string[] split = input.Split(',');
            string[] splitOne = split[0].Split('=');
            position.X = int.Parse(splitOne[1].Trim());

            string[] splitTwo = split[1].Split('=');
            position.Y = int.Parse(splitTwo[1].Trim());

            string[] splitThree = split[2].Split('=');
            string[] splitThreeNoCloseBracket = splitThree[1].Split('>');
            position.Z = int.Parse(splitThreeNoCloseBracket[0].Trim());

            return position;
        }
    }
}