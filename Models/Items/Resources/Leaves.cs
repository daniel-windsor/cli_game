using System;

namespace cli_game.Models.Items.Resources
{
    internal class ItemLeaves : Resource
    {
        public void ExamineItem()
        {
            Console.WriteLine("Thick and fibrous");
        }

        public override void GetItem()
        {
            if (World.PlayerInv.IsInInventory("knife"))
            {
                ProcessGetItem("plant", 3, "You cut a few strands of greenery away", true);
            }
            else
            {
                Console.WriteLine("The plant matter is too thick to gather with your bare hands");
            }
        }

        public void DiscardItem()
        {
            ProcessDiscardItem("leaves", "some");
        }
    }
}