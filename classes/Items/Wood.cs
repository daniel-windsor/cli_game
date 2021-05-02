using System;

namespace cli_game
{
  class ItemWood : Item
  {
    public ItemWood()
    {
      verbs.Add(new string[] { "gather", "collect", "chop", "get" }, "getItem");
      verbs.Add(new string[] { "burn", "use" }, "useItem");
    }

    public override void examineItem()
    {
      Console.WriteLine("Good for crafting. Or burning.");
    }

    public override void useItem()
    {
      base.processUseItem("wood", 1, "You add some wood to the fire", true);
      World.fire.deltaFire(1);
    }

    public override void getItem()
    {
      if (World.playerInv.isInInventory("axe"))
      {
        base.processGetItem("wood", 5, "You gather some wood using your axe", true);
      }
      else
      {
        base.processGetItem("wood", 2, "You gather some dry branches fallen from nearby trees", true);
      }
    }

    public override void discardItem()
    {
      base.processDiscardItem("wood", "some");
    }
  }
}