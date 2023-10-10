using DocConnect.Data.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DocConnect.Data.EntityConfigurations
{
    public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.ToTable("city");

            builder.HasKey(e => e.Id).HasName("PRIMARY");
            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.CreatedAt).HasColumnName("created_at");
            builder.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            builder.Property(e => e.Name).HasColumnName("name");
            builder.Property(e => e.StateId).HasColumnName("state_id");
            builder.Property(e => e.TimeZone).HasColumnName("time_zone");
            builder.Property(e => e.TimeZoneLoc).HasColumnName("time_zone_loc");
            builder.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            builder.Property(e => e.UtcDifference).HasColumnName("utc_difference");
        }
    }
}
