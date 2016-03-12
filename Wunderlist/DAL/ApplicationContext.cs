using System.Data.Entity;
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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserProfile>().HasRequired(x => x.UserEntity);
        }

        public ApplicationContext(string connectionString) : base(connectionString) { }
        public DbSet<TodoListEntity> TodoLists { get; set; }
        public DbSet<TodoItemEntity> TodoItems { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
    }
}
