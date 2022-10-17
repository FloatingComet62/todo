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
        string tableName = argumentHandler.getTableName();
        Tables.Table table = new Tables.Table(tableName);
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
          return 1;
        }

        table.addTask(new Tables.Task(
          argumentHandler.getText(),
          argumentHandler.getImportant()
        ));
      }
      else if (action == "del")
      {

        string tableName = argumentHandler.getTableName();
        Tables.Table table = new Tables.Table(tableName);
        table.yeetExistance();
      }
      else
        Console.Write(
$@"
Running Version 1.0.0

Usage:
todo get <tableName> [Text filter] : Show the tasks inside a table
todo set <tableName> [Task Name]   : Add a new task to a table
todo del <tableName>               : Delete a table

Options:
--imp       Important Mode

"
        );

      return 0;
    }
  }
}