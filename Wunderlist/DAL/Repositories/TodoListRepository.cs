using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class TodoListRepository : ITodoListRepository
    {
        public ApplicationContext db { get; set; }

        public TodoListRepository(ApplicationContext context)
        {
            db = context;
        }

        public void Create(TodoListEntity item)
        {
            db.TodoLists.Add(item);
        }

        public void Delete(TodoListEntity item)
        {
            db.TodoLists.Remove(item);
        }

        public void Update(TodoListEntity item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public TodoListEntity GetTodoListById(int id)
        {
            return db.TodoLists.Find(id);
        }

        public IEnumerable<TodoListEntity> GetAllTodoLists()
        {
            return db.TodoLists;
        }

        /*public TodoListEntity GetTodoListByName(string name)
        {
            TodoListEntity todoListEntity = db.TodoLists.FirstOrDefault(item => item.Name == name);
            return todoListEntity;
        }*/

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
