using System;

namespace cli_game
{
  class ItemKnife : Tool
  {
    public ItemKnife()
    {
      recipe.Add("stone", 5);
    }

    public void examineItem()
    {
      Console.WriteLine("A stone knife, for cutting");
    }

    public override void getItem()
    {
      base.processGetItem("knife", recipe, World.playerInv, "You grind the stone into a rough knife", true);
    }

    public void discardItem()
    {
      base.processDiscardItem("knife", "the");
    }

    protected override void breakItem()
    {
      Console.WriteLine("Your knife broke");
      World.playerInv.removeFromInv("knife", 1);
    }
  }
}