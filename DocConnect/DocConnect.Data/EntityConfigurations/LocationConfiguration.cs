using DocConnect.Data.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DocConnect.Data.EntityConfigurations
{
    public class LocationConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.ToTable("location");

            builder.HasKey(e => e.Id).HasName("PRIMARY");
            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.Address).HasColumnName("address");
            builder.Property(e => e.CityId).HasColumnName("city_id");
            builder.Property(e => e.CreatedAt).HasColumnName("created_at");
            builder.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            builder.Property(e => e.StateId).HasColumnName("state_id");
            builder.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            builder.Property(e => e.Zip).HasColumnName("zip");
        }
    }
}
