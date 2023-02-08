using ChristmasTreeDeliveryApp.Api.Integration;
using Microsoft.Extensions.DependencyInjection;

namespace ChristmasTreeDeliveryApp.Domain.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDomain(this IServiceCollection services)
        {
            services.AddSingleton<ITreeDataProvider, TreeDataProvider>();

            return services;
        }
    }
}
