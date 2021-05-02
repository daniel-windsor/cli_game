using System;

namespace cli_game
{
  class ItemStone : Item
  {
    public ItemStone()
    {
      verbs.Add(new string[] { "gather", "collect", "mine", "get" }, "getItem");
    }

    public override void examineItem()
    {
      Console.WriteLine("A strong crafting material");
    }

    public override void getItem()
    {
      base.processGetItem("stone", 1, "You gather some stone", true);
    }

    public override void discardItem()
    {
      base.processDiscardItem("stone", "some");
    }
  }
}