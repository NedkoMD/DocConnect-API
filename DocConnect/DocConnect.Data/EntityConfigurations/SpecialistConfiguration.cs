using DocConnect.Data.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DocConnect.Data.EntityConfigurations
{
    public class SpecialistConfiguration : IEntityTypeConfiguration<Specialist>
    {
        public void Configure(EntityTypeBuilder<Specialist> builder)
        {
            builder.ToTable("specialist");

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.DoctorId).HasColumnName("doctor_id");
            builder.Property(e => e.SpecialityId).HasColumnName("speciality_id");
            builder.Property(e => e.CreatedAt).HasColumnName("created_at");
            builder.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            builder.Property(e => e.UpdatedAt).HasColumnName("updated_at");
        }
    }
}
