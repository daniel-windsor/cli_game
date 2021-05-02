using System;

namespace cli_game
{
  abstract class Resource : Item
  {
    public Resource()
    {
      verbs.Add(new string[] { "get", "gather", "collect" }, "getItem");
      verbs.Add(new string[] { "use" }, "useItem");
    }
  }
}