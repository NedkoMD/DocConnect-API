using DocConnect.Data.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DocConnect.Data.EntityConfigurations
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.ToTable("country");

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.Alpha2).HasColumnName("alpha-2");
            builder.Property(e => e.Alpha3).HasColumnName("alpha-3");
            builder.Property(e => e.CreatedAt).HasColumnName("created_at");
            builder.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            builder.Property(e => e.Name).HasColumnName("name");
            builder.Property(e => e.UpdatedAt).HasColumnName("updated_at");
        }
    }
}
