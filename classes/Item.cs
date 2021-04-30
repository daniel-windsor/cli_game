using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;

namespace cli_game
{
  abstract class Item
  {
    public Item()
    {
      verbs.Add(new string[] { "examine" }, "examineItem");
      verbs.Add(new string[] { "discard", "drop" }, "discardItem");
    }

    protected Dictionary<string[], string> verbs = new Dictionary<string[], string>();

    public void PerformAction(string verb)
    {
      foreach (KeyValuePair<string[], string> kvm in verbs)
      {
        if (kvm.Key.Any(s => s.Contains(verb)))
        {
          Type type = this.GetType();
          MethodInfo method = type.GetMethod(kvm.Value);

          method.Invoke(this, null);
          return;
        }
      }

      Console.WriteLine("I don't understand");
    }

    abstract public void examineItem();
    public virtual void useItem()
    {
      Console.WriteLine("You can't use that");
    }
    public virtual void craftItem()
    {
      Console.WriteLine("You can't make that");
    }
    abstract public void discardItem();

  }
}