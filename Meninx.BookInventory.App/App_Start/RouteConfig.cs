using Microsoft.AspNet.FriendlyUrls;
using System.Web.Routing;

namespace Meninx.BookInventory.App
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            var settings = new FriendlyUrlSettings();
            settings.AutoRedirectMode = RedirectMode.Permanent;
            routes.EnableFriendlyUrls(settings);

            routes.MapPageRoute("DefaultRoute", "", "~/Pages/Home.aspx");
            routes.MapPageRoute("BooksRoute", "Books/{pageName}", "~/Books/{pageName}.aspx");
            routes.MapPageRoute("CategoriesRoute", "Categories/{pageName}", "~/Categories/{pageName}.aspx");
        }
    }
}
