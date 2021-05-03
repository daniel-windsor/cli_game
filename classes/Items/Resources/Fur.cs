using System;

namespace cli_game
{
  class ItemFur : Resource
  {
    public void examineItem()
    {
      Console.WriteLine("Soft and warm");
    }

    public void discardItem()
    {
      base.processDiscardItem("fur", "some");
    }
  }
}