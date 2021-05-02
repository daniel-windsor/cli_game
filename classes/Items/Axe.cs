using System;

namespace cli_game
{
  class ItemAxe : Item
  {
    public ItemAxe()
    {
      verbs.Add(new string[] { "craft", "make", "fashion" }, "getItem");
      verbs.Add(new string[] { "use" }, "useItem");

      recipe.Add("wood", 5);
      recipe.Add("stone", 2);
    }

    public override void examineItem()
    {
      Console.WriteLine("A sharp tool of stone and wood");
    }

    public override void useItem()
    {
      if (World.playerInv.isInInventory("axe"))
      {
        base.processGetItem("wood", 5, "You gather some wood using your axe", true);
      }
      else {
        Console.WriteLine("You don't have an axe");
      }
    }

    public override void getItem()
    {
      base.processGetItem("axe", recipe, "You craft an axe from wood and stone", true);
    }

    public override void discardItem()
    {
      base.processDiscardItem("axe", "the");
    }
  }
}