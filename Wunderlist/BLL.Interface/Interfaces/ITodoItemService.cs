using System;
using System.Collections.Generic;
using BLL.Interface.DTO;
using BLL.Interface.Infrastructure;

namespace BLL.Interface.Interfaces
{
    public interface ITodoItemService : IDisposable
    {
        OperationDetails CreateTodoItem(TodoItemDTO item);
        OperationDetails DeleteTodoItem(TodoItemDTO item);
        TodoItemDTO GetTodoItemById(int id);
        IEnumerable<TodoItemDTO> GetAllTodoItems();
    }
}
