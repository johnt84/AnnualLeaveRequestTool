using AnnualLeaveRequestToolWebForms.Data;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using System;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;

namespace AnnualLeaveRequestToolWebForms
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ConfigureServices();
        }

        private void ConfigureServices()
        {
            GlobalSettings.Container = new Container();

            GlobalSettings.Container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            GlobalSettings.Container.Register<IAnnualLeaveRequestLogic, AnnualLeaveRequestLogic>(Lifestyle.Singleton);
     
            GlobalSettings.Container.Verify();
        }
    }
}