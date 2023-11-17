using KimeAit.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace KimeAit.Api.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
                                                       IConfiguration configuration)
    {
        AddSqlConfiguration(services, configuration);

        return services;
    }

    private static void AddSqlConfiguration(IServiceCollection services,
                                            IConfiguration configuration)
    {
        var connectionString = configuration["ConnectionString"];

        services.AddDbContext<KimeAitDbContext>(options =>
        {
            options.UseSqlite(
                connectionString,
                sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(typeof(KimeAitDbContext).Assembly.FullName);
                });
        });

        services.AddScoped<IKimeAitDbContext>(provider => provider.GetRequiredService<KimeAitDbContext>());
    }
}