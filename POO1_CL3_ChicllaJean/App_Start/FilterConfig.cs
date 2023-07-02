using System.Web;
using System.Web.Mvc;

namespace POO1_CL3_ChicllaJean
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
