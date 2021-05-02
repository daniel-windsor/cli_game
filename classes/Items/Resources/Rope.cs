using System;

namespace cli_game
{
  class ItemRope : Resource
  {
    public ItemRope()
    {
      verbs.Add(new string[] { "craft", "make", "fashion" }, "getItem");

      recipe.Add("leaves", 5);
    }

    public void examineItem()
    {
      Console.WriteLine("A strong length of woven leaves");
    }

    public override void getItem()
    {
      base.processGetItem("rope", recipe, "You weave plant matter into a strong rope", true);
    }

    public void discardItem()
    {
      base.processDiscardItem("rope", "the");
    }
  }
}