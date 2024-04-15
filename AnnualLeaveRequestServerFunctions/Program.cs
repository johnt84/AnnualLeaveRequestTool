using AnnualLeaveRequestEFDAL.DataAccess;
using AnnualLeaveRequestEFDAL.DataAccess.Interfaces;
using AnnualLeaveRequestEFDAL.Models;
using AnnualLeaveRequestServerFunctions.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices((services) => Configure(services))
    .Build();

host.Run();

public sealed partial class Program
{
    public static void Configure(IServiceCollection services)
    {
        string connectionString = Environment.GetEnvironmentVariable("AnnualLeaveRequestDB");

        services.AddDbContext<AnnualLeaveDbContext>(
            options => options.UseSqlServer(connectionString));

        services.AddScoped<IWarmUpLogic, WarmUpLogic>();
        services.AddScoped<IAnnualLeaveRequestEFDataAccess, AnnualLeaveRequestEFDataAccess>();
        services.AddScoped<IAnnualLeaveRequestYearEFDataAccess, AnnualLeaveYearEFDataAccess>();
    }
}