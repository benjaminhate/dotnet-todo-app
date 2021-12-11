using System;

namespace Todo.Domain
{
    public class TodoItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        
        public string Description { get; set; }
        public bool IsComplete { get; set; }

        public void ModifyFrom(TodoItem other)
        {
            Title = other.Title;
            Description = other.Description;
            IsComplete = other.IsComplete;
        }
        
        private bool Equals(TodoItem other)
        {
            return Id == other.Id 
                   && Title == other.Title 
                   && Description == other.Description
                   && IsComplete == other.IsComplete;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((TodoItem) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Title, Description, IsComplete);
        }
    }
}