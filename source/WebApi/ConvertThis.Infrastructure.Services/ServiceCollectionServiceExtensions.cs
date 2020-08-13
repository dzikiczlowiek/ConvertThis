using Microsoft.Extensions.DependencyInjection;

namespace ConvertThis.Infrastructure.Services
{
    public static class ServiceCollectionServiceExtensions
    {
        public static IServiceCollection AddConverter<T>(this IServiceCollection services)
        {

            return services;
        }
    }
}
