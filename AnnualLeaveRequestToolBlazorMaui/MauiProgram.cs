using AnnualLeaveRequest.Shared;
using AnnualLeaveRequestDapperDAL;
using AnnualLeaveRequestToolBlazorServerDapper.Data;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace AnnualLeaveRequestToolBlazorMaui
{
    public static class MauiProgram
    {
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

            var sqlConnectionConfiguration = new SqlConnectionConfiguration(config.GetConnectionString("AnnualLeaveRequestDB"));
            builder.Services.AddSingleton(sqlConnectionConfiguration);

            builder.Services.AddSingleton<AnnualLeaveRequestDataAccess>();

            builder.Services.AddSingleton<IAnnualLeaveRequestLogic, AnnualLeaveRequestLogic>();

            return builder.Build();
        }
    }
}