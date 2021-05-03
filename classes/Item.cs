using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;

namespace cli_game
{
  abstract class Item
  {

    protected Random r = new Random();

    public Item()
    {
      verbs.Add(new string[] { "examine" }, "examineItem");
      verbs.Add(new string[] { "discard", "drop" }, "discardItem");
    }

    protected Dictionary<string[], string> verbs = new Dictionary<string[], string>();

    protected Dictionary<string, int> recipe = new Dictionary<string, int>();

    public void PerformAction(string verb)
    {
      foreach (KeyValuePair<string[], string> kvm in verbs)
      {
        if (kvm.Key.Any(s => s.Contains(verb)))
        {
          Type type = this.GetType();
          MethodInfo method = type.GetMethod(kvm.Value);

          method.Invoke(this, null);
          return;
        }
      }

      Console.WriteLine("I don't understand");
    }

    // abstract public void examineItem();

    public virtual void useItem()
    {
      Console.WriteLine("You can't use that");
    }

    public bool processUseItem(string noun, int num, string output, bool stamina)
    {
      bool success = World.playerInv.removeFromInv(noun, num);

      if (success)
      {
        Console.WriteLine(output);
        if (stamina) World.pc.deltaStamina(-1);
      }

      return success;
    }

    public virtual void getItem()
    {
      Console.WriteLine("You can't get that");
    }

    public void processGetItem(string noun, int num, string output, bool stamina)
    {
      Console.WriteLine(output);
      World.playerInv.addToInv(noun, num);
      if (stamina) World.pc.deltaStamina(-1);
    }

    public bool processGetItem(string noun, Dictionary<string, int> recipe, Inventory inv, string output, bool stamina)
    {
      if (World.playerInv.checkCraftRequirements(recipe))
      {
        Console.WriteLine(output);
        inv.addToInv(noun, 1);
        if (stamina) World.pc.deltaStamina(-1);

        foreach (KeyValuePair <string, int> kvm in recipe)
        {
          World.playerInv.removeFromInv(kvm.Key, kvm.Value);
        }

        return true;
      }
      else
      {
        Console.WriteLine("You don't have the materials to craft this");
        return false;
      }
    }

    // abstract public void discardItem();

    public void processDiscardItem(string noun, string article)
    {
      bool success = World.playerInv.removeFromInv(noun, 1);

      if (success) Console.WriteLine($"You drop {article} {noun}");
    }

  }
}