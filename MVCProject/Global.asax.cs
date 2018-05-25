using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Http;
using System.Web.Optimization;
using MVCProject.App_Start;

namespace MVCProject
{
    public class Global : HttpApplication
    {
       // private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(MvcApplication));
        protected void Application_Start()
        {
           // log4net.Config.XmlConfigurator.Configure();
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
