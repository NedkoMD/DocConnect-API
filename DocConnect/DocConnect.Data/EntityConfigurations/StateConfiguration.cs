using DocConnect.Data.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DocConnect.Data.EntityConfigurations
{
    public class StateConfiguration : IEntityTypeConfiguration<State>
    {
        public void Configure(EntityTypeBuilder<State> builder)
        {
            builder.ToTable("state");

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.Ansi).HasColumnName("ansi");
            builder.Property(e => e.CountryId).HasColumnName("country_id");
            builder.Property(e => e.CreatedAt).HasColumnName("created_at");
            builder.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            builder.Property(e => e.Name).HasColumnName("name");
            builder.Property(e => e.UpdatedAt).HasColumnName("updated_at");
        }
    }
}
