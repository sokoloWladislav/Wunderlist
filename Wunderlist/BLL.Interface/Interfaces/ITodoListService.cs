using System;
using System.Collections.Generic;
using BLL.Interface.DTO;
using BLL.Interface.Infrastructure;

namespace BLL.Interface.Interfaces
{
    public interface ITodoListService : IDisposable
    {
        OperationDetails CreateTodoList(TodoListDTO list);
        OperationDetails DeleteTodoList(TodoListDTO list);
        OperationDetails UpdateTodoList(TodoListDTO list);
        TodoListDTO GetTodoListById(int id);
        IEnumerable<TodoListDTO> GetAllTodoLists();
        IEnumerable<TodoListDTO> GetAllUserTodoLists(string userId);
    }
}
