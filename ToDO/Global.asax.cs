using System.Web;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Swashbuckle;

//using ToDO.App_Start;

namespace ToDO
{
    /// <summary>
    /// 
    /// </summary>
    public class MvcApplication : HttpApplication
    {
        /// <summary>
        /// 
        /// </summary>
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            //GlobalConfiguration.Configure(WebApiConfig.Register);

            WebApiConfig.Register(GlobalConfiguration.Configuration);

            RouteConfig.RegisterRoutes(RouteTable.Routes);

            //UnityWebActivator.Start();

            GlobalConfiguration.Configuration.EnsureInitialized();

            HttpConfiguration configuration = new HttpConfiguration();

            Bootstrapper.Init(configuration);

        }
    }
}