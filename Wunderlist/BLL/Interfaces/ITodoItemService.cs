using System.Collections.Generic;
using BLL.DTO;
using BLL.Infrastructure;

namespace BLL.Interfaces
{
    public interface ITodoItemService
    {
        OperationDetails CreateTodoItem(TodoItemDTO item);
        OperationDetails DeleteTodoItem(TodoItemDTO item);
        TodoItemDTO GetTodoItemById(int id);
        IEnumerable<TodoItemDTO> GetAllTodoItems();
    }
}
