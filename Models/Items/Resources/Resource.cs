namespace cli_game.Models.Items.Resources
{
    internal abstract class Resource : Item
    {
        protected Resource()
        {
            Verbs.Add(new[] {"get", "gather", "collect"}, "getItem");
            Verbs.Add(new[] {"use"}, "useItem");
        }
    }
}