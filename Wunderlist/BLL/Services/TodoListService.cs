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
    public class TodoListService : ITodoListService
    {
        private readonly IUnitOfWork db;

        private readonly IMapper _mapper;

        private readonly ITodoListRepository _todoListRepository;

        public TodoListService(IUnitOfWork uow,ITodoListRepository todoListRepository)
        {
            _todoListRepository = todoListRepository;
            db = uow;
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<TodoListEntity, TodoListDTO>(); });
            _mapper = config.CreateMapper();
        }
        public OperationDetails CreateTodoList(TodoListDTO todoList)
        {
            TodoListEntity todoListEntity = new TodoListEntity
            {
                    ApplicationUserEntityId = todoList.ApplicationUserEntityId,
                    Name = todoList.Name
            };
                _todoListRepository.Create(todoListEntity);
                db.Commit();
                return new OperationDetails(true, "TodoList успешно добавлен", "");
        }

        public OperationDetails DeleteTodoList(TodoListDTO todoList)
        {
            TodoListEntity todoListEntity = _todoListRepository.GetTodoListById(todoList.Id);
            if (todoListEntity != null)
            {
                _todoListRepository.Delete(todoListEntity);
                db.Commit();
                return new OperationDetails(true, "TodoList успешно удален", "");
            }
            return new OperationDetails(false, "TodoList, который должен быть удален отсутствует", "Id");
        }

        public TodoListDTO GetTodoListById(int id)
        {
            TodoListDTO todoList = _mapper.Map<TodoListEntity, TodoListDTO>(_todoListRepository.GetTodoListById(id));
            return todoList;
        }

        public IEnumerable<TodoListDTO> GetAllTodoLists()
        {
            var todoLists = _mapper.Map<IEnumerable<TodoListEntity>, List<TodoListDTO>>(_todoListRepository.GetAllTodoLists());
            return todoLists;
        }

        public IEnumerable<TodoListDTO> GetAllUserTodoLists(string userId)
        {
            var todoLists = _mapper.Map<IEnumerable<TodoListEntity>, List<TodoListDTO>>(_todoListRepository.GetAllTodoLists());
            return todoLists.Where(item => item.ApplicationUserEntityId == userId);

        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
