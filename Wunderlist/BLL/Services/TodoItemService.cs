using System.Collections.Generic;
using AutoMapper;
using BLL.DTO;
using BLL.Infrastructure;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services
{
    public class TodoItemService : ITodoItemService
    {
        private readonly IUnitOfWork db;

        private readonly IMapper mapper;

        public TodoItemService(IUnitOfWork uow)
        {
            db = uow;
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<TodoItemEntity, TodoItemDTO>(); });
            mapper = config.CreateMapper();
        }
        public OperationDetails CreateTodoItem(TodoItemDTO todoItem)
        {
            TodoItemEntity todoItemEntity = new TodoItemEntity
            {
                TodoListEntityId = todoItem.TodoListEntityId,
                Name = todoItem.Name,
                Note = todoItem.Note,
                IsCompleted = false
            };
            db.TodoItemRepository.Create(todoItemEntity);
            db.Commit();
            return new OperationDetails(true, "TodoItem успешно добавлен", "");
        }

        public OperationDetails DeleteTodoItem(TodoItemDTO todoItem)
        {
            TodoItemEntity todoItemEntity = db.TodoItemRepository.GetTodoItemById(todoItem.Id);
            if (todoItemEntity != null)
            {
                db.TodoItemRepository.Delete(todoItemEntity);
                db.Commit();
                return new OperationDetails(true, "TodoItem успешно удален", "");
            }
            return new OperationDetails(false, "todoItem, который должен быть удален отсутствует", "Id");
        }

        public TodoItemDTO GetTodoItemById(int id)
        {
            TodoItemDTO todoItem = mapper.Map<TodoItemEntity, TodoItemDTO>(db.TodoItemRepository.GetTodoItemById(id));
            return todoItem;
        }

        public IEnumerable<TodoItemDTO> GetAllTodoItems()
        {
            var todoItems = mapper.Map<IEnumerable<TodoItemEntity>, List<TodoItemDTO>>(db.TodoItemRepository.GetAllTodoItems());
            return todoItems;
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
