using System.Web;
using System.Web.Mvc;

namespace Audit_and_Hybrid_Auth_Server
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
