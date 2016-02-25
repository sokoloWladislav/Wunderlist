using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.Interface.Entities;
using DAL.Interface.Repositories;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DAL.Repositories
{
    public class TodoListRepository : ITodoListRepository
    {
        private readonly IdentityDbContext<ApplicationUserEntity> _db;
        private readonly DbSet<TodoListEntity> _todoListDbSet; 

        public TodoListRepository(IdentityDbContext<ApplicationUserEntity> context)
        {
            _db = context;
            _todoListDbSet = context.Set<TodoListEntity>();
        }

        public void Create(TodoListEntity item)
        {
            _todoListDbSet.Add(item);
        }

        public void Delete(TodoListEntity item)
        {
            _todoListDbSet.Remove(item);
        }

        public void Update(TodoListEntity item)
        {
            var entity = _todoListDbSet.Find(item.Id);
            if (entity != null)
            {
                entity.Name = item.Name;
                entity.ApplicationUserEntityId = item.ApplicationUserEntityId;
                _db.Entry(item).State = EntityState.Modified;
            }
        }

        public TodoListEntity GetTodoListById(int id)
        {
            return _todoListDbSet.Find(id);
        }

        public IEnumerable<TodoListEntity> GetAllTodoLists()
        {
            return _todoListDbSet.AsEnumerable();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
