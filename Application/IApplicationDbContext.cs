using Domain.Keys;
using Microsoft.EntityFrameworkCore;

namespace Application.Data;


public interface IApplicationDbContext {
    DbSet<KeyAPI> KeysAPI { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default );
}