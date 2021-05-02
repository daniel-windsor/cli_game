using System;

namespace cli_game
{
  abstract class Tool : Item
  {
    public Tool()
    {
      verbs.Add(new string[] { "craft", "make", "fashion" }, "getItem");
      verbs.Add(new string[] { "use" }, "useItem");

      this.Durability = 10;
    }

    private int Durability
    { get; set; }

    public void deltaDurability(int delta)
    {
      if (Durability + delta > 10) Durability = 10;
      else if (Durability + delta <= 0) breakItem();
      else Durability += delta;
    }

    public void randDurability()
    {
      int rand = r.Next(0, 10);
      if (rand <= 3) deltaDurability(-1);
    }

    abstract protected void breakItem();
  }
}