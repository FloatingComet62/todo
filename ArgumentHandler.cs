namespace ArgumentHandler
{
  public class ArgumentHandler
  {
    private string[] args;

    public ArgumentHandler(string[] args)
    {
      this.args = args;
    }

    public string getAction()
    {
      try
      {
        return this.args.ElementAt(0);
      }
      catch (ArgumentNullException)
      {
        Console.WriteLine("Unable to handle null actions");
        Environment.Exit(1);
      }
      catch (ArgumentOutOfRangeException)
      {
        Console.WriteLine("Not enough arguments were provided\nTry todo help");
        Environment.Exit(1);
      }

      return "";
    }
    public string getTableName()
    {
      try
      {
        return this.args.ElementAt(1);
      }
      catch (ArgumentNullException)
      {
        Console.WriteLine("Unable to handle null tables");
        Environment.Exit(1);
      }
      catch (ArgumentOutOfRangeException)
      {
        Console.WriteLine("Not enough arguments were provided");
        Environment.Exit(1);
      }

      return "";
    }
    public string getText()
    {
      List<string> filterList = new List<string>();

      foreach (string item in this.args.Where((arg, index) => index >= 2))
        if (item != "--imp")
          filterList.Add(item);

      return String.Join(" ", filterList);
    }
    public bool getImportant()
    {
      bool important = false;

      foreach (string item in this.args.Where((arg, index) => index >= 2))
        if (item == "--imp")
          important = true;

      return important;
    }
  }
}