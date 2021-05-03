using System;

namespace cli_game
{
  class ItemBed : Tool
  {
    public ItemBed()
    {
      recipe.Add("wood", 20);
      recipe.Add("fur", 6);
    }
    public void examineItem()
    {
      Console.WriteLine("More comfortable than the hard floor");
    }

    public override void getItem()
    {
      bool success = base.processGetItem("bed", recipe, World.worldInv, "You craft a bed from wood and animal fur", true);

      if (success) World.worldInv.addToInv("bed", 1);

    }

    protected override void breakItem(){}
  }
}