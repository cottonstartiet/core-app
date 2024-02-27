namespace TasksApi;

public class TodoTask
{
  public TodoTask(string id, string text)
  {
    Id = id;
    Text = text;
  }

  public string Id { get; }
  public string Text { get; }
}
