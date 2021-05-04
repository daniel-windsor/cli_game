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

      /**
      This dictionary is used for matching different verbs with particular functions.
      It's not a one size fits all. You "use" an axe, but you "eat" some food.
      Both of these words call the same "useItem" function.
      The ones set here are default for all items
      **/
    protected Dictionary<string[], string> verbs = new Dictionary<string[], string>();

    // Crafting recipes for each item.  The item is the key, the value is number required
    protected Dictionary<string, int> recipe = new Dictionary<string, int>();


    // Takes the verb from user input, checks each key array for its presence then executes the associated function using reflection
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

    /**
    useItem is overrided in the children items. The override method then calls this method passing it item specific parameters.
    Ideally, useItem would be overloaded, but because I've used reflection to call the method, there isn't an easy way to pass parameters directly
    **/
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