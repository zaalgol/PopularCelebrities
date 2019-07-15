using PopularCelebrities.BL;
using PopularCelebrities.DAL;
using PopularCelebrities.Models;
using PopularCelebrities.UnityIot;
using Scraper;
using Scraper.Interfaces;
using System.Web.Http;
using System.Web.Http.Cors;
using Unity;
using Unity.Lifetime;

namespace PopularCelebrities
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var cors = new EnableCorsAttribute("*", "*", "*", "*");
            config.EnableCors(cors);
            //config.EnableCors(new EnableCorsAttribute("https://localhost:44386",
            //    headers: "*",
            //    methods: "*"));
            // Web API configuration and services
            var container = new UnityContainer();
            container.RegisterType<ICelebsBl, CelebsBl>(new HierarchicalLifetimeManager());
            container.RegisterType<IDbRepo<Celebrity>, DbRepo<Celebrity>>(new HierarchicalLifetimeManager());
            container.RegisterType<IReader<Celebrity>, CelebritiesReader>(new HierarchicalLifetimeManager());
            container.RegisterType<IPageScraper, PageScraper>(new HierarchicalLifetimeManager());
            config.DependencyResolver = new UnityResolver(container);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
