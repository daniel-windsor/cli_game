using System;

namespace cli_game
{
  class ItemWood : Resource
  {
    public ItemWood()
    {
      verbs.Add(new string[] { "chop" }, "getItem");
      verbs.Add(new string[] { "burn" }, "useItem");
    }

    public void examineItem()
    {
      Console.WriteLine("Good for crafting. Or burning.");
    }

    public override void useItem()
    {
      int wood = World.playerInv.numInInventory("wood");
      int max = 10 - World.fire.Level;
      int numUsed = r.Next(1, Math.Min(wood, max));

      base.processUseItem("wood", numUsed, "You add some wood to the fire", true);

      World.fire.deltaFire(numUsed);
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

    public void discardItem()
    {
      base.processDiscardItem("wood", "some");
    }
  }
}