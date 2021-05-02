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
    public static Fire fire = new Fire();
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
      //Get user input
      Console.WriteLine("\nAction?");
      var command = Console.ReadLine();
      bool success = parseCommand(command);

      if (success)
      {
        //Randomly decrease fire level based on day count
        fire.randFire(Day);

        //Randomly decrease health based on fire level
        if (fire.Level < fire.FireThreshold[1]) pc.randHealth(-1);
        else if (fire.Level <= fire.FireThreshold[2]) pc.randHealth(-2);

        //Death check
        if (pc.Health == 0)
        {
          die();
          return;
        }
      }

      loop();
    }

    private bool parseCommand(string command)
    {
      // Split into separate words
      string[] split = command.ToLower().Split(' ');

      // Check for util commands
      if (split.Length == 1)
      {
        switch (split[0])
        {
          case "inventory":
          case "inv":
            playerInv.printInv();
            break;
          case "sleep":
            Console.WriteLine("You go to sleep for the night");
            endDay();
            break;
          case "fire":
            fire.printFireStatus();
            break;
          case "health":
            pc.printHealthStatus();
          break;
          default:
            Console.WriteLine("I don't understand");
            break;
        }

        return false;
      }

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
          return false;
        }

        //Execute instance action
        MethodInfo method = type.GetMethod("PerformAction");
        method.Invoke(items[type], new object[] { split[0] });

        return true;
      }

      Console.WriteLine("I don't understand");
      return false;
    }

    private void endDay()
    {
      // End of day character maintenance
      pc.sleep();

      // Deal damage if fire is out
      if (fire.Level == 0) 
      {
        int delta = (int)Math.Ceiling((decimal)pc.Health / 2);
        pc.deltaHealth(-delta);
      }

      fire.deltaFire(-3);

      Day += 1;
    }

    public void die()
    {
      Console.WriteLine("You have died");
      Console.WriteLine($"You survived for {Day} day{(Day > 1 ? "s" : "")}");
    }
  }
}