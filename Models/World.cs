using System;
using System.Collections.Generic;

namespace cli_game.Models
{
    internal class World
    {
        public static readonly PlayerCharacter Pc = new();
        public static readonly Inventory PlayerInv = new();
        public static readonly Inventory WorldInv = new();
        public static readonly Fire Fire = new();

        // This dictionary stores the item object instances created using reflection
        private readonly Dictionary<Type, object> _items = new();

        private readonly Random _random = new();

        private int Day { get; set; }
        
        public World()
        {
            Day = 0;
            Loop();
        }
        
        private void Loop()
        {
            //Get user input
            Console.WriteLine("\nAction?");
            var command = Console.ReadLine();
            var success = ParseCommand(command);

            if (success)
            {
                //Randomly decrease fire level based on day count
                Fire.RandFire(Day);

                //Randomly decrease health based on fire level
                if (Fire.Level < Fire.FireThreshold[1])
                {
                    Pc.RandHealth(-1);
                }
                else if (Fire.Level <= Fire.FireThreshold[2])
                {
                    Pc.RandHealth(-2);
                }

                //Death check
                if (Pc.Health == 0)
                {
                    Die();
                    return;
                }
            }

            Loop();
        }

        private bool ParseCommand(string command)
        {
            // Split into separate words
            var split = command.ToLower().Split(' ');

            // Check for util commands
            if (split.Length == 1)
            {
                switch (split[0])
                {
                    case "inventory":
                    case "inv":
                        PlayerInv.PrintInv();
                        break;
                    case "sleep":
                        Console.WriteLine("You go to sleep for the night");
                        EndDay();
                        break;
                    case "fire":
                        Fire.PrintFireStatus();
                        break;
                    case "health":
                        Pc.PrintHealthStatus();
                        break;
                    default:
                        Console.WriteLine("I don't understand");
                        break;
                }

                return false;
            }

            // Get noun (second word) and change to Title Case
            var first = split[1].Substring(0, 1).ToUpper();
            var noun = $"{first}{split[1].Substring(1)}";

            // Check if object type
            var type = Type.GetType($"cli_game.Item{noun}");

            if (type != null)
            {
                if (!_items.ContainsKey(type))
                {
                    //Add new instance to dictionary
                    var constructor = type.GetConstructor(Type.EmptyTypes);
                    var obj = constructor?.Invoke(Array.Empty<object>());

                    _items.Add(type, obj);
                }

                //Stamina check
                if (Pc.Stamina == 0)
                {
                    Console.WriteLine("You're too exhausted to do that");
                    return false;
                }

                //Execute instance action
                var method = type.GetMethod("PerformAction");
                method?.Invoke(_items[type], new object[] {split[0]});

                return true;
            }

            Console.WriteLine("I don't understand");
            return false;
        }

        private void EndDay()
        {
            // End of day character maintenance
            Pc.Sleep();

            // Deal damage if fire is out
            if (Fire.Level == 0)
            {
                var delta = (int) Math.Ceiling((decimal) Pc.Health / 2);
                Pc.DeltaHealth(-delta);
            }

            Fire.DeltaFire(-_random.Next(0, 3));

            Day += 1;
        }

        private void Die()
        {
            Console.WriteLine("You have died");
            Console.WriteLine($"You survived for {Day} day{(Day > 1 ? "s" : "")}");
        }
    }
}