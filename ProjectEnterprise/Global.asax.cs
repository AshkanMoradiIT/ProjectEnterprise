using ProjectEnterprise.Models.NHibernate.Mapping;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ProjectEnterprise
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            NHibernateHelper.Initialize();
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
