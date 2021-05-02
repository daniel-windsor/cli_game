using System;

namespace cli_game
{
  class ItemTrap : Tool
  {
    public ItemTrap()
    {
      recipe.Add("wood", 8);
      recipe.Add("rope", 2);

      verbs.Add(new string[] { "empty", "check" }, "useItem");
      verbs.Add(new string[] { "set", "arm" }, "setItem");
    }

    public void examineItem()
    {
      Console.WriteLine("A trap for small animals");
    }

    public override void getItem()
    {
      base.processGetItem("trap", recipe, "You fashion a small trap out of sticks and rope", true);
    }

    public void setItem()
    {
      Console.WriteLine("You set the trap. Now to wait");
    }

    public override void useItem()
    {
      Console.WriteLine("You check the trap");
    }

    public void discardItem()
    {
      base.processDiscardItem("trap", "the");
    }

    protected override void breakItem()
    {
      Console.WriteLine("Your trap broke");
      World.playerInv.removeFromInv("trap", 1);
    }


  }
}