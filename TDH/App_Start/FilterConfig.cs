using System.Web;
using System.Web.Mvc;

namespace TDH
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new Areas.Administrator.Filters.ExceptionFilterAttribute());
        }
    }
}
