using Domain.Primitives;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Domain.Keys;

public sealed class KeyAPI : AggregateRoot {
    public KeyAPI(KeyID id,string key,KeyType keyType,bool active,string userId)
    {
        IdKey = id;
        Key = key;
        KeyType = keyType;
        Active = active;
        UserId = userId;
        AddedDate = DateTime.UtcNow;
        //User = identityUser;

        if(id is not null){
            ModifiedDate = DateTime.UtcNow;
        }
    }

    private KeyAPI(){}

    public KeyID IdKey { get; private set; }
    public string Key { get; private set; } = string.Empty;
    public KeyType KeyType{ get; private set; }
    public bool Active { get; private set; }
    public string UserId { get; private set; } = string.Empty;

    public DateTime AddedDate { get; private set; }
    public DateTime ModifiedDate { get; private set; }
   // public IdentityUser User { get; private set; }


   public static KeyAPI UpdateKeyAPI(Guid id,string key,KeyType keyType,bool active,string userId)
    {
        return new KeyAPI(new KeyID(id), key, keyType, active, userId);
    }
}

public enum KeyType {
    Free = 1,
    Professional = 2
}