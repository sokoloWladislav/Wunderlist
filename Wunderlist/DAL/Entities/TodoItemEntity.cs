using System;

namespace DAL.Entities
{
    public class TodoItemEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? DueDate { get; set; }

        public int TodoListEntityId { get; set; }
        public virtual TodoListEntity TodoList { get; set; }
    }
}
