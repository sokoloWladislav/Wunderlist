using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.DTO
{
    public class TodoItemDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime DueDate { get; set; }
        public int TodoListEntityId { get; set; }
    }
}
