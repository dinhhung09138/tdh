using System.Web.Mvc;

namespace TDH.Areas.Website
{
    public class WebsiteAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Website";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Website_default",
                "Website/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "TDH.Areas.Website.Controllers" }
            );
        }
    }
}