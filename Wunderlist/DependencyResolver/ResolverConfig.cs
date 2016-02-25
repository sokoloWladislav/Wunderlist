using System.Data.Entity;
using BLL.Interface.Interfaces;
using BLL.Services;
using DAL;
using DAL.Interface.Entities;
using DAL.Interface.Identity;
using DAL.Interface.Repositories;
using DAL.Repositories;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Ninject;
using Ninject.Web.Common;

namespace DependencyResolver
{
    public static class ResolverConfig
    {
        public static void Configure(this IKernel kernel)
        {
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();
            kernel.Bind(typeof(DbContext)).To(typeof(ApplicationContext)).InRequestScope();
            kernel.Bind<IdentityDbContext<ApplicationUserEntity>>().To<ApplicationContext>().InRequestScope();
            kernel.Bind(typeof(IUserStore<ApplicationUserEntity>)).To(typeof(UserStore<ApplicationUserEntity>)).InRequestScope();
            /*kernel.Bind<IUserStore<ApplicationUserEntity>>()
                .To<UserStore<ApplicationUserEntity>>()
                .WithConstructorArgument("context", kernel.Get<IdentityDbContext<ApplicationUserEntity>>());*/
            kernel.Bind(typeof (UserManager<ApplicationUserEntity>))
                .ToSelf();
            kernel.Bind<ITodoItemRepository>().To<TodoItemRepository>();
            kernel.Bind<ITodoListRepository>().To<TodoListRepository>();
            kernel.Bind<ITodoItemService>().To<TodoItemService>();
            kernel.Bind<ITodoListService>().To<TodoListService>();
            kernel.Bind<IUserService>().To<UserService>();
            kernel.Bind<ApplicationUserManager>().To<ApplicationUserManager>();
            


        }
    }
}
