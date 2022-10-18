using System.Text;

namespace Tables
{
  // A task
  public class Task
  {
    public string task;
    public bool important;
    public Task(string task, bool important)
    {
      this.task = task;
      this.important = important;
    }

    public string raw()
    {
      string rawTask = "";
      if (this.important) rawTask += "!";
      rawTask += this.task;
      return rawTask;
    }
  }

  // A table: collection of tasks
  public class Table
  {
    public string name; // name of the table
    public List<Task> tasks; // collection of tasks

    public Table(string name)
    {
      this.name = name;
      this.tasks = new List<Task>();
    }

    // check if the table actually exists
    public bool checkExistance()
    {
      return File.Exists("tables/" + this.name + ".txt");
    }

    // create the table
    public void createExistance()
    {
      try
      {
        File.Create("tables/" + this.name + ".txt");
      }
      catch (DirectoryNotFoundException)
      {
        Directory.CreateDirectory("tables");
        Console.WriteLine("Tables folder was not found, this will happen once you first install this tool. Now it would happen again unless you manually delete the folder");
        Environment.Exit(1);
      }
    }

    // delete the table
    public void yeetExistance()
    {
      File.Delete("tables/" + this.name + ".txt");
    }

    // load the tasks into this.tasks
    public void loadTasks()
    {
      string contents = File.ReadAllText(
        "tables/" + this.name + ".txt",
        Encoding.UTF8
      );
      string[] rawTasks = contents.Split("\n");
      foreach (string rawTask in rawTasks)
      {
        bool important = false;
        if (rawTask.StartsWith("!"))
          important = true;

        this.tasks.Add(new Task(
          rawTask.Replace("!", String.Empty), // remove the !, that is to signify that it's an important task, we have handled that already
          important
        ));
      }
    }

    // add task in the table file
    public void addTask(Task task)
    {
      File.AppendAllText(
        "tables/" + this.name + ".txt",
        task.raw() + Environment.NewLine,
        Encoding.UTF8
      );
    }

    // Delete a task
    public void deleteTask(Task task)
    {

      List<string> contents = File.ReadAllLines(
        "tables/" + this.name + ".txt"
      ).ToList();
      contents.Remove(task.raw());
      File.WriteAllText(
        "tables/" + this.name + ".txt",
        String.Join("\n", contents) + "\n"
      );
    }

    // filter through the tasks
    public List<Task> filter(string filter, bool important)
    {
      List<Task> filterPass = new List<Task>();

      foreach (Task task in this.tasks)
        if (task.task.Contains(filter))
        {
          if (important)
          {
            if (task.important == true)
              filterPass.Add(task);
            continue;
          }
          filterPass.Add(task);
        }

      return filterPass;
    }
  }
}