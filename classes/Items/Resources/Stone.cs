using System;

namespace cli_game
{
  class ItemStone : Resource
  {
    public ItemStone()
    {
      verbs.Add(new string[] { "mine" }, "getItem");
    }

    public void examineItem()
    {
      Console.WriteLine("A strong crafting material");
    }

    public override void getItem()
    {
      base.processGetItem("stone", r.Next(1, 2), "You gather some stone", true);
    }

    public void discardItem()
    {
      base.processDiscardItem("stone", "some");
    }
  }
}