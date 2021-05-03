using System;
using System.Collections.Generic;

namespace cli_game
{
  class Inventory
  {
    private Dictionary<string, int> inv = new Dictionary<string, int>()
    {
      {"wood", 50 },
      {"stone", 50 },
      {"food", 50 },
      {"leaves", 50 },
      {"knife", 1},
      {"axe", 1},
    }
    ;

    public int numInInventory(string obj)
    {
      if (inv.ContainsKey(obj)) return inv[obj];
      return 0;
    }

    public bool isInInventory(string obj)
    {
      return (inv.ContainsKey(obj));
    }

    public bool checkCraftRequirements(Dictionary<string, int> recipe)
    {
      foreach (KeyValuePair<string, int> kvp in recipe)
      {
        if (!inv.ContainsKey(kvp.Key) || inv[kvp.Key] < kvp.Value) return false;
      }
      return true;
    }

    public void addToInv(string obj, int num)
    {
      if (inv.ContainsKey(obj))
      {
        inv[obj] += num;
      }
      else
      {
        inv.Add(obj, num);
      }
    }

    public bool removeFromInv(string obj, int num)
    {
      if (!inv.ContainsKey(obj))
      {
        Console.WriteLine("You don't have that in your inventory"); return false;
      }

      // if (inv[obj] - num < 0)
      // {
      //   Console.WriteLine("You don't have enough in your inventory");
      //   return false;
      // }

      inv[obj] -= num;
      if (inv[obj] == 0) inv.Remove(obj);
      return true;
    }

    public void printInv()
    {
      if (inv.Count == 0)
      {
        Console.WriteLine("Your inventory is empty");
      }
      else
      {
        foreach (KeyValuePair<String, int> kvp in inv)
        {
          Console.WriteLine($"{kvp.Key}: {kvp.Value}");
        }
      }
    }
  }
}