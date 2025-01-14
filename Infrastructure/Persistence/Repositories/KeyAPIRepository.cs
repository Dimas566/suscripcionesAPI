using Domain.Keys;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;


public class KeyAPIRepository : IKeyRepository
{
    private readonly ApplicationDbContext applicationDbContext;

    public KeyAPIRepository(ApplicationDbContext applicationDbContext)
    {
        this.applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
    }

    public void Add(KeyAPI keyAPI) => applicationDbContext.KeysAPI.Add(keyAPI);
    public void Delete(KeyAPI keyAPI) => applicationDbContext.KeysAPI.Remove(keyAPI);
    public void Update(KeyAPI keyAPI) => applicationDbContext.KeysAPI.Update(keyAPI);
    public async Task<bool> ExistsAsync(KeyID id) => await applicationDbContext.KeysAPI.AnyAsync(k => k.IdKey == id);
    public async Task<KeyAPI?> GetByIdAsync(KeyID id) => await applicationDbContext.KeysAPI.SingleOrDefaultAsync(k => k.IdKey == id);
    public async Task<List<KeyAPI>> GetAll() => await applicationDbContext.KeysAPI.ToListAsync();
}