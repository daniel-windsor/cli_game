using System;

namespace cli_game.Models
{
    internal class PlayerCharacter
    {
        private readonly Random _random = new();
        private int _maxHealth;

        public PlayerCharacter()
        {
            MaxHealth = 15;
            Health = 10;
            MaxStamina = 15;
            Stamina = 10;

            CreateHealthThreshold();
        }

        public int MaxHealth
        {
            get => _maxHealth;
            set
            {
                _maxHealth = value;
                CreateHealthThreshold();
            }
        }

        public int Health { get; private set; }

        private int[] HealthThreshold { get; set; }

        private int MaxStamina { get; set; }

        public int Stamina { get; private set; }

        public void DeltaHealth(int delta)
        {
            var prevHealth = Health;

            if (Health + delta > MaxHealth)
            {
                Health = MaxHealth;
            }
            else if (Health + delta < 0)
            {
                Health = 0;
            }
            else
            {
                Health += delta;
                if (delta > 0) Console.WriteLine("\nYou feel a little better");
            }

            foreach (var i in HealthThreshold)
                if (prevHealth >= i && Health < i || prevHealth < i && Health >= i)
                {
                    PrintHealthStatus();
                    return;
                }
        }

        public void RandHealth(int num)
        {
            var rand = _random.Next(0, 10);
            if (rand <= 2) DeltaHealth(num);
        }

        public void DeltaStamina(int delta)
        {
            if (Stamina + delta > MaxStamina) Stamina = MaxStamina;
            else if (Stamina + delta < 0) Stamina = 0;
            else Stamina += delta;

            switch (Stamina)
            {
                case 3 when delta < 0:
                    Console.WriteLine("\nYou are feeling very tired");
                    break;
                case 0:
                    Console.WriteLine("\nYou are exhausted");
                    break;
            }
        }

        public void Sleep()
        {
            if (World.WorldInv.IsInInventory("bed"))
            {
                Console.WriteLine("\nYou awake feeling well rested");
                Stamina = MaxStamina;
            }
            else
            {
                Console.WriteLine("\nIt is hard sleeping on the floor.  You did not sleep well");
                Stamina = 10;
            }
        }

        // Adjusts the 'cutoff' values for each health threshold based on current max health value
        private void CreateHealthThreshold()
        {
            var b1 = (int) Math.Ceiling((decimal) MaxHealth / 2);
            var b2 = (int) Math.Ceiling((decimal) MaxHealth / 4);
            HealthThreshold = new[] {MaxHealth - 3, b1, b2, 0};
        }

        public void PrintHealthStatus()
        {
            if (Health >= HealthThreshold[0]) Console.WriteLine("\nYou feel in great shape");
            else if (Health >= HealthThreshold[1]) Console.WriteLine("\nYou feel ok");
            else if (Health >= HealthThreshold[2]) Console.WriteLine("\nYou are unwell");
            else if (Health > 1) Console.WriteLine("\nYou are in bad shape");
            else if (Health == 1) Console.WriteLine("\nYou will perish if your condition does not improve");
        }
    }
}