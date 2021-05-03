using System;

namespace cli_game
{
  class ItemAxe : Tool
  {
    public ItemAxe()
    {
      recipe.Add("wood", 5);
      recipe.Add("stone", 2);
    }

    public void examineItem()
    {
      Console.WriteLine("A sharp tool of stone and wood");
    }

    public override void useItem()
    {
      if (World.playerInv.isInInventory("axe"))
      {
        base.processGetItem("wood", 5, "You gather wood using your axe", true);
        randDurability();
      }
      else
      {
        Console.WriteLine("You don't have an axe");
      }
    }

    public override void getItem()
    {
      base.processGetItem("axe", recipe, World.playerInv, "You craft an axe from wood and stone", true);
    }

    public void discardItem()
    {
      base.processDiscardItem("axe", "the");
    }

    protected override void breakItem()
    {
      Console.WriteLine("Your axe broke");
      World.playerInv.removeFromInv("axe", 1);
    }
  }
}