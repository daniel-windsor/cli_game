using System;

namespace cli_game
{
    class Skill
    {
      public Skill(string name) {
        this.Level = 0;
        this.Progress = 0;
        this.Goal = 5;
        this.Name = name;
      }

      public string Name
      { get; set; }

      public int Level
      { get; private set; }

      private int Progress
      { get; set; }

      private int Goal
      { get; set;}

      public void TakeAction() {
        Progress += 1;
        if (Progress >= 5) {
          Level += 1;
          Progress = 0;
          Goal = Goal * 2;
          Console.WriteLine($"Your {Name} level is now {Level}");
        }
      }
    }
}