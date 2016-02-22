using System.Collections.Generic;
using System.Data.Entity;
using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class TodoItemRepository : ITodoItemRepository
    {
        public ApplicationContext db { get; set; }

        public TodoItemRepository(ApplicationContext context)
        {
            db = context;
        }

        public void Create(TodoItemEntity item)
        {
            db.TodoItems.Add(item);
        }

        public void Delete(TodoItemEntity item)
        {
            db.TodoItems.Remove(item);
        }

        public void Update(TodoItemEntity item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public TodoItemEntity GetTodoItemById(int id)
        {
            return db.TodoItems.Find(id);
        }

        public IEnumerable<TodoItemEntity> GetAllTodoItems()
        {
            return db.TodoItems;
        }

        /*public TodoItemEntity GetTodoItemByName(string name)
        {
            TodoItemEntity todoItemEntity = db.TodoItems.FirstOrDefault(item => item.Name == name);
            return todoItemEntity;
        }*/

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
