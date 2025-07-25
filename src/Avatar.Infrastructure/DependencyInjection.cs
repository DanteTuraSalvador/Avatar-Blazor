using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Avatar.Core.Interfaces;
using Avatar.Infrastructure.Data;
using Avatar.Infrastructure.Repositories;
using Avatar.Infrastructure.Services;

namespace Avatar.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Add DbContext
        services.AddDbContext<AvatarDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        // Add repositories
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IAvatarRepository, AvatarRepository>();

        // Add services
        services.AddScoped<IAvatarService, AvatarService>();

        return services;
    }

    public static IServiceCollection AddInfrastructureInMemory(this IServiceCollection services)
    {
        // Add DbContext with In-Memory database - use a fixed name so all instances share the same database
        services.AddDbContext<AvatarDbContext>(options =>
            options.UseInMemoryDatabase(databaseName: "AvatarInMemoryDb"));

        // Add repositories
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IAvatarRepository, AvatarRepository>();

        // Add services
        services.AddScoped<IAvatarService, AvatarService>();

        return services;
    }
}
