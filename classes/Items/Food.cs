using System;

namespace cli_game
{
  class ItemFood : Item
  {
    public ItemFood()
    {
      verbs.Add(new string[] { "eat", "consume" }, "useItem");
      verbs.Add(new string[] { "get", "gather", "collect" }, "craftItem");
    }

    public override void examineItem()
    {
      Console.WriteLine("Looks tasty");
    }

    public override void useItem()
    {
      if (World.playerInv.numInInventory("food") > 0)
      {
        Console.WriteLine("You eat some food");
        World.pc.deltaHealth(1);
        World.playerInv.RemoveFromInv("food", 1);
      }
      else
      {
        Console.WriteLine("You don't have any food");
      }
    }

    public override void craftItem()
    {
      Console.WriteLine("You gather some food");
      World.pc.deltaStamina(-1);
      World.playerInv.AddToInv("food", 1);
    }

    public override void discardItem()
    {
      if (World.playerInv.numInInventory("food") > 0)
      {
        Console.WriteLine("You drop some food");
        World.playerInv.RemoveFromInv("food", 1);
      }
    }
  }
}