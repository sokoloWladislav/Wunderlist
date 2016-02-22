using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Identity;
using DAL.Interfaces;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationContext db;
        private ApplicationUserManager userManager;
        private TodoListRepository todoListRepository;
        private TodoItemRepository todoItemRepository;

        public UnitOfWork(string conectionString)
        {
            db = new ApplicationContext(conectionString);
            userManager = new ApplicationUserManager(new UserStore<ApplicationUserEntity>(db));
            todoListRepository = new TodoListRepository(db);
            todoItemRepository = new TodoItemRepository(db);
        }

        public ApplicationUserManager UserManager => userManager;
        public ITodoListRepository TodoListRepository => todoListRepository;
        public ITodoItemRepository TodoItemRepository => todoItemRepository;

        public void Commit()
        {
            db.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    userManager.Dispose();
                    todoListRepository.Dispose();
                    todoItemRepository.Dispose();
                }
                disposed = true;
            }
        }
    }
}
