using DocConnect.Data.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DocConnect.Data.EntityConfigurations
{
    public class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.ToTable("patient");

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.BloodPressure).HasColumnName("blood_pressure");
            builder.Property(e => e.BloodSugar).HasColumnName("blood_sugar");
            builder.Property(e => e.CreatedAt).HasColumnName("created_at");
            builder.Property(e => e.Height).HasColumnName("height");
            builder.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            builder.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            builder.Property(e => e.UserId).HasColumnName("user_id");
            builder.Property(e => e.Weight).HasColumnName("weight");
        }
    }
}
