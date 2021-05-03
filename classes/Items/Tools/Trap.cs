using System;

namespace cli_game
{
  class ItemTrap : Tool
  {
    public ItemTrap()
    {
      recipe.Add("wood", 8);
      recipe.Add("rope", 2);

      verbs.Add(new string[] { "set", "arm" }, "useItem");
      verbs.Add(new string[] { "empty", "check" }, "checkItem");

      this.Status = null;
      this.CheckedToday = false;
    }

    private string Status
    { get; set; }

    private bool CheckedToday
    { get; set; }

    public void examineItem()
    {
      Console.WriteLine("A trap for small animals");
    }

    public override void getItem()
    {
      base.processGetItem("trap", recipe, World.playerInv, "You fashion a small trap out of sticks and rope", true);
    }

    public override void useItem()
    {
      if (Status == null)
      {
        bool success = base.processUseItem("trap", 1, "You set a trap.  Now to wait", true);

        if (success)
        {
          World.worldInv.addToInv("trap", 1);
          Status = "set";
        }
      }
      else 
      {
        Console.WriteLine("You already have a trap set");
      }
    }

    public void checkItem()
    {
      if (Status == "set")
      {
        Console.WriteLine("You check the trap");

        int rand = r.Next(0, 10);
        Console.WriteLine(rand);

        switch (rand)
        {
          case 0:
            Console.WriteLine("A large animal has taken offence to your contraption.  There is not much of it left");
            World.worldInv.removeFromInv("trap", 1);
            World.playerInv.addToInv("rope", r.Next(0, 1));
            World.playerInv.addToInv("wood", r.Next(1, 2));
            break;
          case 1:
            Console.WriteLine("\nThere is an animal in your trap, but it is not yet dead");
            if (World.playerInv.isInInventory("knife"))
            {
              Console.WriteLine("You dispatch it with your knife, but not before it take a bite out of your arm");
              trapSuccess();
            }
            else
            {
              Console.WriteLine("While trying to remove the struggling animal, it bites your arm and escapes");
            }

            World.pc.deltaHealth(-2);

            break;
          case <= 6:
            Console.WriteLine("\nThe trap is empty");
            break;
          case > 6:
            Console.WriteLine("\nYou find a small animal in the trap");
            trapSuccess();
            break;
        }

        World.pc.deltaStamina(-1);
      }
      else 
      {
        Console.WriteLine("You haven't set a trap");
      }
    }

    public void trapSuccess()
    {
      World.worldInv.removeFromInv("trap", 1);
      World.playerInv.addToInv("trap", 1);
      World.playerInv.addToInv("food", 4);
      World.playerInv.addToInv("fur", 2);
      Status = null;
    }

    public void discardItem()
    {
      base.processDiscardItem("trap", "the");
    }

    protected override void breakItem()
    {
      Console.WriteLine("Your trap broke");
      World.playerInv.removeFromInv("trap", 1);
    }


  }
}