using System;

namespace cli_game
{
  class ItemLeaves : Resource
  {
    public void examineItem()
    {
      Console.WriteLine("Thick and fibrous");
    }

    public override void getItem()
    {
      if (World.playerInv.isInInventory("knife"))
      {
        base.processGetItem("plant", 3, "You cut a few strands of greenery away", true);
      }
      else
      {
        Console.WriteLine("The plant matter is too thick to gather with your bare hands");
      }
    }

    public void discardItem()
    {
      base.processDiscardItem("leaves", "some");
    }
  }
}