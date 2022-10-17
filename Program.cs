using Tables;

namespace Program
{
  public class Program
  {
    public static int Main(string[] args)
    {
      // map the arguments correctl
      string action = args[0];
      string tableName = args[1];
      bool important = false;
      List<string> filterList = new List<string>();

      foreach (string item in args[2..])
      {
        if (item != "--imp")
        {
          filterList.Add(item);
          continue;
        }

        important = true;
        break;
      }

      string text = String.Join(" ", filterList);
      Tables.Table table = new Tables.Table(tableName);

      if (action == "get")
      {
        if (!table.checkExistance())
        {
          Console.WriteLine("Can't fetch tasks from an non existent table");
          return 1;
        }

        table.loadTasks();
        List<Tables.Task> tasks = table.filter(text, important);
        foreach (Tables.Task task in tasks)
        {
          // set the text color
          if (task.important) Console.ForegroundColor = ConsoleColor.Red;
          else Console.ForegroundColor = ConsoleColor.White;

          Console.WriteLine(task.task);
        }
      }
      else if (action == "set")
      {
        if (!table.checkExistance())
        {
          table.createExistance();
          Console.WriteLine(tableName + " Table is created");
          return 1;
        }

        table.addTask(new Tables.Task(
          text,
          important
        ));
      }
      else if (action == "del")
      {
        table.yeetExistance();
      }
      else if (action == "help")
      {
        // TODO
      }
      else
      {
        Console.WriteLine("Please enter a valid action");
        return 1;
      }

      return 0;
    }
  }
}