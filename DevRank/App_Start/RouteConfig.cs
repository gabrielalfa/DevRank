using System.Web.Mvc;
using System.Web.Routing;

namespace DevRank
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "MaintenanceAccent",
                url: "Manutenção/CriarDesafiosManual",
                defaults: new { controller = "Manutencao", action = "CriarDesafiosManual" }
            );

            routes.MapRoute(
                name: "MaintenanceAscii",
                url: "Manutencao/CriarDesafiosManual",
                defaults: new { controller = "Manutencao", action = "CriarDesafiosManual" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
