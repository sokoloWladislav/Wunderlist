using System;
using System.Collections.Generic;
using System.Linq;
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

        private const int ScalingMultiplier = 10000;

        private void NormalizeToDoItemOrderingNumbers()
        {
            var todoItems = _todoItemRepository.GetAllTodoItems().OrderBy(x => x.OrderNumber);
            int currentNumber = 0;
            foreach (var item in todoItems)
            {
                currentNumber += ScalingMultiplier;
                if (item.OrderNumber != currentNumber)
                {
                    item.OrderNumber = currentNumber;
                    _todoItemRepository.Update(item);
                }
            }
            db.Commit();
        }

        

        private int GetToDoItemOrderingNumber(int todoListEntityId, int placeInList,out bool normalizationRequired)
        {
            var todoItems = _todoItemRepository.GetAllTodoItems().Where(x => x.TodoListEntityId == todoListEntityId);
            normalizationRequired = false;
            int resultOrderingNumber=ScalingMultiplier;
            if (placeInList > 0)
            {
                var todoItemsArray= todoItems.OrderBy(x => x.OrderNumber).Skip(placeInList).Take(2).ToArray();
                if (todoItemsArray.Count() == 2)
                {
                    int bottomToDoItemOrderingNumber = todoItemsArray[0].OrderNumber;
                    int topToDoItemOrderingNumber = todoItemsArray[1].OrderNumber;
                    int delta = (topToDoItemOrderingNumber - bottomToDoItemOrderingNumber)/2;
                    resultOrderingNumber = bottomToDoItemOrderingNumber + delta;
                    normalizationRequired = delta == 0;
                }
            }
            else
            {
                if (placeInList == 0)
                {
                    var bottomToDoItem = todoItems.OrderBy(x=>x.OrderNumber).FirstOrDefault();
                    if (bottomToDoItem != null)
                    {
                        int delta = bottomToDoItem.OrderNumber/2;
                        resultOrderingNumber = delta;
                        if (delta < 1)
                            normalizationRequired = true;
                    }
                    else
                        resultOrderingNumber = ScalingMultiplier;
                }
                else
                {
                    var topToDoItem = todoItems.OrderByDescending(x => x.OrderNumber).FirstOrDefault();
                    resultOrderingNumber = topToDoItem == null ? ScalingMultiplier*(todoItems.Count()+1) : Math.Max(ScalingMultiplier*(todoItems.Count()+1),(topToDoItem.OrderNumber/ScalingMultiplier+1)*ScalingMultiplier);
                }
            }
            return resultOrderingNumber;
        }

        public TodoItemService(IUnitOfWork uow,ITodoItemRepository todoItemRepository)
        {
            _todoItemRepository = todoItemRepository;
            db = uow;
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<TodoItemEntity, TodoItemDTO>(); });
            _mapper = config.CreateMapper();
        }

        public OperationDetails UpdateTodoItem(TodoItemDTO todoItem)
        {
            var entity = new TodoItemEntity
            {
                DueDate = todoItem.DueDate,
                Id = todoItem.Id,
                IsCompleted = todoItem.IsCompleted,
                Name = todoItem.Name,
                Note = todoItem.Note,
                OrderNumber = todoItem.OrderNumber,
                TodoListEntityId = todoItem.TodoListEntityId
            };
            _todoItemRepository.Update(entity);
            db.Commit();
            return new OperationDetails(true, "TodoItem успешно изменён", "");
        }

        public OperationDetails CreateTodoItem(TodoItemDTO todoItem)
        {
            var todoItemEntity = new TodoItemEntity
            {
                TodoListEntityId = todoItem.TodoListEntityId,
                Name = todoItem.Name,
                Note = todoItem.Note,
                IsCompleted = false
            };
            bool normalizationRequired;
            todoItemEntity.OrderNumber = GetToDoItemOrderingNumber(todoItemEntity.TodoListEntityId, -1,
                out normalizationRequired);
            _todoItemRepository.Create(todoItemEntity);
            db.Commit();
            if(normalizationRequired)
                NormalizeToDoItemOrderingNumbers();
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
