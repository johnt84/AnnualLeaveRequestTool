using AnnualLeaveRequestToolWebForms.Data;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
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
            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            GlobalSettings.Container = new Container();

            GlobalSettings.Container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            GlobalSettings.Container.Register<IAnnualLeaveRequestLogic, AnnualLeaveRequestClient>(Lifestyle.Singleton);

            //GlobalSettings.Container.Register<HttpClient>(Lifestyle.Singleton);
            GlobalSettings.HttpClient = httpClient;

            GlobalSettings.Container.Verify();
        }
    }
}