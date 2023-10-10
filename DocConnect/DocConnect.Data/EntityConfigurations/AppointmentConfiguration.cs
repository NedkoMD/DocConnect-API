using DocConnect.Data.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DocConnect.Data.EntityConfigurations
{
    public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.HasKey(e => e.Id).HasName("PRIMARY");

            builder.ToTable("appointment");

            builder.HasIndex(e => e.DoctorId, "index_doctor_id");

            builder.HasIndex(e => e.PatientId, "index_patient_id");

            builder.HasIndex(e => e.TimeSlot, "index_time_slot");

            builder.HasIndex(e => e.Id, "unique_id").IsUnique();

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            builder.Property(e => e.DoctorId).HasColumnName("doctor_id");
            builder.Property(e => e.IsCanceled).HasColumnName("is_canceled");
            builder.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            builder.Property(e => e.Notes)
                .HasColumnType("text")
                .HasColumnName("notes");
            builder.Property(e => e.PatientId).HasColumnName("patient_id");
            builder.Property(e => e.TimeSlot)
                .HasColumnType("datetime")
                .HasColumnName("time_slot");
            builder.Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");

            builder.HasOne(d => d.Doctor).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.DoctorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("doctor_appointment_link");

            builder.HasOne(d => d.Patient).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("patient_appointment_link");
        }
    }
}
