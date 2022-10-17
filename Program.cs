using Tables;

namespace Program
{
  public class Program
  {
    public static int Main(string[] args)
    {
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

      string filter = String.Join(" ", filterList);

      if (action == "get")
      {
        Tables.Table table = new Tables.Table(tableName);
        if (!table.checkExistance())
        {
          Console.WriteLine("Can't fetch tasks from a non existent table");
          return 1;
        }

        table.loadTasks();
        List<Tables.Task> tasks = table.filter(filter, important);
        foreach (Tables.Task task in tasks)
        {
          if (task.important) Console.ForegroundColor = ConsoleColor.Red;
          else Console.ForegroundColor = ConsoleColor.White;
          Console.WriteLine(task.task);
        }
      }
      else if (action == "set")
      {

      }
      else
      {
        Console.WriteLine("Please enter a action");
        return 1;
      }

      return 0;
    }
  }
}