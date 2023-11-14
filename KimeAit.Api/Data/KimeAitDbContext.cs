using KimeAit.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace KimeAit.Api.Data;

public sealed class KimeAitDbContext : DbContext, IKimeAitDbContext
{
    public KimeAitDbContext(DbContextOptions<KimeAitDbContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }

    public DbSet<UserAccount> UserAccounts { get; set; }

    public DbSet<AlternativeProduct> AlternativeProducts { get; set; }

    public async Task MigrateAsync() => await Database.MigrateAsync();
}