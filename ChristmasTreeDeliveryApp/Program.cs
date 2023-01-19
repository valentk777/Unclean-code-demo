using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ChristmasTreeDeliveryApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = Host
                .CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    services.AddTransient<ChristmasTreeDeliveryApp>();
                    //ervices.AddSingleton<FileService>();
                    //services.AddSingleton<IPrimeService, PrimeService>();
                    //services.AddSingleton<IPrimeService, PrimeServiceSlow>();
                })
                .Build();

            var app = host.Services.GetRequiredService<ChristmasTreeDeliveryApp>();
            app.StartService();
        }
    }
}
