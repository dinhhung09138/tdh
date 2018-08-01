using System.Web.Mvc;

namespace TDH.Areas.Money
{
    public class MoneyAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Money";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Money_default",
                "Money/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "TDH.Areas.Money.Controllers" }
            );
        }
    }
}