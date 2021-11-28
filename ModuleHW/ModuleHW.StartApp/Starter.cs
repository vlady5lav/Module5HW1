using Microsoft.Extensions.DependencyInjection;

using ModuleHW.StartApp.Services;

namespace ModuleHW.StartApp
{
    public class Starter
    {
        public void Run()
        {
            var serviceProvider = new ServiceCollection()
                .AddTransient<Application>()
                .AddTransient<HttpService>()
                .AddTransient<DataService>()
                .BuildServiceProvider();

            var app = serviceProvider.GetRequiredService<Application>();
            app.Start();
        }
    }
}
