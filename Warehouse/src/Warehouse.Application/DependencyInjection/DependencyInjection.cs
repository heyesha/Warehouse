using Microsoft.Extensions.DependencyInjection;
using Warehouse.Application.Services;
using Warehouse.Domain.Interfaces.Services;

namespace Warehouse.Application.DependencyInjection;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        InitServices(services);
    }

    private static void InitServices(this IServiceCollection services)
    {
        services.AddScoped<IWarehouseService, WarehouseService>();
    }
}