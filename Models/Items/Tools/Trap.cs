using System;

namespace cli_game.Models.Items.Tools
{
    internal class ItemTrap : Tool
    {
        public ItemTrap()
        {
            Recipe.Add("wood", 8);
            Recipe.Add("rope", 2);

            Verbs.Add(new[] {"set", "arm"}, "useItem");
            Verbs.Add(new[] {"empty", "check"}, "checkItem");

            Status = null;
            CheckedToday = false;
        }

        private string Status { get; set; }

        private bool CheckedToday { get; }

        public void ExamineItem()
        {
            Console.WriteLine("A trap for small animals");
        }

        public override void GetItem()
        {
            ProcessGetItem("trap", Recipe, World.PlayerInv, "You fashion a small trap out of sticks and rope", true);
        }

        public override void UseItem()
        {
            if (Status == null)
            {
                var success = ProcessUseItem("trap", 1, "You set a trap.  Now to wait", true);

                if (success)
                {
                    World.WorldInv.AddToInv("trap", 1);
                    Status = "set";
                }
            }
            else
            {
                Console.WriteLine("You already have a trap set");
            }
        }

        public void CheckItem()
        {
            if (Status == "set")
            {
                Console.WriteLine("You check the trap");

                var rand = Random.Next(0, 10);
                Console.WriteLine(rand);

                switch (rand)
                {
                    case 0:
                        Console.WriteLine("A large animal has taken offence to your contraption.  There is not much of it left");
                        World.WorldInv.RemoveFromInv("trap", 1);
                        World.PlayerInv.AddToInv("rope", Random.Next(0, 1));
                        World.PlayerInv.AddToInv("wood", Random.Next(1, 2));
                        break;
                    case 1:
                        Console.WriteLine("\nThere is an animal in your trap, but it is not yet dead");
                        if (World.PlayerInv.IsInInventory("knife"))
                        {
                            Console.WriteLine("You dispatch it with your knife, but not before it take a bite out of your arm");
                            TrapSuccess();
                        }
                        else
                        {
                            Console.WriteLine("While trying to remove the struggling animal, it bites your arm and escapes");
                        }

                        World.Pc.DeltaHealth(-2);

                        break;
                    case <= 6:
                        Console.WriteLine("\nThe trap is empty");
                        break;
                    case > 6:
                        Console.WriteLine("\nYou find a small animal in the trap");
                        TrapSuccess();
                        break;
                }

                World.Pc.DeltaStamina(-1);
            }
            else
            {
                Console.WriteLine("You haven't set a trap");
            }
        }

        private void TrapSuccess()
        {
            World.WorldInv.RemoveFromInv("trap", 1);
            World.PlayerInv.AddToInv("trap", 1);
            World.PlayerInv.AddToInv("food", 4);
            World.PlayerInv.AddToInv("fur", 2);
            Status = null;
        }

        public void DiscardItem()
        {
            ProcessDiscardItem("trap", "the");
        }

        protected override void BreakItem()
        {
            Console.WriteLine("Your trap broke");
            World.PlayerInv.RemoveFromInv("trap", 1);
        }
    }
}