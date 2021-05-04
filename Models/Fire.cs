using System;

namespace cli_game.Models
{
    internal class Fire
    {
        private readonly Random _random = new();

        public Fire()
        {
            MaxLevel = 10;
            Level = 5;
            FireThreshold = new[] {8, 4, 1, 0};
        }

        private int MaxLevel { get; }

        public int Level { get; private set; }

        public int[] FireThreshold { get; }

        public void DeltaFire(int num)
        {
            var prevLevel = Level;

            if (Level + num > MaxLevel) Level = MaxLevel;
            else if (Level + num < 0) Level = 0;
            else Level += num;

            // Console.WriteLine($"Fire level: {Level}");

            // Print fire status when moving over a threshold
            foreach (var i in FireThreshold)
                if (prevLevel >= i && Level < i || prevLevel < i && Level >= i)
                {
                    PrintFireStatus();
                    return;
                }
        }

        public void PrintFireStatus()
        {
            if (Level >= FireThreshold[0])
                Console.WriteLine("\nThe fire burns strongly");
            else if (Level >= FireThreshold[1])
                Console.WriteLine("\nThe fire crackles, providing a comforting warmth");
            else if (Level >= FireThreshold[2])
                Console.WriteLine("\nThe fire dwindles.  It grows cold");
            else
                Console.WriteLine("\nThe fire has gone out");
        }

        public void RandFire(int day)
        {
            var rand = _random.Next(0, 15);
            if (rand <= day / 10 + 1) DeltaFire(-1);
        }
    }
}