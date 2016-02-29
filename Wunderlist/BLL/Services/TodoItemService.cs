using System.Collections.Generic;
using AutoMapper;
using BLL.Interface.DTO;
using BLL.Interface.Infrastructure;
using BLL.Interface.Interfaces;
using DAL.Interface.Entities;
using DAL.Interface.Repositories;

namespace BLL.Services
{
    public class TodoItemService : ITodoItemService
    {
        private readonly IUnitOfWork db;

        private readonly IMapper _mapper;

        private readonly ITodoItemRepository _todoItemRepository;

        public TodoItemService(IUnitOfWork uow,ITodoItemRepository todoItemRepository)
        {
            _todoItemRepository = todoItemRepository;
            db = uow;
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<TodoItemEntity, TodoItemDTO>(); });
            _mapper = config.CreateMapper();
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
            _todoItemRepository.Create(todoItemEntity);
            db.Commit();
            return new OperationDetails(true, "TodoItem успешно добавлен", "");
        }

        public OperationDetails DeleteTodoItem(TodoItemDTO todoItem)
        {
            TodoItemEntity todoItemEntity = _todoItemRepository.GetTodoItemById(todoItem.Id);
            if (todoItemEntity != null)
            {
                _todoItemRepository.Delete(todoItemEntity);
                db.Commit();
                return new OperationDetails(true, "TodoItem успешно удален", "");
            }
            return new OperationDetails(false, "todoItem, который должен быть удален отсутствует", "Id");
        }

        public TodoItemDTO GetTodoItemById(int id)
        {
            TodoItemDTO todoItem = _mapper.Map<TodoItemEntity, TodoItemDTO>(_todoItemRepository.GetTodoItemById(id));
            return todoItem;
        }

        public IEnumerable<TodoItemDTO> GetAllTodoItems()
        {
            var todoItems = _mapper.Map<IEnumerable<TodoItemEntity>, List<TodoItemDTO>>(_todoItemRepository.GetAllTodoItems());
            return todoItems;
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
