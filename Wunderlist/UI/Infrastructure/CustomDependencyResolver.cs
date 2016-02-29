using System;
using System.Collections.Generic;
using System.Web.Mvc;
using DependencyResolver;
using Ninject;

namespace UI.Infrastructure
{
    public class CustomDependencyResolver:IDependencyResolver
    {
        private readonly IKernel _kernel;

        public CustomDependencyResolver(IKernel kernel)
        {
            _kernel = kernel;
            kernel.Configure();
        }
        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }
    }
}