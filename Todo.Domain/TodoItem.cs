namespace Todo.Domain
{
    public class TodoItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsComplete { get; set; }

        public void ModifyFrom(TodoItem other)
        {
            Title = other.Title;
            IsComplete = other.IsComplete;
        }
    }
}