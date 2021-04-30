using System;
using System.Reflection;
using System.Collections.Generic;

namespace cli_game
{
  class World
  {
    public static PlayerCharacter pc = new PlayerCharacter();
    public static Inventory playerInv = new Inventory();
    public static Inventory houseInv = new Inventory();
    public Dictionary<Type, object> items = new Dictionary<Type, object>();

    public World()
    {
      this.Day = 0;
      loop();
    }

    private int Day
    { get; set; }

    public void loop()
    {
      Console.WriteLine("\nAction?");
      var command = Console.ReadLine();
      parseCommand(command);

      loop();
    }

    private void parseCommand(string command)
    {
      // Split into separate words
      string[] split = command.ToLower().Split(' ');

      // Get noun (second word) and change to Title Case
      string first = split[1].Substring(0, 1).ToUpper();
      string noun = $"{first}{split[1].Substring(1)}";

      // Check if object type
      Type type = Type.GetType($"cli_game.Item{noun}");

      if (type != null)
      {
        if (!items.ContainsKey(type))
        {
          //Add new instance to dictionary
          ConstructorInfo constructor = type.GetConstructor(Type.EmptyTypes);
          object obj = constructor.Invoke(new object[] { });

          items.Add(type, obj);
        }

        //Stamina check
        if (pc.Stamina == 0)
        {
          Console.WriteLine("You're too exhausted to do that");
          return;
        }

        // Execute instance action
        MethodInfo method = type.GetMethod("PerformAction");
        method.Invoke(items[type], new object[] { split[0] });

        return;
      }

      switch (noun)
      {
        case "Inventory":
          playerInv.PrintInv();
          break;
        case "Sleep":
          Console.WriteLine("You go to sleep for the night");
          endDay();
          break;
        default:
          Console.WriteLine("I don't understand");
          break;
      }
    }
  
    private void endDay()
    {
      pc.sleep();
      Day += 1;
    }
  }
}