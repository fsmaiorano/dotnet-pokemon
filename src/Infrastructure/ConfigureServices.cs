﻿using Application.Common.Interfaces;
using Infrastructure.Context;
using Infrastructure.Firestore;
using Infrastructure.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton(provider => configuration);
        services.AddTransient<IDataContext>(provider => provider.GetRequiredService<DataContext>());
        services.AddTransient<IFirestoreService, FirestoreService>();
        services.AddScoped<ISaveChangesInterceptor, AuditableEntitySaveChangesInterceptor>();

        if (!AppDomain.CurrentDomain.FriendlyName.Contains("testhost"))
        {
            services.AddDbContextFactory<DataContext>(options =>
               options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
               b => b.MigrationsAssembly(typeof(DataContext).Assembly.FullName)), ServiceLifetime.Transient);
        }
        else
        {
            services.AddDbContextFactory<DataContext>(options =>
            {
                options.UseInMemoryDatabase("InMemoryDbForTesting");
            });

            // services.AddDbContext<DataContext>(options =>
            //  options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
            //  b => b.MigrationsAssembly(typeof(DataContext).Assembly.FullName)), ServiceLifetime.Transient);
        }

        return services;
    }
}
