﻿using System.Web.Http;

namespace UI
{
    class WebApiConfig
    {
        public static void Register(HttpConfiguration configuration)
        {
            
            configuration.Routes.MapHttpRoute(
                "API Default", 
                "api/{controller}/{id}",
                new { id = RouteParameter.Optional }
                );
            configuration.Routes.MapHttpRoute(
                name: "ApiByActionAndId",
                routeTemplate: "api/{controller}/{action}/{id}"
                );
        }
    }
}