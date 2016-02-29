using System;
using DAL.Interface.Identity;

namespace DAL.Interface.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        /*ApplicationUserManager UserManager { get; }
        ITodoListRepository TodoListRepository { get; }
        ITodoItemRepository TodoItemRepository { get; }*/
        void Commit();
    }
}
