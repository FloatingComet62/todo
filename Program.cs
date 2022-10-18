namespace Program
{
  public class Program
  {
    public static int Main(string[] args)
    {
      ArgumentHandler.ArgumentHandler argumentHandler = new ArgumentHandler.ArgumentHandler(args);


      string action = argumentHandler.getAction();
      if (action == "get")
      {
        Tables.Table table = new Tables.Table(argumentHandler.getTableName());
        if (!table.checkExistance())
        {
          Console.WriteLine("Can't fetch tasks from an non existent table");
          return 1;
        }

        table.loadTasks();
        List<Tables.Task> tasks = table.filter(
          argumentHandler.getText(),
          argumentHandler.getImportant()
        );
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
        string tableName = argumentHandler.getTableName();
        Tables.Table table = new Tables.Table(tableName);
        if (!table.checkExistance())
        {
          table.createExistance();
          Console.WriteLine(tableName + " Table is created");
          return 1; // to prevent multiple access to the same file at the same time, we just exit the program and ask the user to run the program again
        }

        table.addTask(new Tables.Task(
          argumentHandler.getText(),
          argumentHandler.getImportant()
        ));
      }
      else if (action == "rm")
      {
        Tables.Table table = new Tables.Table(argumentHandler.getTableName());
        if (!table.checkExistance())
        {
          Console.WriteLine("Can't remove tasks from an non existent table");
          return 1;
        }

        table.loadTasks();
        Tables.Task task;
        try
        {
          task = table.filter(
            argumentHandler.getText(),
            argumentHandler.getImportant()
          )[0];
        }
        catch (ArgumentOutOfRangeException)
        {
          Console.WriteLine("Task not found");
          return 1;
        }

        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine(task.task);
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Are you sure you want to delete this task");
        Console.ForegroundColor = ConsoleColor.White;
        Console.ReadKey();

        table.deleteTask(task);
      }
      else if (action == "del")
      {

        Tables.Table table = new Tables.Table(argumentHandler.getTableName());
        if (!table.checkExistance())
          Console.WriteLine("Can't delete an non existent table");

        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine(table.name);
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Are you sure you want to delete this table");
        Console.ForegroundColor = ConsoleColor.White;
        Console.ReadKey();

        table.yeetExistance();
      }
      else
        Console.Write(
$@"
Running Version 1.0.0

Usage:
todo get <tableName> [Text filter] : Show the tasks inside a table
todo set <tableName> [Task Name]   : Add a new task to a table
todo rm <tableName> [Task Name]    : Removes a task
todo del <tableName>               : Delete a table

Options:
--imp       Important Mode

"
        );

      return 0;
    }
  }
}