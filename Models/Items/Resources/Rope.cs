using System;

namespace cli_game.Models.Items.Resources
{
    internal class ItemRope : Resource
    {
        public ItemRope()
        {
            Verbs.Add(new[] {"craft", "make", "fashion"}, "getItem");

            Recipe.Add("leaves", 5);
        }

        public void ExamineItem()
        {
            Console.WriteLine("A strong length of woven leaves");
        }

        public override void GetItem()
        {
            ProcessGetItem("rope", Recipe, World.PlayerInv, "You weave plant matter into a strong rope", true);
        }

        public void DiscardItem()
        {
            ProcessDiscardItem("rope", "the");
        }
    }
}