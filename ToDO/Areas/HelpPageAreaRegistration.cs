using System.Web.Http;
using System.Web.Mvc;
using ToDO.Areas.HelpPage;

namespace ToDO.Areas
{
    public class HelpPageAreaRegistration : AreaRegistration
    {
        public override string AreaName => "HelpPage";

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "HelpPage_Default",
                "help/{action}/{apiId}",
                new { controller = "Help", action = "Index", apiId = UrlParameter.Optional });

            HelpPageConfig.Register(GlobalConfiguration.Configuration);
        }
    }
}