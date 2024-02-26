namespace TasksApi;

public class TodoTask
{
  public TodoTask(int id, string text)
  {
    Id = id;
    Text = text;
  }

  public int Id { get; }
  public string Text { get; }
}
