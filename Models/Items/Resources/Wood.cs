using System;

namespace cli_game.Models.Items.Resources
{
    internal class ItemWood : Resource
    {
        public ItemWood()
        {
            Verbs.Add(new[] {"chop"}, "getItem");
            Verbs.Add(new[] {"burn"}, "useItem");
        }

        public void ExamineItem()
        {
            Console.WriteLine("Good for crafting. Or burning.");
        }

        public override void UseItem()
        {
            var wood = World.PlayerInv.NumInInventory("wood");
            var max = 10 - World.Fire.Level;
            var numUsed = Random.Next(1, Math.Min(wood, max));

            ProcessUseItem("wood", numUsed, "You add some wood to the fire", true);

            World.Fire.DeltaFire(numUsed);
        }

        public override void GetItem()
        {
            if (World.PlayerInv.IsInInventory("axe"))
            {
                ProcessGetItem("wood", 5, "You gather some wood using your axe", true);
            }
            else
            {
                ProcessGetItem("wood", 2, "You gather some dry branches fallen from nearby trees", true);
            }
        }

        public void DiscardItem()
        {
            ProcessDiscardItem("wood", "some");
        }
    }
}