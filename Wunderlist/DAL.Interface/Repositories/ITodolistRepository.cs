using System;
using System.Collections.Generic;
using DAL.Interface.Entities;

namespace DAL.Interface.Repositories
{
    public interface ITodoListRepository : IDisposable
    {
        void Create(TodoListEntity item);
        void Delete(TodoListEntity item);
        void Update(TodoListEntity item);
        //DalTodoList GetTodoListByName(string name);
        TodoListEntity GetTodoListById(int id);
        IEnumerable<TodoListEntity> GetAllTodoLists();
    }
}
