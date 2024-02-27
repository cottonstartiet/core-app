namespace TasksApi;

public class TodoTaskList
{
  public TodoTaskList(string id, string name)
  {
    Id = id;
    Name = name;
  }

  public string Id { get; }
  public string Name { get; }
}
