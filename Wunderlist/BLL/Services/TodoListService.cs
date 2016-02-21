using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BLL.DTO;
using BLL.Infrastructure;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services
{
    public class TodoListService : ITodoListService
    {
        private readonly IUnitOfWork db;

        private readonly IMapper mapper;

        public TodoListService(IUnitOfWork uow)
        {
            db = uow;
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<TodoListEntity, TodoListDTO>(); });
            mapper = config.CreateMapper();
        }
        public OperationDetails CreateTodoList(TodoListDTO todoList)
        {
            TodoListEntity todoListEntity = new TodoListEntity
            {
                    ApplicationUserEntityId = todoList.ApplicationUserEntityId,
                    Name = todoList.Name
            };
                db.TodoListRepository.Create(todoListEntity);
                db.Commit();
                return new OperationDetails(true, "TodoList успешно добавлен", "");
        }

        public OperationDetails DeleteTodoList(TodoListDTO todoList)
        {
            TodoListEntity todoListEntity = db.TodoListRepository.GetTodoListById(todoList.Id);
            if (todoListEntity != null)
            {
                db.TodoListRepository.Delete(todoListEntity);
                db.Commit();
                return new OperationDetails(true, "TodoList успешно удален", "");
            }
            return new OperationDetails(false, "TodoList, который должен быть удален отсутствует", "Id");
        }

        public TodoListDTO GetTodoListById(int id)
        {
            TodoListDTO todoList = mapper.Map<TodoListEntity, TodoListDTO>(db.TodoListRepository.GetTodoListById(id));
            return todoList;
        }

        public IEnumerable<TodoListDTO> GetAllTodoLists()
        {
            var todoLists = mapper.Map<IEnumerable<TodoListEntity>, List<TodoListDTO>>(db.TodoListRepository.GetAllTodoLists());
            return todoLists;
        }

        public IEnumerable<TodoListDTO> GetAllUserTodoLists(string userId)
        {
            var todoLists = mapper.Map<IEnumerable<TodoListEntity>, List<TodoListDTO>>(db.TodoListRepository.GetAllTodoLists());
            return todoLists.Where(item => item.ApplicationUserEntityId == userId);

        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
