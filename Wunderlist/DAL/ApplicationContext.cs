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
    public class ApplicationContext : IdentityDbContext<ApplicationUserEntity>
    {
        static ApplicationContext()
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<ApplicationContext>());
        }
        public ApplicationContext(string connectionString) : base(connectionString) { }
        public DbSet<TodoListEntity> TodoLists { get; set; }
        public DbSet<TodoItemEntity> TodoItems { get; set; }
    }
}
