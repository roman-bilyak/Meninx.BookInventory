using Microsoft.AspNet.FriendlyUrls;
using System.Web.Routing;

namespace Meninx.BookInventory.App
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            FriendlyUrlSettings settings = new FriendlyUrlSettings
            {
                AutoRedirectMode = RedirectMode.Permanent
            };
            routes.EnableFriendlyUrls(settings);

            routes.MapPageRoute("DefaultRoute", "", "~/Default.aspx");
            routes.MapPageRoute("BooksRoute", "Books/{pageName}", "~/Books/{pageName}.aspx");
            routes.MapPageRoute("CategoriesRoute", "Categories/{pageName}", "~/Categories/{pageName}.aspx");
        }
    }
}
