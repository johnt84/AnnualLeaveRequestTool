using AnnualLeaveRequestToolBlazorWASM.Contracts;
using AnnualLeaveRequestToolBlazorWASM.Logic;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace AnnualLeaveRequestToolBlazorWASM
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddHttpClient<IAnnualLeaveRequestClient, AnnualLeaveRequestClient>
                (client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

            builder.Services.AddScoped<IErrorMessageHandler, ErrorMessageHandler>();

            await builder.Build().RunAsync();
        }
    }
}
