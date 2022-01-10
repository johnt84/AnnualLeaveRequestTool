using AnnualLeaveRequestToolWebForms.Data;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using System;
using System.Configuration;
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
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ConfigureServices();
        }

        private void ConfigureServices()
        {
            var httpClient = HttpClientFactory.Create();

            var annualLeaveRequestURL = new Uri(ConfigurationManager.AppSettings["AnnualLeaveRequestAPI"].ToString());

            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.BaseAddress = annualLeaveRequestURL;

            GlobalSettings.HttpClient = httpClient;

            GlobalSettings.Container = new Container();

            GlobalSettings.Container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            GlobalSettings.Container.Register<IAnnualLeaveRequestLogic, AnnualLeaveRequestClient>(Lifestyle.Singleton);
            GlobalSettings.Container.Register<IErrorHandler, ErrorHandler>(Lifestyle.Scoped);

            GlobalSettings.Container.Verify();
        }
    }
}