﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Warehouse.DAL.Interceptors;
using Warehouse.DAL.Repositories;
using Warehouse.Domain.Entities;
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
        services.AddScoped<IBaseRepository<Product>, BaseRepository<Product>>();
        services.AddScoped<IBaseRepository<Supply>, BaseRepository<Supply>>();
        services.AddScoped<IBaseRepository<SupplyProducts>, BaseRepository<SupplyProducts>>();
        services.AddScoped<IBaseRepository<ProductWarehouse>, BaseRepository<ProductWarehouse>>();
        services.AddScoped<IBaseRepository<Employee>, BaseRepository<Employee>>();
        services.AddScoped<IBaseRepository<Achievement>, BaseRepository<Achievement>>();
        services.AddScoped<IBaseRepository<EmployeeAchievement>, BaseRepository<EmployeeAchievement>>();
        
    }
}