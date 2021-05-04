using System;

namespace cli_game.Models.Items.Resources
{
    internal class ItemFood : Resource
    {
        public ItemFood()
        {
            Verbs.Add(new[] {"eat", "consume"}, "useItem");
        }

        public void ExamineItem()
        {
            Console.WriteLine("Looks tasty");
        }

        public override void UseItem()
        {
            var food = World.PlayerInv.NumInInventory("food");
            var max = World.Pc.MaxHealth - World.Pc.Health;
            var num = Random.Next(1, Math.Min(food, max));

            var success = ProcessUseItem("food", num, "You eat some food", false);

            if (success)
            {
                World.Pc.DeltaHealth(num);
            }
        }

        public override void GetItem()
        {
            ProcessGetItem("food", 1, "You gather some food", true);
        }

        public void DiscardItem()
        {
            ProcessDiscardItem("food", "some");
        }
    }
}