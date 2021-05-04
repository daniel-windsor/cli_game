using System;
using System.Collections.Generic;
using System.Linq;

namespace cli_game.Models
{
    internal abstract class Item
    {
        protected readonly Random Random = new();

        // Crafting recipes for each item.  The item is the key, the value is number required
        protected readonly Dictionary<string, int> Recipe = new();

        /**
         * This dictionary is used for matching different verbs with particular functions.
         * It's not a one size fits all. You "use" an axe, but you "eat" some food.
         * Both of these words call the same "useItem" function.
         * The ones set here are default for all items
         * *
         */
        protected readonly Dictionary<string[], string> Verbs = new();

        protected Item()
        {
            Verbs.Add(new[] {"examine"}, "examineItem");
            Verbs.Add(new[] {"discard", "drop"}, "discardItem");
        }


        // Takes the verb from user input, checks each key array for its presence then executes the associated function using reflection
        public void PerformAction(string verb)
        {
            foreach (var (key, value) in Verbs)
            {
                if (key.Any(s => s.Contains(verb)))
                {
                    var type = GetType();
                    var method = type.GetMethod(value);

                    method?.Invoke(this, null);
                    return;
                }
            }

            Console.WriteLine("I don't understand");
        }

        // abstract public void examineItem();

        public virtual void UseItem()
        {
            Console.WriteLine("You can't use that");
        }

        /**
         * useItem is override in the children items. The override method then calls this method passing it item specific parameters.
         * Ideally, useItem would be overloaded, but because I've used reflection to call the method, there isn't an easy way to pass parameters directly
         * *
         */
        protected static bool ProcessUseItem(string noun, int num, string output, bool stamina)
        {
            var success = World.PlayerInv.RemoveFromInv(noun, num);

            if (success)
            {
                Console.WriteLine(output);
                if (stamina)
                {
                    World.Pc.DeltaStamina(-1);
                }
            }

            return success;
        }

        public virtual void GetItem()
        {
            Console.WriteLine("You can't get that");
        }

        protected static void ProcessGetItem(string noun, int num, string output, bool stamina)
        {
            Console.WriteLine(output);
            World.PlayerInv.AddToInv(noun, num);
            if (stamina)
            {
                World.Pc.DeltaStamina(-1);
            }
        }

        protected static bool ProcessGetItem(string noun, Dictionary<string, int> recipe, Inventory inv, string output, bool stamina)
        {
            if (World.PlayerInv.CheckCraftRequirements(recipe))
            {
                Console.WriteLine(output);
                inv.AddToInv(noun, 1);
                if (stamina)
                {
                    World.Pc.DeltaStamina(-1);
                }

                foreach (var kvm in recipe)
                {
                    World.PlayerInv.RemoveFromInv(kvm.Key, kvm.Value);
                }

                return true;
            }

            Console.WriteLine("You don't have the materials to craft this");
            return false;
        }

        // abstract public void discardItem();

        protected static void ProcessDiscardItem(string noun, string article)
        {
            var success = World.PlayerInv.RemoveFromInv(noun, 1);

            if (success)
            {
                Console.WriteLine($"You drop {article} {noun}");
            }
        }
    }
}