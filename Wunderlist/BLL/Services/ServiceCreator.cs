using BLL.Interface.Interfaces;
using DAL.Interface.Repositories;
using DAL.Interface.Entities;
using DAL.Repositories;

namespace BLL.Services
{
    /*public class ServiceCreator : IServiceCreator
    {
        public IUserService CreateUserService(string connection)
        {
            return new UserService(new UnitOfWork(connection));
        }

        public ITodoListService CreateTodoListService(string connection)
        {
            return new TodoListService(new UnitOfWork(connection));
        }

        public ITodoItemService CreateTodoItemService(string connection)
        {
            return new TodoItemService(new UnitOfWork(connection));
        }
        /*private readonly UnitOfWork _uow;

        public ServiceCreator(UnitOfWork unitOfWork)
        {
            
        }
    }*/
}
