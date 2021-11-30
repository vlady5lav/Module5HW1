using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;

using ModuleHW.StartApp.Abstractions;
using ModuleHW.StartApp.Services;

namespace ModuleHW.StartApp
{
    public class Starter
    {
        public async Task Run()
        {
            var serviceProvider = new ServiceCollection()
                .AddTransient<Application>()
                .AddTransient<IHttpService, HttpService>()
                .AddTransient<IApiService, ApiService>()
                .BuildServiceProvider();

            var app = serviceProvider.GetRequiredService<Application>();
            await app.Start();
        }
    }
}
