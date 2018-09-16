using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;


namespace KnockOutJsMvcCreateArticle
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Attribute routing.
            //  config.Routes.MapHttpRoute(
            //  name: "DefaultApi",
            //  routeTemplate: "api/{controller}/{action}/{id}",
            //   defaults: new { id = RouteParameter.Optional }
            // For Controller names only
            config.Routes.MapHttpRoute(
          name: "ControllerOnly",
          routeTemplate: "api/{controller}");
            // Controller and ID
            config.Routes.MapHttpRoute(
            name: "DefaultApi",
            routeTemplate: "api/{controller}/{id}",
            defaults: null,
            constraints: new { id = @"^\d+$" }
            );
            // Controller and Action
            config.Routes.MapHttpRoute(
            name: "WithActioname",
            routeTemplate: "api/{controller}/{action}");
            // Controller, action, and ID

            config.Routes.MapHttpRoute(
            name: "WithActionameandID",
            routeTemplate: "api/{controller}/{action}/{id}"
            );
            //List<Fruit> GetSeasonalFruits() = api/FruitApi/GetSeasonalFruit
       

           

            var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
            config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);
            // Uncomment the following line of code to enable query support for actions with an IQueryable or IQueryable<T> return type.
            // To avoid processing unexpected or malicious queries, use the validation settings on QueryableAttribute to validate incoming queries.
            // For more information, visit http://go.microsoft.com/fwlink/?LinkId=279712.
            //config.EnableQuerySupport();
        }
    }
}