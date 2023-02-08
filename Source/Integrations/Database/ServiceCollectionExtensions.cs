using ChristmasTreeDeliveryApp.Domain.Integrations;
using Microsoft.Extensions.DependencyInjection;

namespace ChristmasTreeDeliveryApp.Integrations.Database
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
