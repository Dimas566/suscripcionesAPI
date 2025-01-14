namespace Domain.Keys;

public interface IKeyRepository {

    Task<List<KeyAPI>> GetAll();
    Task<KeyAPI?> GetByIdAsync(KeyID id);
    Task<bool> ExistsAsync(KeyID id);
    void Add(KeyAPI keyAPI);
    void Update(KeyAPI keyAPI);
    void Delete(KeyAPI keyAPI);
}