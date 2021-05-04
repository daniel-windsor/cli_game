using System;
using cli_game.Models.Items.Tools;

namespace cli_game.Models.Items.Furnishings
{
    internal class ItemBed : Tool
    {
        public ItemBed()
        {
            Recipe.Add("wood", 20);
            Recipe.Add("fur", 6);
        }

        public void ExamineItem()
        {
            Console.WriteLine("More comfortable than the hard floor");
        }

        public override void GetItem()
        {
            var success = ProcessGetItem("bed", Recipe, World.WorldInv, "You craft a bed from wood and animal fur", true);

            if (success)
            {
                World.WorldInv.AddToInv("bed", 1);
            }
        }

        protected override void BreakItem()
        {
        }
    }
}