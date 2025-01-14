using Domain.Keys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration;

public class KeyAPIConfiguration : IEntityTypeConfiguration<KeyAPI>
{
    public void Configure(EntityTypeBuilder<KeyAPI> builder)
    {
        builder.HasKey(k => k.IdKey);

        builder.Property(k => k.IdKey).HasConversion(
            keyId => keyId.Guid,
            value => new KeyID(value));

        builder.Property(k => k.Key).HasMaxLength(60);

        builder.Property(k => k.KeyType)
            .HasConversion<int>();

         builder.Property(k => k.Active);

        builder.Property(k => k.UserId)
            .HasMaxLength(50);

        builder.ToTable("KeyAPI");
    }
}


