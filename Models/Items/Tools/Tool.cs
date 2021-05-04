namespace cli_game.Models.Items.Tools
{
    internal abstract class Tool : Item
    {
        protected Tool()
        {
            Verbs.Add(new[] {"craft", "make", "fashion"}, "getItem");
            Verbs.Add(new[] {"use"}, "useItem");

            Durability = 10;
        }

        private int Durability { get; set; }

        private void DeltaDurability(int delta)
        {
            switch (Durability + delta)
            {
                case > 10:
                    Durability = 10;
                    break;
                case <= 0:
                    BreakItem();
                    break;
                default:
                    Durability += delta;
                    break;
            }
        }

        protected void RandDurability()
        {
            var rand = Random.Next(0, 10);
            if (rand <= 3) DeltaDurability(-1);
        }

        protected abstract void BreakItem();
    }
}