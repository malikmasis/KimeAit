using KimeAit.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace KimeAit.Api.Extentions;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
                                                       IConfiguration configuration)
    {
        AddPostgresql(services, configuration);

        return services;
    }

    private static void AddPostgresql(IServiceCollection services,
                                      IConfiguration configuration)
    {
        //AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        var connectionString = configuration["ConnectionString"];

        services.AddDbContext<KimeAitDbContext>(options =>
        {
            options.UseSqlite(
                connectionString,
                sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(typeof(KimeAitDbContext).Assembly.FullName);
                });

            //options.UseSnakeCaseNamingConvention();
        });

        services.AddScoped<IKimeAitDbContext>(provider => provider.GetRequiredService<KimeAitDbContext>());
    }
}