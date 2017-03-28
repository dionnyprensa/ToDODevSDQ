using System.Web.Http;
using Swashbuckle.Application;
using ToDO;
using WebActivatorEx;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace ToDO
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            Swashbuckle.Bootstrapper.Init(GlobalConfiguration.Configuration);

            // NOTE: If you want to customize the generated swagger or UI, use SwaggerSpecConfig and/or SwaggerUiConfig here ...
            SwaggerSpecConfig.Customize(c =>
            {
                c.IncludeXmlComments(GetXMLComments());
            });
        }
        private static string GetXMLComments()
        {
            return string.Format(@"{0}bin\ToDO.XML", System.AppDomain.CurrentDomain.BaseDirectory);
        }
    }
}