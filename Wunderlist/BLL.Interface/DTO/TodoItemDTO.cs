﻿using System;

namespace BLL.Interface.DTO
{
    public class TodoItemDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? DueDate { get; set; }
        public int OrderNumber { get; set; }
        public int TodoListEntityId { get; set; }
        public int IncertToPlace { get; set; }
        public bool ChangedFromCompletedToIncompleted { get; set; }
    }
}
