using System;

namespace cli_game.Models.Items.Resources
{
    internal class ItemStone : Resource
    {
        public ItemStone()
        {
            Verbs.Add(new[] {"mine"}, "getItem");
        }

        public void ExamineItem()
        {
            Console.WriteLine("A strong crafting material");
        }

        public override void GetItem()
        {
            ProcessGetItem("stone", Random.Next(1, 2), "You gather some stone", true);
        }

        public void DiscardItem()
        {
            ProcessDiscardItem("stone", "some");
        }
    }
}