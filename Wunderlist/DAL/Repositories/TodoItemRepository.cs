using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.Interface.Entities;
using DAL.Interface.Repositories;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DAL.Repositories
{
    public class TodoItemRepository : ITodoItemRepository
    {
        private readonly DbContext _db;
        private readonly DbSet<TodoItemEntity> _todoEntityDbSet;
        public TodoItemRepository(DbContext context)
        {
            _db = context;
            _todoEntityDbSet = context.Set<TodoItemEntity>();
        }

        public void Create(TodoItemEntity item)
        {
            _todoEntityDbSet.Add(item);
        }

        public void Delete(TodoItemEntity item)
        {
            _todoEntityDbSet.Remove(item);

        }

        public void Update(TodoItemEntity item)
        {
            var entity = _db.Set<TodoItemEntity>().Find(item.Id);
            if (entity != null)
            {
                entity.DueDate = item.DueDate;
                entity.IsCompleted = item.IsCompleted;
                entity.Name = item.Name;
                entity.Note = item.Note;
                entity.OrderNumber = item.OrderNumber;
                _db.Entry(entity).State = EntityState.Modified;
            }
        }

        public TodoItemEntity GetTodoItemById(int id)
        {
            return _todoEntityDbSet.Find(id);
        }

        public IEnumerable<TodoItemEntity> GetAllTodoItems()
        {
            return _db.Set<TodoItemEntity>().AsEnumerable();
        }

        /*public TodoItemEntity GetTodoItemByName(string name)
        {
            TodoItemEntity todoItemEntity = db.TodoItems.FirstOrDefault(item => item.Name == name);
            return todoItemEntity;
        }*/

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
