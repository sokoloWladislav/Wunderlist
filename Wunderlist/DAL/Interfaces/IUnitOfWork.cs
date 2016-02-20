using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Identity;

namespace DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ApplicationUserManager UserManager { get; set; }
        ITodoListRepository TodoListRepository { get; set; }
        ITodoItemRepository TodoItemRepository { get; set; }
    }
}
