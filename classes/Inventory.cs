using System;
using System.Collections.Generic;

namespace cli_game
{
  class Inventory
  {
    private static Dictionary<string, int> inv = new Dictionary<string, int>();

    public int numInInventory(string obj)
    {
      if (inv.ContainsKey(obj)) return inv[obj];
      return 0;
    }

    public bool CheckCraftRequirements(Dictionary<string, int> recipe)
    {
      foreach (KeyValuePair<string, int> kvp in recipe)
      {
        if (!inv.ContainsKey(kvp.Key) || inv[kvp.Key] < kvp.Value) return false;
      }
      return true;
    }

    public void AddToInv(string obj, int num)
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

    public void RemoveFromInv(string obj, int num)
    {
      if (inv[obj] - num < 0)
      {
        Console.WriteLine("You don't have enough in your inventory");
      }
      else
      {
        inv[obj] -= num;
        if (inv[obj] == 0) inv.Remove(obj);
      }
    }

    public void PrintInv()
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