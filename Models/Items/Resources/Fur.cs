using System;

namespace cli_game.Models.Items.Resources
{
    internal class ItemFur : Resource
    {
        public void ExamineItem()
        {
            Console.WriteLine("Soft and warm");
        }

        public void DiscardItem()
        {
            ProcessDiscardItem("fur", "some");
        }
    }
}