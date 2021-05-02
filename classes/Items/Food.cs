using System;

namespace cli_game
{
  class ItemFood : Item
  {
    public ItemFood()
    {
      verbs.Add(new string[] { "eat", "consume" }, "useItem");
      verbs.Add(new string[] { "get", "gather", "collect" }, "getItem");
    }

    public override void examineItem()
    {
      Console.WriteLine("Looks tasty");
    }

    public override void useItem()
    {
      base.processUseItem("food", 1, "You eat some food", false);
      World.pc.deltaHealth(1);
    }

    public override void getItem()
    {
      base.processGetItem("food", 1, "You gather some food", true);
    }

    public override void discardItem()
    {
      base.processDiscardItem("food", "some");
    }
  }
}