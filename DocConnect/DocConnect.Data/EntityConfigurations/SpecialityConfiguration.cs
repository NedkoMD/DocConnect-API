using DocConnect.Data.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DocConnect.Data.EntityConfigurations
{
    public class SpecialityConfiguration : IEntityTypeConfiguration<Speciality>
    {
        public void Configure(EntityTypeBuilder<Speciality> builder)
        {
            builder.ToTable("speciality");

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.CreatedAt).HasColumnName("created_at");
            builder.Property(e => e.ImageUrl).HasColumnName("image_url");
            builder.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            builder.Property(e => e.Name).HasColumnName("name");
            builder.Property(e => e.UpdatedAt).HasColumnName("updated_at");
        }
    }
}
