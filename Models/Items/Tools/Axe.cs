using System;

namespace cli_game.Models.Items.Tools
{
    internal class ItemAxe : Tool
    {
        public ItemAxe()
        {
            Recipe.Add("wood", 5);
            Recipe.Add("stone", 2);
        }

        public void ExamineItem()
        {
            Console.WriteLine("A sharp tool of stone and wood");
        }

        public override void UseItem()
        {
            if (World.PlayerInv.IsInInventory("axe"))
            {
                ProcessGetItem("wood", 5, "You gather wood using your axe", true);
                RandDurability();
            }
            else
            {
                Console.WriteLine("You don't have an axe");
            }
        }

        public override void GetItem()
        {
            ProcessGetItem("axe", Recipe, World.PlayerInv, "You craft an axe from wood and stone", true);
        }

        public void DiscardItem()
        {
            ProcessDiscardItem("axe", "the");
        }

        protected override void BreakItem()
        {
            Console.WriteLine("Your axe broke");
            World.PlayerInv.RemoveFromInv("axe", 1);
        }
    }
}