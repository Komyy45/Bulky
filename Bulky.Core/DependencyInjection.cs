using Bulky.Core.Contracts.Services;
using Bulky.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Bulky.Core;

public static class DependencyInjection
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services)
    {
        services.AddScoped<ICategoryService, CategoryService>();
        return services;
    }
}