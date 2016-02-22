using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Interfaces
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
