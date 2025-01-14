namespace KeysAPI.Common;

public record KeyAPIResponse(
Guid Id,
string Key,
string KeyType,
bool Active,
string UserId
);
