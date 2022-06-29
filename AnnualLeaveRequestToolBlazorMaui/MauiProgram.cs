using AnnualLeaveRequest.Shared;
using AnnualLeaveRequestDapperDAL;
using AnnualLeaveRequestToolBlazorMaui.Data.Interfaces;
using AnnualLeaveRequestToolBlazorMaui.Data;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace AnnualLeaveRequestToolBlazorMaui
{
    public static class MauiProgram
    {
        public static string Base = DeviceInfo.Platform == DevicePlatform.Android 
                                        ? "http://10.0.2.2" 
                                        : "https://localhost";

        public static string APIUrl = $"{Base}:5003/api/";

        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            var a = Assembly.GetExecutingAssembly();
            using var stream = a.GetManifestResourceStream("AnnualLeaveRequestToolBlazorMaui.appsettings.json");

            var config = new ConfigurationBuilder()
              .AddJsonStream(stream)
              .Build();

            builder.Services.AddMauiBlazorWebView();
#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
#endif
            
            builder.Services.AddHttpClient<IAnnualLeaveRequestClient, AnnualLeaveRequestClient>(client =>
            {
                client.BaseAddress = new Uri(APIUrl);
            });

            var sqlConnectionConfiguration = new SqlConnectionConfiguration(config.GetConnectionString("AnnualLeaveRequestDB"));
            builder.Services.AddSingleton(sqlConnectionConfiguration);

            builder.Services.AddSingleton<AnnualLeaveRequestDataAccess>();

            return builder.Build();
        }
    }
}