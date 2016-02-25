﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DependencyResolver;
using Ninject;

namespace UI.Infrastructure
{
    public class CustomDependencyResolver:IDependencyResolver
    {
        private IKernel kernel;

        public CustomDependencyResolver(IKernel kernel)
        {
            this.kernel = kernel;
            kernel.Configure();
        }
        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
    }
}