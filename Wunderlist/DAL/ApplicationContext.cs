using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DAL
{
    public class ApplicationContext : IdentityDbContext<DalApplicationUser>
    {
        public ApplicationContext(string connectionString) : base(connectionString) { }
        public DbSet<DalTodoList> TodoLists { get; set; }
        public DbSet<DalTodoItem> TodoItems { get; set; }
    }
}
