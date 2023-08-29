using System;
using System.Web;
using System.Web.Http;
using Unity;
using Unity.WebApi;

namespace Meninx.BookInventory.App
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            UnityContainer container = new UnityContainer();

            container.RegisterType<IReadRepository<Book>, BookRepository>();
            container.RegisterType<IReadRepository<Book, Guid>, BookRepository>();
            container.RegisterType<IRepository<Book>, BookRepository>();
            container.RegisterType<IRepository<Book, Guid>, BookRepository>();

            container.RegisterType<IReadRepository<Category>, BaseRepository<BookInventoryDbContext, Category>>();
            container.RegisterType<IReadRepository<Category, Guid>, BaseRepository<BookInventoryDbContext, Category>>();
            container.RegisterType<IRepository<Category>, BaseRepository<BookInventoryDbContext, Category>>();
            container.RegisterType<IRepository<Category, Guid>, BaseRepository<BookInventoryDbContext, Category>>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);

            HttpRuntime.WebObjectActivator = new UnityContainerServiceProvider(HttpRuntime.WebObjectActivator, container);
        }
    }
}