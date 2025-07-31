using Bulky.Core.Application.Services;
using Bulky.Core.Ports.In;
using Microsoft.Extensions.DependencyInjection;

namespace Bulky.Core;

public static class DependencyInjection
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services)
    {
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IBasketService, BasketService>();
        return services;
    }
}