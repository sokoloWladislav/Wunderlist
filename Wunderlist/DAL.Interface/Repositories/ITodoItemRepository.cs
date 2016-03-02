using System;
using System.Collections.Generic;
using DAL.Interface.Entities;

namespace DAL.Interface.Repositories
{
    public interface ITodoItemRepository : IDisposable
    {
        void Create(TodoItemEntity item);
        void Delete(TodoItemEntity item);
        void Update(TodoItemEntity item);
        TodoItemEntity GetTodoItemById(int id);
        IEnumerable<TodoItemEntity> GetAllTodoItems();
    }
}
