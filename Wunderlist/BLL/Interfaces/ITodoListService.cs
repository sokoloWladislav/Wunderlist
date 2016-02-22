using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO;
using BLL.Infrastructure;

namespace BLL.Interfaces
{
    public interface ITodoListService : IDisposable
    {
        OperationDetails CreateTodoList(TodoListDTO list);
        OperationDetails DeleteTodoList(TodoListDTO list);
        TodoListDTO GetTodoListById(int id);
        IEnumerable<TodoListDTO> GetAllTodoLists();
        IEnumerable<TodoListDTO> GetAllUserTodoLists(string userId);
    }
}
