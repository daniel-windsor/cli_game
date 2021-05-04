using System;

namespace cli_game
{
  class PlayerCharacter
  {
    Random r = new Random();

    public PlayerCharacter()
    {
      MaxHealth = 15;
      Health = 10;
      MaxStamina = 15;
      Stamina = 10;

      createHealthThreshold();
    }

    private int maxHealth;
    public int MaxHealth
    {
      get { return maxHealth; }
      set
      {
        maxHealth = value;
        createHealthThreshold();
      }
    }

    public int Health
    { get; private set; }

    private int[] HealthThreshold
    { get; set; }

    public int MaxStamina
    { get; set; }

    public int Stamina
    { get; private set; }

    public void deltaHealth(int delta)
    {
      int prevHealth = Health;

      if (Health + delta > MaxHealth) Health = MaxHealth;
      else if (Health + delta < 0) Health = 0;
      else
      {
        Health += delta;
        if (delta > 0) Console.WriteLine("\nYou feel a little better");
      }

      foreach (int i in HealthThreshold)
      {
        if ((prevHealth >= i && Health < i) || (prevHealth < i && Health >= i))
        {
          printHealthStatus();
          return;
        }
      }
    }

    public void randHealth(int num)
    {
      int rand = r.Next(0, 10);
      if (rand <= 2) deltaHealth(num);
    }

    public void deltaStamina(int delta)
    {
      if (Stamina + delta > MaxStamina) Stamina = MaxStamina;
      else if (Stamina + delta < 0) Stamina = 0;
      else Stamina += delta;

      if (Stamina == 3 && delta < 0)
      {
        Console.WriteLine("\nYou are feeling very tired");
      }

      if (Stamina == 0)
      {
        Console.WriteLine("\nYou are exhausted");
      }
    }

    public void sleep()
    {
      if (World.worldInv.isInInventory("bed"))
      {
        Console.WriteLine("\nYou awake feeling well rested");
        Stamina = MaxStamina;
      }
      else
      {
        Console.WriteLine("\nIt is hard sleeping on the floor.  You did not sleep well");
        Stamina = 10;
      }
    }

    // Adjusts the 'cutoff' values for each health threshold based on current max health value
    private void createHealthThreshold()
    {
      int b1 = (int)Math.Ceiling((decimal)MaxHealth / 2);
      int b2 = (int)Math.Ceiling((decimal)MaxHealth / 4);
      HealthThreshold = new int[] { MaxHealth - 3, b1, b2, 0 };
    }

    public void printHealthStatus()
    {
      if (Health >= HealthThreshold[0]) Console.WriteLine("\nYou feel in great shape");
      else if (Health >= HealthThreshold[1]) Console.WriteLine("\nYou feel ok");
      else if (Health >= HealthThreshold[2]) Console.WriteLine("\nYou are unwell");
      else if (Health > 1) Console.WriteLine("\nYou are in bad shape");
      else if (Health == 1) Console.WriteLine("\nYou will perish if your condition does not improve");
    }
  }
}