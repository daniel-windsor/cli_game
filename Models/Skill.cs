using System;

namespace cli_game.Models
{
    internal class Skill
    {
        public Skill(string name)
        {
            Level = 0;
            Progress = 0;
            Goal = 5;
            Name = name;
        }

        private string Name { get; set; }

        private int Level { get; set; }

        private int Progress { get; set; }

        private int Goal { get; set; }

        public void TakeAction()
        {
            Progress += 1;
            if (Progress >= 5)
            {
                Level += 1;
                Progress = 0;
                Goal = Goal * 2;
                Console.WriteLine($"Your {Name} level is now {Level}");
            }
        }
    }
}