using Swashbuckle.Application;
using System.Web.Http;

namespace Meninx.BookInventory.App
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            GlobalConfiguration.Configuration
                .EnableSwagger(x =>
                    {
                        x.SingleApiVersion("v1", "Book Inventory API");
                    })
                .EnableSwaggerUi(x =>
                    {
                        x.DocumentTitle("Book Inventory API");
                    });
        }
    }
}
