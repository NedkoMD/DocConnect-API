using DocConnect.Data.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DocConnect.Data.EntityConfigurations
{
    public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.ToTable("doctor");

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.CreatedAt).HasColumnName("created_at");
            builder.Property(e => e.ExperienceSince).HasColumnName("experience_since");
            builder.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            builder.Property(e => e.LocationId).HasColumnName("location_id");
            builder.Property(e => e.PictureLocation).HasColumnName("picture_location");
            builder.Property(e => e.Summary).HasColumnName("summary");
            builder.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            builder.Property(e => e.UserId).HasColumnName("user_id");

            builder.HasOne(d => d.Location).WithMany(p => p.Doctors)
                .HasForeignKey(d => d.LocationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("location_doctor_link");

            builder.HasOne(d => d.User).WithOne(p => p.Doctor)
                .HasForeignKey<Doctor>(d => d.UserId)
                .HasConstraintName("user_doctor_link");
        }
    }
}
