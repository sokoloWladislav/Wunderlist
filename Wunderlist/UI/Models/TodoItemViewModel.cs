using System;

namespace UI.Models
{
    public class TodoItemViewModel
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        public string Note { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? DueDate { get; set; }
        public int InsertToPosition { get; set; }
        public int TodoListEntityId { get; set; }
    }
}