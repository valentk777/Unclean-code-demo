using ChristmasTreeDeliveryApp.Api.Integration;
using Microsoft.Extensions.DependencyInjection;

namespace ChristmasTreeDeliveryApp.Integrations.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services)
        {
            services.AddSingleton<IDatabase, FileDatabase>();

            return services;
        }
    }
}
