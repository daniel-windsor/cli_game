using System;
using System.Linq;

namespace cli_game
{
  class Fire
  {
    Random r = new Random();

    public Fire()
    {
      this.MaxLevel = 10;
      this.Level = 5;
      this.FireThreshold = new int[] { 8, 4, 1, 0 };
    }

    private int MaxLevel
    { get; set; }

    public int Level
    { get; private set; }

    public int[] FireThreshold
    { get; private set; }

    public void deltaFire(int num)
    {
      int prevLevel = Level;

      if (Level + num > MaxLevel) Level = MaxLevel;
      else if (Level + num < 0) Level = 0;
      else Level += num;

      // Console.WriteLine($"Fire level: {Level}");

      // Print fire status when moving over a threshold
      foreach(int i in FireThreshold)
      {
        if ((prevLevel >= i && Level < i) || (prevLevel < i && Level >= i))
        {
          printFireStatus();
          return;
        }
      }
    }

    public void printFireStatus()
    {
      if (Level >= FireThreshold[0])
      {
        Console.WriteLine("\nThe fire burns strongly");
      }
      else if (Level >= FireThreshold[1])
      {
        Console.WriteLine("\nThe fire crackles, providing a comforting warmth");
      }
      else if (Level >= FireThreshold[2])
      {
        Console.WriteLine("\nThe fire dwindles.  It grows cold");
      }
      else
      {
        Console.WriteLine("\nThe fire has gone out");
      }
    }

    public void randFire(int day)
    {
      int rand = r.Next(0, 15);
      if (rand <= (day / 10 + 1 )) deltaFire(-1);
    }
  }
}