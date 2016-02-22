using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class TodoListEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string ApplicationUserEntityId { get; set; }
        public virtual ApplicationUserEntity DalApplicationUser { get; set; }

        public virtual ICollection<TodoItemEntity> TodoItems { get; set; } = new List<TodoItemEntity>();
    }
}
