using OnBoardingTaskV3.Models;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace OnBoardingTaskV3
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            UnityConfig.RegisterComponents();
            AutoMapper.Mapper.Initialize(amp=>amp.AddProfile<AutoMapperProfile>());            
        }
    }
}
