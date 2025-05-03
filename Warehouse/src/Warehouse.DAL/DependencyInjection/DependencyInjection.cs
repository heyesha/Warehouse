using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Warehouse.DAL.Interceptors;
using Warehouse.DAL.Repositories;
using Warehouse.Domain.Interfaces.Repositories;

namespace Warehouse.DAL.DependencyInjection;

public static class DependencyInjection
{
    public static void AddDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("PostgresSQL");

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });
        services.AddSingleton<DateInterceptor>();
        services.InitRepositories();
    }

    public static void InitRepositories(this IServiceCollection services)
    {
        services.AddScoped<IBaseRepository<Domain.Entities.Warehouse>, BaseRepository<Domain.Entities.Warehouse>>();
    }
}