using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.DTO
{
    public class TodoListDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ApplicationUserEntityId { get; set; }
    }
}
