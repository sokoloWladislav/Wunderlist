using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DAL.Interface.Entities
{
    public class ApplicationUserEntity : IdentityUser
    {
        public string UserProfileName { get; set; }
        public virtual ICollection<TodoListEntity> TodoLists { get; set; } = new List<TodoListEntity>();
        public virtual UserProfile Profile { get; set; }
    }
}
