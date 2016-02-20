using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class DalTodoList
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string DalApplicationUserId { get; set; }
        public virtual DalApplicationUser DalApplicationUser { get; set; }

        public virtual ICollection<DalTodoItem> TodoItems { get; set; } = new List<DalTodoItem>();
    }
}
