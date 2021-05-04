using System;

namespace cli_game.Models.Items.Tools
{
    internal class ItemKnife : Tool
    {
        public ItemKnife()
        {
            Recipe.Add("stone", 5);
        }

        public void ExamineItem()
        {
            Console.WriteLine("A stone knife, for cutting");
        }

        public override void GetItem()
        {
            ProcessGetItem("knife", Recipe, World.PlayerInv, "You grind the stone into a rough knife", true);
        }

        public void DiscardItem()
        {
            ProcessDiscardItem("knife", "the");
        }

        protected override void BreakItem()
        {
            Console.WriteLine("Your knife broke");
            World.PlayerInv.RemoveFromInv("knife", 1);
        }
    }
}