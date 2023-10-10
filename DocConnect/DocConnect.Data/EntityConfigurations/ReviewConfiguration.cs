using DocConnect.Data.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DocConnect.Data.EntityConfigurations
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.ToTable("review");
            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.Content).HasColumnName("content");
            builder.Property(e => e.CreatedAt).HasColumnName("created_at");
            builder.Property(e => e.DoctorId).HasColumnName("doctor_id");
            builder.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            builder.Property(e => e.PatientId).HasColumnName("patient_id");
            builder.Property(e => e.Raiting).HasColumnName("raiting");
            builder.Property(e => e.UpdatedAt).HasColumnName("updated_at");
        }
    }
}
