using System;

namespace cli_game
{
  class PlayerCharacter
  {
    public PlayerCharacter()
    {
      MaxHealth = 15;
      Health = 10;
      MaxStamina = 10;
      Stamina = 10;
    }

    public int MaxHealth
    { get; set; }

    public int Health
    { get; set; }

    public void deltaHealth(int delta)
    {
      if (Health + delta > MaxHealth) Health = MaxHealth;
      else Health += delta;
    }

    public int MaxStamina
    { get; set; }


    public int Stamina
    { get; set; }

    public void deltaStamina(int delta)
    {
      if (Stamina + delta > MaxStamina) Stamina = MaxStamina;
      else Stamina += delta;

      if (Stamina == 3 && delta < 0) {
        Console.WriteLine("You're feeling very tired");
      }
      
      if (Stamina == 0) {
        Console.WriteLine("You are exhausted");
      }
    }
  
    public void sleep()
    {
      if (World.houseInv.numInInventory("bed") > 0)
      {
        Console.WriteLine("You awake feeling well rested");
        Stamina = 15;
      }
      else
      {
        Console.WriteLine("It's hard sleeping on the floor.  You didn't sleep well");
        Stamina = 10;
      }
    }
  }
}