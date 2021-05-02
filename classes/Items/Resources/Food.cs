using System;

namespace cli_game
{
  class ItemFood : Resource
  {
    public ItemFood()
    {
      verbs.Add(new string[] { "eat", "consume" }, "useItem");
    }

    public void examineItem()
    {
      Console.WriteLine("Looks tasty");
    }

    public override void useItem()
    {
      int food = World.playerInv.numInInventory("food");
      int max = World.pc.MaxHealth - World.pc.Health;
      int num = r.Next(1, Math.Min(food, max));

      bool success = base.processUseItem("food", num, "You eat some food", false);

      if (success) World.pc.deltaHealth(num);
    }

    public override void getItem()
    {
      base.processGetItem("food", 1, "You gather some food", true);
    }

    public void discardItem()
    {
      base.processDiscardItem("food", "some");
    }
  }
}