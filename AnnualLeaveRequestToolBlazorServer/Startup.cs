using AnnualLeaveRequest.Shared;
using AnnualLeaveRequestDAL;
using AnnualLeaveRequestToolBlazorServer.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace AnnualLeaveRequestToolBlazorServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();

            services.AddServerSideBlazor()
                .AddCircuitOptions(options =>
                {
                    //can toggle detailed errors on or off from app settings
                    options.DetailedErrors = Convert.ToBoolean(Configuration["DetailedErrors"]);
                });

            var sqlConnectionConfiguration = new SqlConnectionConfiguration(Configuration.GetConnectionString("AnnualLeaveRequestDB"));
            services.AddSingleton(sqlConnectionConfiguration);

            services.AddSingleton<AnnualLeaveRequestDataAccess>();

            services.AddSingleton<IAnnualLeaveRequestLogic, AnnualLeaveRequestLogic>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
