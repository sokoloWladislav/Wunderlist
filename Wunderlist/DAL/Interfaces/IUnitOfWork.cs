using System;
using DAL.Identity;

namespace DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ApplicationUserManager UserManager { get; }
        ITodoListRepository TodoListRepository { get; }
        ITodoItemRepository TodoItemRepository { get; }
    }
}
