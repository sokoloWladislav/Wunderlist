using BLL.Interfaces;
using DAL.Repositories;

namespace BLL.Services
{
    public class ServiceCreator
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
    }
}
