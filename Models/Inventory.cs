using System;
using System.Collections.Generic;

namespace cli_game.Models
{
    internal class Inventory
    {
        private readonly Dictionary<string, int> _inventory = new()
        {
            {"wood", 50},
            {"stone", 50},
            {"food", 50},
            {"leaves", 50},
            {"knife", 1},
            {"axe", 1}
        };

        public int NumInInventory(string obj)
        {
            return _inventory.ContainsKey(obj) ? _inventory[obj] : 0;
        }

        public bool IsInInventory(string obj)
        {
            return _inventory.ContainsKey(obj);
        }

        public bool CheckCraftRequirements(Dictionary<string, int> recipe)
        {
            foreach (var (key, value) in recipe)
                if (!_inventory.ContainsKey(key) || _inventory[key] < value)
                {
                    return false;
                }

            return true;
        }

        public void AddToInv(string obj, int num)
        {
            if (_inventory.ContainsKey(obj))
                _inventory[obj] += num;
            else
                _inventory.Add(obj, num);
        }

        public bool RemoveFromInv(string obj, int num)
        {
            if (!_inventory.ContainsKey(obj))
            {
                Console.WriteLine("You don't have that in your inventory");
                return false;
            }

            // if (inv[obj] - num < 0)
            // {
            //   Console.WriteLine("You don't have enough in your inventory");
            //   return false;
            // }

            _inventory[obj] -= num;
            if (_inventory[obj] == 0) _inventory.Remove(obj);
            return true;
        }

        public void PrintInv()
        {
            if (_inventory.Count == 0)
                Console.WriteLine("Your inventory is empty");
            else
                foreach (var (key, value) in _inventory)
                    Console.WriteLine($"{key}: {value}");
        }
    }
}