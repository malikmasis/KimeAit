using KimeAit.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace KimeAit.Api.Data;

public interface IKimeAitDbContext
{
    DbSet<Product> Products { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    Task MigrateAsync();
}