using System.Web.Http;

namespace ToDO
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes(); 

            config.Routes.MapHttpRoute(
                "DefaultApi",
                "api/task/{id}",
                new {id = RouteParameter.Optional}
            );
        }
    }
}