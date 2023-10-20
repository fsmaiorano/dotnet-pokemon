using Application.Common.Interfaces;
using Infrastructure.Context;
using Infrastructure.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton(provider => configuration);

        services.AddScoped<AuditableEntitySaveChangesInterceptor>();
        services.AddScoped<ApplicationDbContextInitialiser>();
        // services.AddTransient<IDataContext>(provider => provider.GetRequiredService<DataContext>());
        services.AddTransient<IDataContext>(provider => provider.GetRequiredService<DataContext>());


        if (!AppDomain.CurrentDomain.FriendlyName.Contains("testhost"))
        {
            services.AddDbContext<DataContext>(options =>
               options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
               b => b.MigrationsAssembly(typeof(DataContext).Assembly.FullName)), ServiceLifetime.Transient);
        }
        else
        {
            services.AddDbContext<DataContext>(options =>
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
