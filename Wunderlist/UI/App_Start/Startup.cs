﻿using System;
using BLL.Interface.Interfaces;
using BLL.Services;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Ninject.Web.Common.OwinHost;
using Owin;

[assembly: OwinStartup(typeof(UI.App_Start.Startup))]

namespace UI.App_Start
{
    public class Startup
    {
        //IServiceCreator serviceCreator = new ServiceCreator();
        /*private IServiceCreator serviceCreator;
        public Startup(IServiceCreator ServiceCreator)
        {
                serviceCreator=ServiceCreator;
        }*/

        public void Configuration(IAppBuilder app)
        {
            
            /*app.CreatePerOwinContext(CreateUserService);
            app.CreatePerOwinContext(CreateTodoListService);
            app.CreatePerOwinContext(CreateTodoItemService);*/
            //app.UseNinjectMiddleware()
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                ExpireTimeSpan = TimeSpan.FromHours(4.0),

            });
        }

        /*private IUserService CreateUserService()
        {
            return serviceCreator.CreateUserService("DefaultConnection");
        }

        private ITodoListService CreateTodoListService()
        {
            return serviceCreator.CreateTodoListService("DefaultConnection");
        }

        private ITodoItemService CreateTodoItemService()
        {
            return serviceCreator.CreateTodoItemService("DefaultConnection");
        }*/
    }
}
