using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.Interface.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DAL
{
    public class ApplicationContext : IdentityDbContext<ApplicationUserEntity>
    {
        public ApplicationContext() : base("DefaultConnection")
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<ApplicationContext>());
        }
        public ApplicationContext(string connectionString) : base(connectionString) { }
        public DbSet<TodoListEntity> TodoLists { get; set; }
        public DbSet<TodoItemEntity> TodoItems { get; set; }
    }
}
